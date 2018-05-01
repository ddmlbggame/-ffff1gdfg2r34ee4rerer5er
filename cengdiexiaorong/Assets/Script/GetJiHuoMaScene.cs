using System;
using UnityEngine;
using UnityEngine.UI;

public class GetJiHuoMaScene : MonoBehaviour
{
	public InputField randomIntInput;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnShengChengClick()
	{
		string message = CommonDefine.MD5Code(this.randomIntInput.text + "cdxr");
		Debug.Log(message);
	}
}
