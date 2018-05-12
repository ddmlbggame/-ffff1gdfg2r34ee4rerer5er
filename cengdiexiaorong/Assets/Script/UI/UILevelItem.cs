using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILevelItem : MonoBehaviour {

	public Text Level;

	public GameObject Lock;

	public GameObject Select;

	public void Init(LevelData data)
	{
		if (data != null)
		{
			this.Level.text = data.CurrentLevel.ToString();
			this.Lock.SetActive(!data.IsPassed);
			
		}
	}

}
