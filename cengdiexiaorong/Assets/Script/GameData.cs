using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {

	// 简单难度
	private Dictionary<int , LevelData> simple_level_datas;

	private Dictionary<int, LevelData> normal_level_datas;

	private Dictionary<int, LevelData> hard_level_datas;

	private Dictionary<int, LevelData> _abnormal_level_datas;

	private static GameData _instance;

	public static GameData Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = new GameData();
			}
			return _instance;
		}
	}

	public void Init()
	{
		this.simple_level_datas = new Dictionary<int, LevelData>();
		this.normal_level_datas = new Dictionary<int, LevelData>();
		this.hard_level_datas = new Dictionary<int, LevelData>();
		this._abnormal_level_datas = new Dictionary<int, LevelData>();
		_Init();
	}

	private string[] _InitLevel()
	{
		TextAsset map = Resources.Load("gamedatas/level") as TextAsset;
		string mapText = map.text;
		string[] lines = mapText.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
		return lines;
	}

	private void _Init()
	{

		//List<string> list = new List<string>();
		//list.Add("1|0|0,0,0,20,30;1,0,0,20,30;2,0,0,20,30;7,0,0,20,30");
		//list.Add("2|0;0,-100,100,-160,160;0,80,-140,140,160;3,0,0,-160,-140;7,0,0,140,-140");
		var levels = _InitLevel();
		for (int i = 0; i < levels.Length; i++)
		{
			char[] split = { ',',';' ,'|'};
			string[] array = levels[i].ToString().Split(split);
			LevelData level_data = new LevelData();
			level_data.CurrentLevel = int.Parse(array[0]);
			level_data.Level_Difficulty = (LevelDifficulty) int.Parse(array[1]);
			level_data.ImageDatas = new List<ImageData>();
			for (int j = 2; j < array.Length; j += 5)
			{
				ImageData image_data = new ImageData();
				image_data.ImageType = (enBaseImageType)int.Parse(array[j]);
				image_data.ImagePosition = new Vector3(float.Parse(array[j + 1]), float.Parse(array[j + 2]), 0f);
				image_data.OperationalImagePosition = new Vector3(float.Parse(array[j + 3]), float.Parse(array[j + 4]));
				level_data.ImageDatas.Add(image_data);
			}
			switch (level_data.Level_Difficulty)
			{
				case LevelDifficulty.Simple:
					if (!simple_level_datas.ContainsKey(level_data.CurrentLevel))
					{
						simple_level_datas.Add(level_data.CurrentLevel, level_data);
					}else
					{
						Debug.LogError("关卡重复添加 level="+level_data.CurrentLevel);
					}
					
					break;
				case LevelDifficulty.Normal:
					normal_level_datas.Add(level_data.CurrentLevel, level_data);
					break;
				case LevelDifficulty.Hard:
					hard_level_datas.Add(level_data.CurrentLevel, level_data);
					break;
				case LevelDifficulty.Abnormal:
					_abnormal_level_datas.Add(level_data.CurrentLevel, level_data);
					break;
			}
		}
	}

	public LevelData GetLevelData(int level , LevelDifficulty level_difficult)
	{
		Dictionary<int, LevelData> datas = null;
		switch (level_difficult)
		{
			case LevelDifficulty.Simple:
				datas = simple_level_datas;
				break;
			case LevelDifficulty.Normal:
				datas = normal_level_datas;
				break;
			case LevelDifficulty.Hard:
				datas = hard_level_datas;
				break;
			case LevelDifficulty.Abnormal:
				datas = _abnormal_level_datas;
				break;
		}
		if (datas.ContainsKey(level))
		{
			return datas[level];
		}
		return null;
	}

}
