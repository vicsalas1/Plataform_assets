//MD5Hash:5260432f9a19d632fc66e2ea6f5ce0f2;
using UnityEngine;
using System;


public class BC_Cube_Converter : UnityEngine.MonoBehaviour
{
	[TooltipAttribute("Type of material")]
	public string ConvertFilter = "Wood";
	public UnityEngine.Transform TeleportPosition = null;
	public UnityEngine.Transform SpawnPos = null;
	private bool _animationTrigger = false;
	private UnityEngine.Animator _animator = null;
	private UnityEngine.Transform _ball = null;
	private UnityEngine.Rigidbody _ball_Rigidbody = null;
	public UnityEngine.GameObject NewBallPrefab = null;
	private UnityEngine.GameObject _newSpawnedBall = null;
	private UnityEngine.Rigidbody _newSpawnedBallRigidbody = null;
	private bool _converterEnabled = true;


	void Start()
	{
		_InitialiseStateMachine();
		_animator = GetComponent<UnityEngine.Animator>();
	}
	public void OnTriggerEnter(UnityEngine.Collider collider)
	{
		if (_converterEnabled)
		{
			var _TempVar_113_1 = collider.transform;
			if (!_TempVar_113_1.name.Contains(ConvertFilter))
			{
				_ball = _TempVar_113_1;
				_ball_Rigidbody = collider.gameObject.GetComponent<UnityEngine.Rigidbody>();
				_converterEnabled = false;
				_animationTrigger = true;
			}

		}

	}
	void Update()
	{
		_StateMachine.UpdateStateMachine();
	}

#region stateMachine
	//////////////////////////// State Machine /////////////////////////////////////
	private NSM_StateMachine _StateMachine = new NSM_StateMachine();
	private void _InitialiseStateMachine()
	{
		//// States:
		var _state_0 = _StateMachine.AddNewState("Entry");
		var _state_1 = _StateMachine.AddNewState("Wait for a ball hit trigger");
		var _state_2 = _StateMachine.AddNewState("Start Animation");
		var _state_3 = _StateMachine.AddNewState("Change camera target");

		//// Transitions:
		var _transition_0_1 = _state_0.AddNewTransition(new StateM_Transition(_state_1));
		var _transition_1_2 = _state_1.AddNewTransition(new StateM_Transition(_state_2, true, 0.5f));
		var _transition_2_3 = _state_2.AddNewTransition(new StateM_Transition(_state_3, true, 1f));
		var _transition_3_1 = _state_3.AddNewTransition(new StateM_Transition(_state_1, true, 2.4f));

		//// Events Linking:
		_transition_1_2.OnStartTransit = _transition_1_2_On_Start_Transition;
		_transition_1_2.OnEndTransit = _transition_1_2_On_End_Transition;
		_transition_1_2.TransitPosition = _transition_1_2_Transition_Loop;
		_transition_2_3.OnEndTransit = _transition_2_3_On_End_Transition;
		_transition_3_1.OnEndTransit = _transition_3_1_On_End_Transition;

		//// Transition Conditions:
		_transition_0_1.TestAllowTransit = delegate { return true; };
		_transition_1_2.TestAllowTransit = delegate {  if(_animationTrigger) {_animationTrigger = false; return true; } return false; };
		_transition_2_3.TestAllowTransit = delegate { return true; };
		_transition_3_1.TestAllowTransit = delegate { return true; };

		_StateMachine.InitStateMachine(_state_0);
	}

	//// Events
	private void _transition_1_2_On_Start_Transition()
	{
		_ball_Rigidbody.isKinematic = true;
	}
	private void _transition_1_2_On_End_Transition()
	{
		_ball.parent = TeleportPosition;
		_animator.SetTrigger("StartConversion");
		_newSpawnedBall = (Instantiate(NewBallPrefab, SpawnPos.position, SpawnPos.rotation) as UnityEngine.GameObject);
		_newSpawnedBall.transform.parent = SpawnPos;
		_newSpawnedBallRigidbody = _newSpawnedBall.GetComponent<UnityEngine.Rigidbody>();
		_newSpawnedBallRigidbody.isKinematic = true;
	}
	private void _transition_1_2_Transition_Loop(float _transitionDelta)
	{
		_ball.position = UnityEngine.Vector3.Lerp(_ball.position, TeleportPosition.position, _transitionDelta);
	}
	private void _transition_2_3_On_End_Transition()
	{
		BG_CameraController.Instance.FollowObject = _newSpawnedBall.transform;
	}
	private void _transition_3_1_On_End_Transition()
	{
		_newSpawnedBallRigidbody.isKinematic = false;
		_newSpawnedBall.transform.parent = null;
		Destroy(_ball.gameObject);
		_converterEnabled = true;
	}
	//////////////////////////// End State Machine /////////////////////////////////
#endregion stateMachine

}


