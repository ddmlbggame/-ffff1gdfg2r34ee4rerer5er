using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFinish : UIBase {

	public  static UIInfo Info = new UIInfo(UIType.Finish, UIHierarchyType.Dialog, "UI_Finish");
	[SerializeField]
	private GameObject _back;
	[SerializeField]
	private GameObject _restart;
	[SerializeField]
	private GameObject _next;
	public override void OnEnable()
	{
		base.OnEnable();
		EventTriggerListener.Get(this._back).onClick = this._OnClickBack;
		EventTriggerListener.Get(this._restart).onClick = this.OnClickRestart;
		EventTriggerListener.Get(this._next).onClick = this.OnClickNext;
	}

	private void _Refresh()
	{
		if(GameControl.Instance.game_data._current_game_type == GameType.Custom)
		{
			int max_level = GameControl.Instance.game_data.GetLevelDatas(GameControl.Instance.game_data.Current_Difficulty).Count;
			if (GameControl.Instance.game_data.currentGameLevel < max_level)
			{
				this._next.SetActive(true);
				this._restart.SetActive(false);
			}else
			{
				this._next.SetActive(false);
				this._restart.SetActive(true);
			}
		}else
		{
			this._next.SetActive(false);
			this._restart.SetActive(true);
		}
		
	}
	private void _OnClickBack(GameObject obj)
	{
		UIManager.Instance.Hide(Info);
		UIManager.Instance.PopShow();
	}

	private void OnClickRestart(GameObject obj)
	{
		UIManager.Instance.Hide(Info);
		GameScene.Instance.SetGameStart();
		GameControl.Instance.game_data.ResetChallangeData();
		GameControl.HandleRestartEvent();
	}

	private void OnClickNext(GameObject obj)
	{
		UIManager.Instance.Hide(Info);
		GameControl.Instance.game_data.currentGameLevel++;
		GameScene.Instance.SetGameStart(false);
		GameControl.HandleRestartEvent();

	}
}
