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

	public static int custom_cost_time =0;

	public override void OnEnable()
	{
		base.OnEnable();
		EventTriggerListener.Get(this._back).onClick = this.OnClickBack;
		EventTriggerListener.Get(this._tip).onClick = this.OnClickTip;
		GameControl.RestartEvent += this.Show;
		GameControl.FinishChallangeOneEvent += this.RefreshChange;
		Show();
	}
	public override void OnDisable()
	{
		base.OnDisable();
		GameControl.RestartEvent -= this.Show;
		GameControl.FinishChallangeOneEvent -= this.RefreshChange;
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
			this._level.text = string.Format("完成 {0}关", GameControl.Instance.game_data.ChallangePassedNumber);
			this._challange_parent.SetActive(true);
			this._cumsom_parent.SetActive(false);
			if (this._count_time != null)
			{
				StopCoroutine(this._count_time);
			}
			this._count_time = _CountTime();
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

	private void RefreshChange()
	{
		this._level.text = string.Format("完成 {0}关", GameControl.Instance.game_data.ChallangePassedNumber);
	}
	private IEnumerator _count_time = null;
	YieldInstruction wait = new WaitForSeconds(0.25f);
	private IEnumerator _CountTime()
	{
		float time = 0;
		while (GameControl.Instance.game_data.ChallangeRestTime >= 0)
		{
			this._time.text = GameControl.Instance.game_data.ChallangeRestTime.ToString();
			yield return wait;
			yield return wait;
			if (GameControl.Instance.game_data.ChallangeRestTime <= 4)
			{
				kaca++;
				PlayKaCa();
			}
			yield return wait;
			yield return wait;
			kaca++;
			GameControl.Instance.game_data.ChallangeRestTime--;
			if (GameControl.Instance.game_data.ChallangeRestTime <=9)
			{
				PlayKaCa();
			}

		}
		// 游戏结束
		GameControl.Instance.ChallangeGameFinshed();
	}

	int kaca = 0;
	private void PlayKaCa()
	{
		if (kaca % 2 == 1)
		{
			FSoundManager.PlaySound("ca");
		}
		else
		{
			FSoundManager.PlaySound("ka");
		}
	}

	private IEnumerator _custom_count_time = null;
	private IEnumerator _CustomCountTime()
	{
		custom_cost_time = 0;
		while (true)
		{
			this._custom_time.text = GameControl.SetTimeFormat(custom_cost_time);
			yield return new WaitForSeconds(1);
			custom_cost_time++;
		}
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
