using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPanel : MonoBehaviour
{
	public Transform baseImageGrid;

	public GameObject preKuaiImage;

	private void Start()
	{
		this.ShowBaseImage();
	}

	public void OnSaveButtonClick()
	{
		string text = string.Empty;
		Vector3 localPosition = GameScene.gameSceneInsta.Operational_Figure_Control.imageList[0].transform.localPosition;
		for (int i = 0; i < GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Count; i++)
		{
			ImageControl imageControl = GameScene.gameSceneInsta.Operational_Figure_Control.imageList[i];
			Vector3 localPosition2 = imageControl.transform.localPosition;
			Vector3 vector = localPosition - localPosition2;
			if (text == string.Empty)
			{
				text = string.Concat(new object[]
				{
					imageControl.imageIndex.ToString(),
					',',
					vector.x.ToString(),
					',',
					vector.y
				});
			}
			else
			{
				text = string.Concat(new object[]
				{
					text,
					',',
					imageControl.imageIndex.ToString(),
					',',
					vector.x.ToString(),
					',',
					vector.y
				});
			}
		}
		CommonDefine.WriteFile(text, null);
	}

	public void ShowBaseImage()
	{
		int num = -2147483648;
		int num2 = -2147483648;
		foreach (KeyValuePair<int, BaseImage> current in CommonDefine.baseImages)
		{
			if (current.Value.imageWidth > num)
			{
				num = current.Value.imageWidth;
			}
			if (current.Value.imageHeight > num2)
			{
				num2 = current.Value.imageHeight;
			}
		}
		int num3 = num2 + 40;
		int num4 = num + 40;
		float num5 = 0f - (float)num4 * 1.5f;
		float num6 = (float)(240 - num2 / 2 - 20);
		foreach (KeyValuePair<int, BaseImage> current2 in CommonDefine.baseImages)
		{
			Texture2D texture2D = CommonDefine.CreateTexture(current2.Value);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.preKuaiImage, this.baseImageGrid);
			gameObject.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), Vector2.zero);
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)texture2D.width, (float)texture2D.height);
			Button button = gameObject.AddComponent<Button>();
			int baseImageIndex = current2.Key;
			button.onClick.AddListener(delegate
			{
				GameScene.gameSceneInsta.CreateImageOnCaoZuoPan(baseImageIndex);
			});
			UnityEngine.Object.Destroy(gameObject.GetComponent<ImageControl>());
			int num7 = current2.Key / 4;
			int num8 = current2.Key % 4;
			gameObject.transform.localPosition = new Vector3(num5 + (float)(num8 * num4), num6 - (float)(num7 * num3), 0f);
		}
	}

	public void OnInitClick()
	{
		foreach (ImageControl current in GameScene.gameSceneInsta.Operational_Figure_Control.imageList)
		{
			UnityEngine.Object.Destroy(current.gameObject);
		}
		GameScene.gameSceneInsta.Operational_Figure_Control.imageList.Clear();
		GameScene.gameSceneInsta.Operational_Figure_Control.DoGame();
	}
}
