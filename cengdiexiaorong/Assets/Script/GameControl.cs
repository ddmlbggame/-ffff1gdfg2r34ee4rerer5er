using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameControl {
	public GameData game_data;

	public AudioControl audio_control;

	private static GameControl _instance;

	public static Action RestartEvent;

	public static void HandleRestartEvent()
	{
		if (RestartEvent != null)
		{
			RestartEvent();
		}
	}
	public static GameControl Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameControl();
			}
			return _instance;
		}
	}

	public void Init(Transform parent)
	{
		game_data = new GameData();
		var obj = Resources.Load("UI/GamePlayPanel");
		if (obj != null)
		{
			GameObject.Instantiate(obj, UIManager.Instance.Game);
		}else
		{
			Debug.LogError("can't load UI/GamePlayPanel");
		}

		var audio = Resources.Load("UI/AudioSource");
		if (audio != null)
		{
			GameObject audio_obj =GameObject.Instantiate(audio, parent) as GameObject;
			this.audio_control = audio_obj.GetComponent<AudioControl>();
		}
		else
		{
			Debug.LogError("can't load UI/AudioSource");
		}

	}

	public void PlayGame(GameType type , LevelDifficulty level_difficulty ,int level)
	{
		GameControl.Instance.game_data.currentGameLevel = level;
		GameControl.Instance.game_data.Current_Difficulty = level_difficulty;
		GameControl.Instance.game_data._current_game_type = type;
		UIManager.Instance.PushShow(UIMain.Info,true);
		GameScene.gameSceneInsta.SetGameStart();
	}


	public void DoGameOver()
	{
		game_data.isGamePlay = false;
		UnityEngine.Debug.Log("Finished");
		//this.startPanel.ShowPanel(true);
		this.audio_control.PlayAudio( SoundType.success);
		UIManager.Instance.PushShow(UIFinish.Info);
	}

}
