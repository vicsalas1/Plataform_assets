//MD5Hash:6113c80de55734e914c1e9bc22b7c403;
using UnityEngine;
using System;


public class BG_Bonus : UnityEngine.MonoBehaviour
{
	public int BonusScore = 5;
	public UnityEngine.Color32 LerpColor1 = new UnityEngine.Color32(71, 255, 0, 255);
	public UnityEngine.Color32 LerpColor2 = new UnityEngine.Color32(255, 236, 0, 255);
	private UnityEngine.Light _light = null;


	public void Start()
	{
		_light = GetComponent<UnityEngine.Light>();
	}
	void Update()
	{
		transform.Rotate(new UnityEngine.Vector3(0f, 0f, 0f));
		_light.color = UnityEngine.Color32.Lerp(LerpColor1, LerpColor2, UnityEngine.Mathf.Sin(((UnityEngine.Time.time) * (3f))));
	}
	public void OnTriggerEnter(UnityEngine.Collider collider)
	{
		BG_GuiBonusController.Instance.AddScore(BonusScore);
		Destroy(gameObject);
	}
}


