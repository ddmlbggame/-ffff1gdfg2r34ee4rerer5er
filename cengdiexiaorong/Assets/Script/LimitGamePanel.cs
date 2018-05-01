using System;
using UnityEngine;
using UnityEngine.UI;

public class LimitGamePanel : MonoBehaviour
{
	public Text randomIntText;

	public InputField jiHuoMaInput;

	public GameObject jhmErrorText;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		this.randomIntText.text = "随机码: " + CommonDefine.GetRandomInt();
	}

	public void OnJiHuoClick()
	{
		if (CommonDefine.CheckJiHuoMa(this.jiHuoMaInput.text))
		{
			base.gameObject.SetActive(false);
			GameScene.gameSceneInsta.startPanel.ShowPanel(false);
		}
		else
		{
			this.jhmErrorText.SetActive(true);
		}
	}
}
