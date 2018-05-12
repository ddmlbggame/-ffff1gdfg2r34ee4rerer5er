using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : UIBase {

	public static UIInfo Info = new UIInfo(UIType.Level, UIHierarchyType.Normal, "UI_Level");

	public ToggleGroup toggle_group;

	public Toggle[] toggles;

	public Transform _level_parent;

	public GameObject _back;

	private List<UILevelItem> level_items = new List<UILevelItem>();

	public override void OnEnable()
	{
		base.OnEnable();
		EventTriggerListener.Get(this._back).onClick = this.Back;
		this.toggle_group.SetAllTogglesOff();
		toggles[0].onValueChanged.AddListener(this.OnClickSimpleToggle);
		toggles[1].onValueChanged.AddListener(this.OnClickNormalToggle);
		toggles[2].onValueChanged.AddListener(this.OnClickHardToggle);
		toggles[3].onValueChanged.AddListener(this.OnClickAbnormalToggle);
		this.toggles[0].isOn = true;
	}

	public void Refresh(LevelDifficulty level_difficulty)
	{
		var datas = GameControl.Instance.game_data.GetLevelDatas(level_difficulty);
		int child_count = this._level_parent.transform.childCount;
		if (child_count < datas.Count)
		{
			for (int i = 0; i < datas.Count - child_count; i++)
			{
				UILevelItem item = UILevelItem.Create(this._level_parent);
				this.level_items.Add(item);
			}
			
		}

		for (int i = 0; i < this.level_items.Count; i++)
		{
			this.level_items[i].gameObject.SetActive(false);
		}
		int index = 0;
		foreach (var item in datas)
		{
			this.level_items[index].Init(item.Value);
			this.level_items[index].gameObject.SetActive(true);
			index++;
		}
	}
	public override void OnDisable()
	{
		base.OnDisable();
	}

	private void Back(GameObject obj)
	{
		UIManager.Instance.PopShow();
	}

	private void OnClickSimpleToggle(bool state)
	{
		if (state)
		{
			this.Refresh(LevelDifficulty.Simple);
		}
	}

	private void OnClickNormalToggle(bool state)
	{
		if (state)
		{
			this.Refresh(LevelDifficulty.Normal);
		}
	}

	private void OnClickHardToggle(bool state)
	{
		if (state)
		{
			this.Refresh(LevelDifficulty.Hard);
		}
	}

	private void OnClickAbnormalToggle(bool state)
	{
		if (state)
		{
			this.Refresh(LevelDifficulty.Abnormal);
		}
	}

}
