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

	[SerializeField]
	private GameObject _tip;

	[SerializeField]
	private GameObject _challange_parent;

	[SerializeField]
	private GameObject _cumsom_parent;

	[SerializeField]
	private Text _custom_level;

	[SerializeField]
	private Text _custom_time;

	private int rest_time;


	public override void OnEnable()
	{
		base.OnEnable();
		EventTriggerListener.Get(this._back).onClick = this.OnClickBack;
		EventTriggerListener.Get(this._tip).onClick = this.OnClickTip;
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
		if (this._custom_count_time != null)
		{
			StopCoroutine(this._custom_count_time);
		}
	}

	public void Show()
	{
		if (GameControl.Instance.game_data._current_game_type == GameType.challenge)
		{
			this._challange_parent.SetActive(true);
			this._cumsom_parent.SetActive(false);
			if (this._count_time != null)
			{
				StopCoroutine(this._count_time);
			}
			this._count_time = _CountTime();
			this.rest_time = GameControl.Instance.game_data.ChallangeTime;
			StartCoroutine(this._count_time);
		}
		else
		{
			this._challange_parent.SetActive(false);
			this._cumsom_parent.SetActive(true);
			if (this._custom_count_time != null)
			{
				StopCoroutine(this._custom_count_time);
			}
			this._custom_count_time = _CustomCountTime();
			StartCoroutine(this._custom_count_time);
		}
	
	}

	private IEnumerator _count_time = null;
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

	private IEnumerator _custom_count_time = null;
	private IEnumerator _CustomCountTime()
	{
		int time = 0;
		while (true)
		{
			this._custom_time.text = SetTimeFormat(time);
			yield return new WaitForSeconds(1);
			time++;
		}
	}

	public static string SetTimeFormat(int time)
	{
		int second = time % 60;
		int min = (time % 3600 - second)/60;
		return string.Format("{0}:{1}", min.ToString("00"), second.ToString("00"));
	}

	private void OnClickBack(GameObject obj)
	{
		FSoundManager.StopMusic();
		UIManager.Instance.PopShow();
	}

	private void OnClickTip(GameObject obj)
	{
		GameScene.Instance.ShowTip();
	}
}
