using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : UIBase {

	public static UIInfo Info = new UIInfo(UIType.Pause, UIHierarchyType.Normal, "UI_Pause");

	public GameObject close;

	public override void OnEnable()
	{
		EventTriggerListener.Get(this.close).onClick = this._OnClose;
	}

	private void _OnClose(GameObject obj)
	{
		UIManager.Instance.Hide(Info);
	}
}
