using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIBase {

	public static UIInfo Info = new UIInfo(UIType.Main, UIHierarchyType.Normal, "UI_Main");
	[SerializeField]
	private GameObject _back;

	[SerializeField]
	private Text _level;

	[SerializeField]
	private Text _time;

	private int rest_time;
	private IEnumerator _count_time = null;

	public override void OnEnable()
	{
		base.OnEnable();
		EventTriggerListener.Get(this._back).onClick = this.OnClickBack;
		GameControl.RestartEvent += this.Show;
		Show();
	}
	public override void OnDisable()
	{
		base.OnDisable();
		GameControl.RestartEvent -= this.Show;
		if (this._count_time != null)
		{
			StopCoroutine(this._count_time);
		}
	}

	public void Show()
	{
		if(this._count_time != null)
		{
			StopCoroutine(this._count_time);
		}
		this._count_time = _CountTime();
		if (GameControl.Instance.game_data._current_game_type == GameType.challenge)
		{
			this.rest_time = GameControl.Instance.game_data.ChallangeTime;
			StartCoroutine(this._count_time);
		}
		else
		{

		}
	}

	private IEnumerator _CountTime()
	{
		while (this.rest_time > 0)
		{
			this._time.text = this.rest_time.ToString();
			yield return new WaitForSeconds(1);
			this.rest_time--;
		}
		// 游戏结束
		GameControl.Instance.DoGameOver();
	}
	private void OnClickBack(GameObject obj)
	{
		UIManager.Instance.PopShow();
	}

}
