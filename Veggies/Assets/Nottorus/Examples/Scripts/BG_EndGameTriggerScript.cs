//MD5Hash:142f0957f3fe36187a068c54e6ec94b1;
using UnityEngine;
using System;


public class BG_EndGameTriggerScript : UnityEngine.MonoBehaviour
{


	void OnTriggerEnter(UnityEngine.Collider collider)
	{
		BG_GuiBonusController.Instance.ControlGameMessage(true);
	}
}


