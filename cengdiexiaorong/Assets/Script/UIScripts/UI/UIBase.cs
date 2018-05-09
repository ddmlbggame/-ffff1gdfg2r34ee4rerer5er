using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour {

	//public static UIInfo Info = new UIInfo(UIType.UIMain , UIHierarchyType.Normal );

	public virtual void OnEnable () {
		
	}

	public virtual void OnDisable()
	{

	}

	public virtual void Show()
	{
		this.gameObject.SetActive(true);
	}

	public virtual void Hide()
	{
		this.gameObject.SetActive(false);
	}
}
