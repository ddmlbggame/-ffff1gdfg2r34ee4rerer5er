using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPanel : MonoBehaviour
{
	public Transform baseImageGrid;

	public enBaseImageType ImageType;

	public LevelDifficulty Level_Difficulty;

	public int Level;

	public Dictionary<ImageControl, ImageControl> images;
	public void OnSave()
	{
		string text;
		text = this.Level + "|" + (int)this.Level_Difficulty;
		for (int i = 0; i < GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Count; i++)
		{
			
			ImageControl imageControl = GameScene.gameSceneInsta.Operational_Figure_Control.imageList[i];
			if (image_datas.ContainsKey(imageControl))
			{
				text += ";" + image_datas[imageControl].index + "," + image_datas[imageControl].pos.x + "," + image_datas[imageControl].pos.y;
				Vector3 localPosition2 = imageControl.transform.localPosition;
				Vector3 vector = localPosition2;
				text += "," + vector.x + "," + vector.y;
			}else
			{
				Debug.LogError("编辑是不是出问题了");
			}
			
		}
		CommonDefine.WriteFile(text, null);
	}

	private Dictionary<ImageControl, imagedata> image_datas = new Dictionary<ImageControl, imagedata>();
	public void OnSaveData()
	{
		image_datas.Clear();
		for (int i = 0; i < GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Count; i++)
		{
			imagedata data = new imagedata();
			ImageControl imageControl = GameScene.gameSceneInsta.Operational_Figure_Control.imageList[i];
			data.index = imageControl.imageIndex;
			data.pos = imageControl.transform.localPosition;
			image_datas.Add(imageControl,data);
		}
	}
	//public void ShowBaseImage()
	//{
	//	int num = -2147483648;
	//	int num2 = -2147483648;
	//	foreach (KeyValuePair<int, BaseImage> current in CommonDefine.baseImages)
	//	{
	//		if (current.Value.imageWidth > num)
	//		{
	//			num = current.Value.imageWidth;
	//		}
	//		if (current.Value.imageHeight > num2)
	//		{
	//			num2 = current.Value.imageHeight;
	//		}
	//	}
	//	int num3 = num2 + 40;
	//	int num4 = num + 40;
	//	float num5 = 0f - (float)num4 * 1.5f;
	//	float num6 = (float)(240 - num2 / 2 - 20);
	//	foreach (KeyValuePair<int, BaseImage> current2 in CommonDefine.baseImages)
	//	{
	//		Texture2D texture2D = CommonDefine.CreateTexture(current2.Value);
	//		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.preKuaiImage, this.baseImageGrid);
	//		gameObject.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), Vector2.zero);
	//		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)texture2D.width, (float)texture2D.height);
	//		Button button = gameObject.AddComponent<Button>();
	//		int baseImageIndex = current2.Key;
	//		button.onClick.AddListener(delegate
	//		{
	//			GameScene.gameSceneInsta.CreateImageOnCaoZuoPan(baseImageIndex);
	//		});
	//		UnityEngine.Object.Destroy(gameObject.GetComponent<ImageControl>());
	//		int num7 = current2.Key / 4;
	//		int num8 = current2.Key % 4;
	//		gameObject.transform.localPosition = new Vector3(num5 + (float)(num8 * num4), num6 - (float)(num7 * num3), 0f);
	//	}
	//}

	public void AddOneTypeImage()
	{
		GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Clear();
		foreach (var item in this.images)
		{
			GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Add(item.Value);
		}
		int num = GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Count / 2;
		int num2 = GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Count % 2;
		Vector2 position = new Vector2((float)(-60 + 120 * num2), (float)(-80 - num * 120));
		var image_control = this._CreateImageOnCaoZuoPan((int)this.ImageType , Vector2.zero);
		this.images.Add(image_control, image_control);
	}

	public void RemoveSelectedImage(ImageControl image_control)
	{
		GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Clear();
		if (this.images.ContainsKey(image_control))
		{
			this.images.Remove(image_control);
		}
		GameObject.DestroyImmediate(image_control.gameObject);
		foreach (var item in this.images)
		{
			GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Add(item.Value);
		}
		GameScene.gameSceneInsta.Operational_Figure_Control.DoGame();
	}

	private ImageControl _CreateImageOnCaoZuoPan(int baseImageIndex ,Vector2 pos)
	{
		return GameScene.gameSceneInsta.Operational_Figure_Control.CreateBaseImage(baseImageIndex, pos, false);
	}

	public void OnInit()
	{
		if(this.images == null)
		{
			this.images = new Dictionary<ImageControl, ImageControl>();
		}
		foreach (ImageControl current in GameScene.gameSceneInsta.Operational_Figure_Control.imageList)
		{
			UnityEngine.Object.DestroyImmediate(current.gameObject);
		}
		GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Clear();
		GameScene.gameSceneInsta.Operational_Figure_Control.DoGame();
	}

	public struct imagedata
	{
		public int index;
		public Vector2 pos;
		public Vector2 opratepos;
	}
}
