using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
	//[CompilerGenerated]
	//private sealed class <ShowGameScoreIe>c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	//{
	//	internal int <jiBaiRenShu>__0;

	//	internal float <moveSpeed>__0;

	//	internal Vector3 <targetPosition>__0;

	//	internal StartPanel $this;

	//	internal object $current;

	//	internal bool $disposing;

	//	internal int $PC;

	//	object IEnumerator<object>.Current
	//	{
	//		[DebuggerHidden]
	//		get
	//		{
	//			return this.$current;
	//		}
	//	}

	//	object IEnumerator.Current
	//	{
	//		[DebuggerHidden]
	//		get
	//		{
	//			return this.$current;
	//		}
	//	}

	//	[DebuggerHidden]
	//	public <ShowGameScoreIe>c__Iterator0()
	//	{
	//	}

	//	public bool MoveNext()
	//	{
	//		uint num = (uint)this.$PC;
	//		this.$PC = -1;
	//		switch (num)
	//		{
	//		case 0u:
	//			this.<jiBaiRenShu>__0 = CommonDefine.GetLastJiBaiRenShu() + CommonDefine.RandomInt(10, 1000) + CommonDefine.currentLevel * 10;
	//			CommonDefine.SaveJiBaiRenShu(this.<jiBaiRenShu>__0);
	//			this.<moveSpeed>__0 = 100f;
	//			this.<targetPosition>__0 = new Vector3(0f, 200f, 0f);
	//			this.$this.gameScoreText.gameObject.SetActive(true);
	//			this.$this.gameScoreText.gameObject.transform.localPosition = Vector3.zero;
	//			this.$this.gameScoreText.text = "击败" + this.<jiBaiRenShu>__0 + "人";
	//			break;
	//		case 1u:
	//			if (this.$this.gameScoreText.transform.localPosition == this.<targetPosition>__0)
	//			{
	//				this.$this.gameScoreText.gameObject.SetActive(false);
	//				this.$this.gameScoreCoroutine = null;
	//				this.$PC = -1;
	//				return false;
	//			}
	//			break;
	//		default:
	//			return false;
	//		}
	//		this.$this.gameScoreText.transform.localPosition = Vector3.MoveTowards(this.$this.gameScoreText.transform.localPosition, this.<targetPosition>__0, Time.deltaTime * this.<moveSpeed>__0);
	//		this.$current = new WaitForEndOfFrame();
	//		if (!this.$disposing)
	//		{
	//			this.$PC = 1;
	//		}
	//		return true;
	//	}

	//	[DebuggerHidden]
	//	public void Dispose()
	//	{
	//		this.$disposing = true;
	//		this.$PC = -1;
	//	}

	//	[DebuggerHidden]
	//	public void Reset()
	//	{
	//		throw new NotSupportedException();
	//	}
	//}

	public IEnumerator ShowGameScoreIe()
	{
		int jiBaiRenShu;

		float moveSpeed;

		Vector3 targetPosition;

		jiBaiRenShu = CommonDefine.GetLastJiBaiRenShu() + CommonDefine.RandomInt(10, 1000) + CommonDefine.currentLevel * 10;
		CommonDefine.SaveJiBaiRenShu(jiBaiRenShu);
		moveSpeed = 100f;
		targetPosition = new Vector3(0f, 200f, 0f);
		this.gameScoreText.gameObject.SetActive(true);
		this.gameScoreText.gameObject.transform.localPosition = Vector3.zero;
		this.gameScoreText.text = "击败" + jiBaiRenShu + "人";
		yield return 0;
		if (this.gameScoreText.transform.localPosition == targetPosition)
		{
			this.gameScoreText.gameObject.SetActive(false);
			this.gameScoreCoroutine = null;
		}

		yield return 0;
		this.gameScoreText.transform.localPosition = Vector3.MoveTowards(this.gameScoreText.transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
		yield return new WaitForEndOfFrame();
	}
	public GameObject nextButtonGo;

	public GameObject startButtonGo;

	public Text gameScoreText;

	public InputField retryLevelInput;

	public Text errorText;

	public GameObject tongGuanPanel;

	private Coroutine gameScoreCoroutine;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ShowPanel(bool isNextPanel)
	{
		this.errorText.gameObject.SetActive(false);
		this.retryLevelInput.gameObject.SetActive(false);
		this.nextButtonGo.SetActive(isNextPanel);
		this.startButtonGo.SetActive(!isNextPanel);
		base.gameObject.SetActive(true);
		if (isNextPanel && GameScene.gameSceneInsta.currentGameLevel == CommonDefine.currentLevel)
		{
			this.ShowGameScore();
			CommonDefine.currentLevel++;
			PlayerPrefs.SetInt("CurrentLevel3", CommonDefine.currentLevel);
		}
	}

	public void ShowTongGuanPanel()
	{
		this.tongGuanPanel.SetActive(true);
		this.nextButtonGo.SetActive(false);
		this.startButtonGo.SetActive(true);
		base.gameObject.SetActive(true);
	}

	public void OnNextButtonClick(int p)
	{
		int num = CommonDefine.currentLevel;
		if (this.retryLevelInput.gameObject.activeSelf && this.retryLevelInput.text != string.Empty)
		{
			try
			{
				num = int.Parse(this.retryLevelInput.text);
			}
			catch (Exception)
			{
				this.errorText.gameObject.SetActive(true);
				this.errorText.text = "请正确输入关卡编号";
				return;
			}
		}
		if (num < 1 || num > CommonDefine.currentLevel)
		{
			this.errorText.gameObject.SetActive(true);
			this.errorText.text = "可选择范围为1-" + CommonDefine.currentLevel;
			return;
		}
		base.gameObject.SetActive(false);
		GameScene.gameSceneInsta.RefreshJinDu(num);
		GameScene.gameSceneInsta.SetGameStart(num);
	}

	public void OnSelectLevelClick()
	{
		this.retryLevelInput.gameObject.SetActive(true);
		if (CommonDefine.currentLevel > CommonDefine.maxLevel)
		{
			this.retryLevelInput.text = (CommonDefine.currentLevel - 1).ToString();
		}
		else
		{
			this.retryLevelInput.text = CommonDefine.currentLevel.ToString();
		}
		this.nextButtonGo.SetActive(false);
		this.startButtonGo.SetActive(true);
	}

	private void ShowGameScore()
	{
		if (this.gameScoreCoroutine != null)
		{
			base.StopCoroutine(this.gameScoreCoroutine);
		}
		this.gameScoreCoroutine = base.StartCoroutine(this.ShowGameScoreIe());
	}

}
