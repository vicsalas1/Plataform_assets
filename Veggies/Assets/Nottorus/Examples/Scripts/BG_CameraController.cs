//MD5Hash:9128cd74666213e524a5b06f950385a2;
using UnityEngine;
using System;


public class BG_CameraController : UnityEngine.MonoBehaviour
{
	[HideInInspector]
	public UnityEngine.Transform FollowObject = null;
	public float CamRoot_X = 40f;
	public float CamRoot_Y = -90f;
	[SpaceAttribute(5f)]
	public float CamDistance = 10f;
	public float MouseCensivity = 1f;
	[SpaceAttribute(5f)]
	public float CameraMax_X = 70f;
	public float CameraMin_X = 10f;
	public float ZoomStep = 10f;
	[SpaceAttribute(5f)]
	public float CamDist_Min = 3f;
	public float CamDist_Max = 13f;
	[HideInInspector]
	public UnityEngine.Vector3 CamDir;
	private static BG_CameraController _instance = null;
	public UnityEngine.LayerMask CheckLayerMask;

	public static BG_CameraController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<BG_CameraController>();
			}

			return _instance;
		}
	}

	void Update()
	{
		UnityEngine.Quaternion CamRotation;

		if (BG_GameController.Instance.CameraLocked)
		{
			return;
		}

		if (FollowObject == null)
		{
			return;
		}

		ProcessInput();
		CamRotation = UnityEngine.Quaternion.Euler(CamRoot_X, CamRoot_Y, 0f);
		CamDir = CamRotation * new UnityEngine.Vector3(0f, 0f, (CamDistance * -1f));
		transform.position = CheckHitWall((CamDir + FollowObject.position));
		transform.rotation = CamRotation;
	}
	public void ProcessInput()
	{
		CamRoot_Y += (UnityEngine.Input.GetAxis("Mouse X") * MouseCensivity);
		CamRoot_X -= (UnityEngine.Input.GetAxis("Mouse Y") * MouseCensivity);
		CamRoot_X = UnityEngine.Mathf.Clamp(CamRoot_X, CameraMin_X, CameraMax_X);
		CamDistance -= (UnityEngine.Input.GetAxis("Mouse ScrollWheel") * ZoomStep);
		CamDistance = UnityEngine.Mathf.Clamp(CamDistance, CamDist_Min, CamDist_Max);
	}
	public UnityEngine.Vector3 CheckHitWall(UnityEngine.Vector3 WantedPosition)
	{
		UnityEngine.RaycastHit _hit;

		if (UnityEngine.Physics.Linecast(FollowObject.position, WantedPosition, out _hit, CheckLayerMask))
		{
			return _hit.point;
		}
		else
		{
			return WantedPosition;
		}

	}
}


