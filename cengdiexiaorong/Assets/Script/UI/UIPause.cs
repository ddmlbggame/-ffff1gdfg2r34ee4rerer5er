using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : UIBase {

	public static UIInfo Info = new UIInfo(UIType.Pause, UIHierarchyType.Normal, "UI_Pause");

	public GameObject close;

	public GameObject sound;

	public GameObject music;

	public GameObject advertising;

	public GameObject soundoff;

	public GameObject soundon;

	public GameObject musicoff;

	public GameObject musicon;

	public override void OnEnable()
	{
		EventTriggerListener.Get(this.close).onClick = this._OnClose;
		EventTriggerListener.Get(this.sound).onClick = this._OnSound;
		EventTriggerListener.Get(this.music).onClick = this._OnMusic;
		EventTriggerListener.Get(this.advertising).onClick = this._OnAdvertising;
		this._Refresh();
	}

	private void _Refresh()
	{
		bool mute = FSoundManager.IsSoundMute;
		soundoff.SetActive(mute);
		soundon.SetActive(!mute);

		bool music_mute = FSoundManager.IsMusicMute;
		musicoff.SetActive(music_mute);
		musicon.SetActive(!music_mute);
	}
	private void _OnClose(GameObject obj)
	{
		UIManager.Instance.Hide(Info);
	}
	private void _OnAdvertising(GameObject obj)
	{
		
	}

	private void _OnSound(GameObject obj)
	{
		bool mute = FSoundManager.IsSoundMute;
		FSoundManager.IsSoundMute = !mute;
		soundoff.SetActive(!mute);
		soundon.SetActive(mute);
	}

	private void _OnMusic(GameObject obj)
	{
		bool mute = FSoundManager.IsMusicMute;
		musicoff.SetActive(!mute);
		musicon.SetActive(mute);
		FSoundManager.IsMusicMute = !mute;
	}
	
}
