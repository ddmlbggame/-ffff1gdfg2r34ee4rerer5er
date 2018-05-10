using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : UIBase {

	public static UIInfo Info = new UIInfo(UIType.Main, UIHierarchyType.Normal, "Main");

	public override void OnEnable()
	{
		base.OnEnable();
	}

}
