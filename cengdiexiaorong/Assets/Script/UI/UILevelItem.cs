using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILevelItem : MonoBehaviour {

	public Text Level;

	public GameObject Lock;

	public GameObject Select;

	private LevelData level_data;

	public void OnEnable()
	{
		EventTriggerListener.Get(this.gameObject).onClick = this._OnClick;
	}

	private void _OnClick(GameObject obj)
	{
		if(this.level_data!=null && GameData.GetPassedLevel(level_data.Level_Difficulty)>= level_data.CurrentLevel)
		{
			GameControl.Instance.PlayGame(GameControl.Instance.game_data._current_game_type, this.level_data.Level_Difficulty, this.level_data.CurrentLevel);
		}
	}

	public void Init(LevelData data)
	{
		this.level_data = data;
		if (data != null)
		{
			this.Level.text = data.CurrentLevel.ToString();
			int passed_level = GameData.GetPassedLevel(data.Level_Difficulty);
			this.Lock.SetActive(passed_level< data.CurrentLevel);
			this.Select.SetActive(data.CurrentLevel == passed_level);
		}
	}

	public static UILevelItem Create(Transform parent)
	{
		var obj = Resources.Load("UI/UILevelItem");
		GameObject item = GameObject.Instantiate(obj) as GameObject;
		item.transform.SetParent(parent);
		item.transform.localPosition = Vector3.zero;
		item.transform.localScale = Vector3.one;
		item.transform.localRotation = Quaternion.identity;
		return item.GetComponent<UILevelItem>();
	}
}
