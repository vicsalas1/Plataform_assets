//MD5Hash:8644640017ecffd919631457e7b72c98;
using UnityEngine;
using System;
using UnityEngine.UI;


public class BG_GuiBonusController : UnityEngine.MonoBehaviour
{
	public int Score = 0;
	public UnityEngine.UI.Text ScoreTextLabel = null;
	public UnityEngine.GameObject EndGameLabel = null;
	private static BG_GuiBonusController _instance = null;

	public static BG_GuiBonusController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<BG_GuiBonusController>();
			}

			return _instance;
		}
	}

	public void AddScore(int score)
	{
		Score += score;
		ScoreTextLabel.text = Score.ToString();
	}
	public void ControlGameMessage(bool open)
	{
		EndGameLabel.SetActive(open);
	}
	public void SetScore(int newScore)
	{
		Score = newScore;
		ScoreTextLabel.text = Score.ToString();
	}
}


