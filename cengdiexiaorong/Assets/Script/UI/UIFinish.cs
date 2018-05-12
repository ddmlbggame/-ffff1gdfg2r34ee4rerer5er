using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFinish : UIBase {

	public  static UIInfo Info = new UIInfo(UIType.Finish, UIHierarchyType.Dialog, "UI_Finish");
	[SerializeField]
	private GameObject _back;
	[SerializeField]
	private GameObject _restart;
	public override void OnEnable()
	{
		base.OnEnable();
		EventTriggerListener.Get(this._back).onClick = this._OnClickBack;
		EventTriggerListener.Get(this._restart).onClick = this.OnClickRestart;
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
}
