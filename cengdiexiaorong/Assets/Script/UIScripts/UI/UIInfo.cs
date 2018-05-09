using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class UIInfo  {

	public  UIType Type;

	public UIHierarchyType UI_Hierarchy_Type;

	public string Path;

	public UIInfo(UIType type , UIHierarchyType ui_hierarchy_type ,string path)
	{
		this.Type = type;
		this.UI_Hierarchy_Type = ui_hierarchy_type;
		this.Path = path;
	}
}

public enum UIType
{
	Login,
	Buy,
}

public enum UIHierarchyType
{
	Normal,
	Dialog,
}