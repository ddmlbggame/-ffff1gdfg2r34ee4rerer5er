using System;

public class BaseImage
{
	public enBaseImageType baseImageType;

	public int imageWidth;

	public int imageHeight;

	public BaseImage(enBaseImageType baseImageType, int imageWidth, int imageHeight)
	{
		this.baseImageType = baseImageType;
		this.imageWidth = imageWidth;
		this.imageHeight = imageHeight;
	}
}
