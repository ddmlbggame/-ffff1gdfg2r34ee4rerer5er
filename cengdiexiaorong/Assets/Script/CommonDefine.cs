using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class CommonDefine
{
	public const bool isEditMode = false;

	public static int DotDistance = 30;

	public const int kuaiSize = 120;

	public const int kuaiBianSize = 0;

	public static int Operational_Figure_Length = 750;

	public static Dictionary<int, Dictionary<int, Vector3>> gameLevelPostions = new Dictionary<int, Dictionary<int, Vector3>>();

	public static Dictionary<int, BaseImage> baseImages = new Dictionary<int, BaseImage>();

	public static int maxLevel = 0;

	public const char splitChar = ',';

	public const string showTextureGoName = "showTextureGo";

	public static int currentLevel;

	public const string Current_Level_Name = "CurrentLeveL";
	public static void InitGameData()
	{
		if (PlayerPrefs.HasKey(Current_Level_Name))
		{
			CommonDefine.currentLevel = PlayerPrefs.GetInt(Current_Level_Name);
		}
		else
		{
			CommonDefine.currentLevel = 1;
		}
		CommonDefine.baseImages.Add((int)enBaseImageType.ZhengFangXing, new BaseImage(enBaseImageType.ZhengFangXing, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize));
		CommonDefine.baseImages.Add((int)enBaseImageType.SanJiaoXing1, new BaseImage(enBaseImageType.SanJiaoXing1, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.SanJiaoXing3, new BaseImage(enBaseImageType.SanJiaoXing3, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.SanJiaoXing2, new BaseImage(enBaseImageType.SanJiaoXing2, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.SanJiaoXing4, new BaseImage(enBaseImageType.SanJiaoXing4, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.LingXing, new BaseImage(enBaseImageType.LingXing, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.ChangFangXing1, new BaseImage(enBaseImageType.ChangFangXing1, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.ChangFangXing2, new BaseImage(enBaseImageType.ChangFangXing2, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.ChangFangXingLeft1, new BaseImage(enBaseImageType.ChangFangXingLeft1, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.ChangFangXingRight1, new BaseImage(enBaseImageType.ChangFangXingRight1, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonDefine.baseImages.Add((int)enBaseImageType.BigZhengFangXing, new BaseImage(enBaseImageType.BigZhengFangXing, kuaiSize * 2 + kuaiBianSize * 2, kuaiSize * 2 + kuaiBianSize * 2));
	}

	public static Texture2D CreateTexture(BaseImage bi)
	{
		Texture2D texture2D = new Texture2D(bi.imageWidth, bi.imageHeight);
		texture2D.SetPixels(CommonDefine.CreateBaseImage(texture2D.width, texture2D.height, bi.baseImageType));
		texture2D.Apply();
		return texture2D;
	}

	public static int GetLastJiBaiRenShu()
	{
		if (PlayerPrefs.HasKey("lastJiBaiRenShuKey"))
		{
			return PlayerPrefs.GetInt("lastJiBaiRenShuKey");
		}
		return 0;
	}

	public static void SaveJiBaiRenShu(int jiBaiRenShu)
	{
		PlayerPrefs.SetInt("lastJiBaiRenShuKey", jiBaiRenShu);
	}

	public static int GetRandomInt()
	{
		if (PlayerPrefs.HasKey("randomIntKey"))
		{
			return PlayerPrefs.GetInt("randomIntKey");
		}
		int num = CommonDefine.RandomInt(100000, 999999);
		PlayerPrefs.SetInt("randomIntKey", num);
		PlayerPrefs.Save();
		return num;
	}


	public static string MD5Code(string text)
	{
		MD5 mD = new MD5CryptoServiceProvider();
		byte[] value = mD.ComputeHash(Encoding.UTF8.GetBytes(text));
		return BitConverter.ToString(value).Replace("-", string.Empty);
	}

	public static void WriteFile(string text, string path = null)
	{
		if (path == null)
		{
			int num = 1;
			while (true)
			{
				path = Environment.CurrentDirectory + "/Assets/Resources/gamedatas/" + num.ToString() + ".txt";
				if (!File.Exists(path))
				{
					break;
				}
				num++;
			}
		}
		else
		{
			path = Environment.CurrentDirectory + "/Assets/Resources/gamedatas/" + path + ".txt";
		}
		FileStream fileStream = new FileStream(path, FileMode.Create);
		StreamWriter streamWriter = new StreamWriter(fileStream);
		streamWriter.WriteLine(text);
		streamWriter.Flush();
		streamWriter.Close();
		fileStream.Close();
	}

	public static Color[] CreateBaseImage(int imageWidth, int imageHeight, enBaseImageType baseIMageType)
	{
		Color[] array = new Color[imageWidth * imageHeight];
		for (int i = 0; i < imageWidth; i++)
		{
			for (int j = 0; j < imageHeight; j++)
			{
				int num = j * imageWidth + i;
				if (CommonDefine.IsShowPoint(imageWidth, imageHeight, i, j, baseIMageType))
				{
					array[num] = new Color(0f, 0f, 0f, 1f);
				}
				else
				{
					array[num] = new Color(0f, 0f, 0f, 0f);
				}
			}
		}
		return array;
	}

	public static bool IsShowPoint(int imageWidth, int imageHeight, int x, int y, enBaseImageType imageType)
	{
		int num = y * imageWidth + x;
		if (x >= imageWidth - kuaiBianSize || y >= imageHeight - kuaiBianSize || x < kuaiBianSize || y < kuaiBianSize)
		{
			return false;
		}
		switch (imageType)
		{
			case enBaseImageType.ZhengFangXing:
			case enBaseImageType.BigZhengFangXing:
				return true;
			case enBaseImageType.SanJiaoXing1:
				if (y >= x)
				{
					return true;
				}
				break;
			case enBaseImageType.SanJiaoXing2:
				if (y < x)
				{
					return true;
				}
				break;
			case enBaseImageType.SanJiaoXing3:
				if (y <= -x + imageWidth)
				{
					return true;
				}
				break;
			case enBaseImageType.SanJiaoXing4:
				if (y > -x + imageWidth)
				{
					return true;
				}
				break;
			case enBaseImageType.ChangFangXing1:
				if (y > imageHeight / 4 && y < 3 * imageHeight / 4 - 1)
				{
					return true;
				}
				break;
			case enBaseImageType.ChangFangXing2:
				if (x > imageWidth / 4 && x < 3 * imageWidth / 4 - 1)
				{
					return true;
				}
				break;
			case enBaseImageType.ChangFangXingLeft1:
				if (x <imageWidth/2)
				{
					return true;
				}
				break;
			case enBaseImageType.ChangFangXingRight1:
				if (x > imageWidth / 2)
				{
					return true;
				}
				break;
			case enBaseImageType.LingXing:
				if ((x <= imageWidth / 2 && y <= imageHeight / 2 && x > imageWidth / 2 - y + 2) || (x >= imageWidth / 2 && y <= imageHeight / 2 && x <= y + imageWidth / 2 - 2) || (x <= imageWidth / 2 && y >= imageHeight / 2 && x > y - imageWidth / 2 + 2) || (x >= imageWidth / 2 && y >= imageHeight / 2 && x <= 3 * imageWidth / 2 - y - 2))
				{
					return true;
				}
				break;
		}
		return false;
	}

	public static int RandomInt(int min, int max)
	{
		System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
		return random.Next(min, max + 1);
	}
}
