//MD5Hash:7504a4547c3858da376a7715f6f88ef3;
using UnityEngine;
using System;


public class BG_BallController : UnityEngine.MonoBehaviour
{
	private UnityEngine.Rigidbody RBody = null;
	private float Axis_FW = 0f;
	private float Axis_Sides = 0f;
	private UnityEngine.Vector3 BallTorgueVector;
	public float ForceScale = 2f;
	private BG_CameraController _camController = null;


	void Start()
	{
		RBody = GetComponent<UnityEngine.Rigidbody>();
		_camController = BG_CameraController.Instance;
	}
	void Update()
	{
		ProcessInput();
		var _TempVar_121_1 = UnityEngine.Vector3.ProjectOnPlane(_camController.CamDir, new UnityEngine.Vector3(0f, 1f, 0f));
		BallTorgueVector = _TempVar_121_1 * Axis_Sides;
		BallTorgueVector += UnityEngine.Quaternion.AngleAxis(90f, new UnityEngine.Vector3(0f, 1f, 0f)) * _TempVar_121_1 * Axis_FW;
	}
	public void ProcessInput()
	{
		Axis_FW = (UnityEngine.Input.GetAxis("Vertical") * -1f);
		Axis_Sides = UnityEngine.Input.GetAxis("Horizontal");
	}
	public void FixedUpdate()
	{
		RBody.AddTorque(BallTorgueVector.normalized * ForceScale, UnityEngine.ForceMode.Impulse);
	}
}


