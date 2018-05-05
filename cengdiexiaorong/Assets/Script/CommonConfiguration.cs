using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonConfiguration
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
			CommonConfiguration.currentLevel = PlayerPrefs.GetInt(Current_Level_Name);
		}
		else
		{
			CommonConfiguration.currentLevel = 1;
		}
		CommonConfiguration.baseImages.Add((int)ImageType.ZhengFangXing, new BaseImage(ImageType.ZhengFangXing, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize));
		CommonConfiguration.baseImages.Add((int)ImageType.SanJiaoXing1, new BaseImage(ImageType.SanJiaoXing1, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.SanJiaoXing3, new BaseImage(ImageType.SanJiaoXing3, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.SanJiaoXing2, new BaseImage(ImageType.SanJiaoXing2, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.SanJiaoXing4, new BaseImage(ImageType.SanJiaoXing4, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.LingXing, new BaseImage(ImageType.LingXing, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.ChangFangXing1, new BaseImage(ImageType.ChangFangXing1, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.ChangFangXing2, new BaseImage(ImageType.ChangFangXing2, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.ChangFangXingLeft1, new BaseImage(ImageType.ChangFangXingLeft1, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.ChangFangXingRight1, new BaseImage(ImageType.ChangFangXingRight1, kuaiSize + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.BigZhengFangXing, new BaseImage(ImageType.BigZhengFangXing, kuaiSize * 2 + kuaiBianSize * 2, kuaiSize * 2 + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.Parallelogram1, new BaseImage(ImageType.Parallelogram1, 4*kuaiSize/2 + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.Parallelogram2, new BaseImage(ImageType.Parallelogram2, 4 * kuaiSize / 2 + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.Parallelogram3, new BaseImage(ImageType.Parallelogram3, kuaiSize + kuaiBianSize * 2, 4 * kuaiSize / 2 + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.Parallelogram4, new BaseImage(ImageType.Parallelogram4, kuaiSize + kuaiBianSize * 2, 4 * kuaiSize / 2 + kuaiBianSize * 2));

		CommonConfiguration.baseImages.Add((int)ImageType.ParallelogramLong1, new BaseImage(ImageType.ParallelogramLong1, 5 * kuaiSize / 2 + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.ParallelogramLong2, new BaseImage(ImageType.ParallelogramLong2, 5 * kuaiSize / 2 + kuaiBianSize * 2, kuaiSize + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.ParallelogramLong3, new BaseImage(ImageType.ParallelogramLong3, kuaiSize + kuaiBianSize * 2, 5*kuaiSize/2 + kuaiBianSize * 2));
		CommonConfiguration.baseImages.Add((int)ImageType.ParallelogramLong4, new BaseImage(ImageType.ParallelogramLong4, kuaiSize  + kuaiBianSize * 2, 5*kuaiSize/2 + kuaiBianSize * 2));

	}

	public static Texture2D CreateTexture(BaseImage bi)
	{
		Texture2D texture2D = new Texture2D(bi.imageWidth, bi.imageHeight);
		texture2D.SetPixels(CommonConfiguration.CreateBaseImage(texture2D.width, texture2D.height, bi.baseImageType));
		texture2D.Apply();
		return texture2D;
	}


	public static Color[] CreateBaseImage(int imageWidth, int imageHeight, ImageType baseIMageType)
	{
		Color[] array = new Color[imageWidth * imageHeight];
		for (int i = 0; i < imageWidth; i++)
		{
			for (int j = 0; j < imageHeight; j++)
			{
				int num = j * imageWidth + i;
				if (CommonConfiguration.IsShowPoint(imageWidth, imageHeight, i, j, baseIMageType))
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

	public static bool IsShowPoint(int imageWidth, int imageHeight, int x, int y, ImageType imageType)
	{
		int num = y * imageWidth + x;
		if (x >= imageWidth - kuaiBianSize || y >= imageHeight - kuaiBianSize || x < kuaiBianSize || y < kuaiBianSize)
		{
			return false;
		}
		switch (imageType)
		{
			case ImageType.ZhengFangXing:
			case ImageType.BigZhengFangXing:
				return true;
			case ImageType.SanJiaoXing1:
				if (y >= x)
				{
					return true;
				}
				break;
			case ImageType.SanJiaoXing2:
				if (y < x)
				{
					return true;
				}
				break;
			case ImageType.SanJiaoXing3:
				if (y <= -x + imageWidth)
				{
					return true;
				}
				break;
			case ImageType.SanJiaoXing4:
				if (y > -x + imageWidth)
				{
					return true;
				}
				break;
			case ImageType.ChangFangXing1:
				if (y > imageHeight / 4 && y < 3 * imageHeight / 4 - 1)
				{
					return true;
				}
				break;
			case ImageType.ChangFangXing2:
				if (x > imageWidth / 4 && x < 3 * imageWidth / 4 - 1)
				{
					return true;
				}
				break;
			case ImageType.ChangFangXingLeft1:
				if (x <imageWidth/2)
				{
					return true;
				}
				break;
			case ImageType.ChangFangXingRight1:
				if (x > imageWidth / 2)
				{
					return true;
				}
				break;
			case ImageType.LingXing:
				if ((x <= imageWidth / 2 && y <= imageHeight / 2 && x > imageWidth / 2 - y + 2) || (x >= imageWidth / 2 && y <= imageHeight / 2 && x <= y + imageWidth / 2 - 2) || (x <= imageWidth / 2 && y >= imageHeight / 2 && x > y - imageWidth / 2 + 2) || (x >= imageWidth / 2 && y >= imageHeight / 2 && x <= 3 * imageWidth / 2 - y - 2))
				{
					return true;
				}
				break;
			case ImageType.Parallelogram1:
				if(x>imageWidth/4 && x < (3*imageWidth / 4)){
					return true;
				}
				if(x>y/2 && x<= imageWidth / 4)
				{
					return true;
				}
				if (x- (3 * imageWidth / 4) < y/2 && x >= (3 * imageWidth / 4))
				{
					return true;
				}
				break;
			case ImageType.Parallelogram2:
				if (x > imageWidth / 4 && x < (3 * imageWidth / 4))
				{
					return true;
				}
				if (x>-y/2+ imageWidth / 4 && x <= imageWidth / 4)
				{
					return true;
				}
				if (x < -y / 2 +imageWidth && x >= (3 * imageWidth / 4))
				{
					return true;
				}
				break;

			case ImageType.Parallelogram3:
				if(y>=imageHeight/4 && y <= 3 * imageHeight / 4)
				{
					return true;
				}
				if (y > x / 2 && y <= imageHeight / 4)
				{
					return true;
				}
				if (y - (3 * imageHeight / 4) < x / 2 && y >= (3 * imageHeight / 4))
				{
					return true;
				}
				break;

			case ImageType.Parallelogram4:
				if (y > imageHeight / 4 && y < (3 * imageHeight / 4))
				{
					return true;
				}
				if (y>-x/2 + imageHeight / 4 && y <= imageHeight / 4)
				{
					return true;
				}
				if(y<-x/2 +imageHeight && y >= (3 * imageHeight / 4))
				{
					return true;
				}
				break;

			case ImageType.ParallelogramLong1:
				if (x > 2*imageWidth / 5 && x < (3 * imageWidth / 5))
				{
					return true;
				}
				if (x > y && x <= 2 * imageWidth / 5)
				{
					return true;
				}
				if (x - (3 * imageWidth / 5) < y && x >= (3 * imageWidth / 5))
				{
					return true;
				}
				break;

			case ImageType.ParallelogramLong2:
				if (x > 2 * imageWidth / 5 && x < (3 * imageWidth / 5))
				{
					return true;
				}
				if (x > -y + 2 * imageWidth / 5 && x <= 2 * imageWidth / 5)
				{
					return true;
				}
				if (x < -y + imageWidth && x >= (3 * imageWidth / 5))
				{
					return true;
				}
				break;

			case ImageType.ParallelogramLong3:
				if (y >= 2 * imageHeight / 5 && y <= (3 * imageHeight / 5))
				{
					return true;
				}
				if (y > x && y <= 2 * imageHeight / 5)
				{
					return true;
				}
				if (y - (3 * imageHeight / 5) < x && y >= (3 * imageHeight / 5))
				{
					return true;
				}
				break;

			case ImageType.ParallelogramLong4:
				if (y > 2 * imageHeight / 5 && y < (3 * imageHeight / 5))
				{
					return true;
				}


				if (y > -x + 2 * imageHeight / 5 && y <= 2 * imageHeight / 5)
				{
					return true;
				}
				if (y < -x + imageHeight && y >= (3 * imageHeight / 5))
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
