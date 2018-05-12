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

}
