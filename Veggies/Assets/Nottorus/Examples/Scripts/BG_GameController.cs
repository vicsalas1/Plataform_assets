//MD5Hash:952bc83c8516c9abeb1e58c353da421e;
using UnityEngine;
using System;


public class BG_GameController : UnityEngine.MonoBehaviour
{
	private bool MainMenuOpened = false;
	public UnityEngine.GameObject MainMenuPanel = null;
	private static BG_GameController _instance = null;
	public UnityEngine.GameObject StartBallPrefab = null;
	public UnityEngine.Transform StartSpawnPosition = null;

	public bool CameraLocked
	{
		get
		{
			return MainMenuOpened;
		}
		set
		{
			MainMenuPanelState(value);
		}
	}
	public static BG_GameController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<BG_GameController>();
			}

			return _instance;
		}
	}

	void Update()
	{
		if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Escape))
		{
			MainMenuPanelState(!(MainMenuOpened));
			BG_GuiBonusController.Instance.ControlGameMessage(false);
		}

	}
	public void MainMenuPanelState(bool opened)
	{
		MainMenuOpened = opened;
		MainMenuPanel.SetActive(MainMenuOpened);
		UnityEngine.Time.timeScale = (MainMenuOpened) ? (0f) : (1f);
		UnityEngine.Cursor.lockState = (MainMenuOpened) ? (UnityEngine.CursorLockMode.None) : (UnityEngine.CursorLockMode.Locked);
		UnityEngine.Cursor.visible = MainMenuOpened;
	}
	public void StartNewGame()
	{
		MainMenuPanelState(false);
		if (BG_CameraController.Instance.FollowObject == null)
		{
		}
		else
		{
			Destroy(BG_CameraController.Instance.FollowObject.gameObject);
		}

		BG_CameraController.Instance.FollowObject = (Instantiate(StartBallPrefab, StartSpawnPosition.position, StartSpawnPosition.rotation) as UnityEngine.GameObject).transform;
	}
	public void ResetScore()
	{
		BG_GuiBonusController.Instance.SetScore(0);
		MainMenuPanelState(false);
	}
	public void QuitGame()
	{
		UnityEngine.Application.Quit();
	}
}

