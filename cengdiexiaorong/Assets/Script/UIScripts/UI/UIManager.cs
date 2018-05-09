using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class UIManager {

	private static UIManager _instance;

	public static UIManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = new UIManager();
			}
			return _instance;
		}
	}

	private GameObject _canvas;

	public Canvas Dialog;

	public Canvas Normal;

	private Stack<UIBase> ui_queue = new Stack<UIBase>();

	private Dictionary<UIInfo, UIBase> ui_dictionary = new Dictionary<UIInfo, UIBase>();
	
	private Dictionary<UIInfo, UIBase> ui_active = new Dictionary<UIInfo, UIBase>();

	private UIBase _current_ui = null;

	public void _Init(Transform parent)
	{
		var canvas = Resources.Load("UI/UICanvas");
		this._canvas = GameObject.Instantiate(canvas) as GameObject;
		this._canvas.transform.SetParent(parent);
		this._canvas.transform.localPosition = Vector3.zero;
		this._canvas.transform.localScale = Vector3.one;
		this._canvas.transform.localRotation = Quaternion.identity;
		this.Dialog = this._canvas.transform.FindChild("Dialog").GetComponent<Canvas>();
		this.Normal = this._canvas.transform.FindChild("Normal").GetComponent<Canvas>();
	}

	public void PushShow(UIInfo info , bool is_push = false)
	{
		UIBase ui = null;
		if(this.ui_dictionary.ContainsKey(info))
		{
			ui = this.ui_dictionary[info];
		}
		else
		{
			var ui_obj = Resources.Load(info.Path);
			if (ui_obj != null)
			{
				GameObject ui_game_obj = GameObject.Instantiate(ui_obj) as GameObject;
				ui = ui_game_obj.GetComponent<UIBase>();
			}else
			{
				Debug.LogError("can't load ui path =" + info.Path);
			}
	
		}
		this._current_ui = ui;
		if (is_push)
		{
			ui_queue.Push(this._current_ui);
			UIBase peek_ui = null;
			if (this.ui_queue.Count > 0)
			{
				peek_ui = this.ui_queue.Peek();
				peek_ui.Hide();
			}
		}
		this._current_ui.Show();
	}

	public void PopShow()
	{
		if (this.ui_queue.Count > 0)
		{
			UIBase ui = this.ui_queue.Pop();
			this._current_ui = ui;
			this._current_ui.Show();
		}
		else
		{
			Debug.LogError("current ui stack count = 0");
		}
	}

	public void Hide(UIInfo info)
	{
		UIBase ui = null;
		if(this.ui_dictionary.TryGetValue(info ,out ui))
		{
			ui.Hide();
		}
	}
}
