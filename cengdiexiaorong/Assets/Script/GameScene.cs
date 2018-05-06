using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Umeng;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
	public static GameScene gameSceneInsta;

	public AudioSource audioSource;

	public AudioClip success;

	public Canvas canvas;

	public Transform caoZuoPanTransfrom;

	public Transform shiLiTransfrom;

	public GameObject preDian;

	public DieJiaControl Operational_Figure_Control;

	public RectTransform Operational_Figure;

	public DieJiaControl Fixed_Figure_Control;

	public Text jinDuText;

	private Coroutine gameScoreCoroutine;

	private float gameStartTime;

	private bool isGamePlay;

	private ImageControl currentSelectImage;

	public bool isLimitGame = true;

	private int kaiFangLevelCount = 30;

	private void Awake()
	{
		//Analytics.StartWithAppKeyAndChannelId("5a5f086da40fa30870000130", enChannelId.TapTap.ToString());
		Operational_Figure = this.Operational_Figure_Control.GetComponent<RectTransform>();
		GameScene.gameSceneInsta = this;
		Screen.sleepTimeout = -1;
		CommonConfiguration.InitGameData();
		GameData.Instance.Init();
		this.Operational_Figure_Control.dianList = this.CreateDian(this.caoZuoPanTransfrom ,2);
		this.CreateDian(this.shiLiTransfrom,2);
		this.Fixed_Figure_Control.dianList = this.Operational_Figure_Control.dianList;
	}

	private void Start()
	{
		this.RefreshJinDu(CommonConfiguration.currentLevel);
		//this.startPanel.ShowPanel(false);
		//base.StartCoroutine(this.ShowFPS());
		GameScene.gameSceneInsta.SetGameStart(2);
	}

	private void Update()
	{
		if (this.isGamePlay)
		{
			// 如果正在提示中
			if (GameData.Instance.doing_show_tip)
			{
				return;
			}
			Profiler.BeginSample("--------------------Update");
			this.SelectDragImage();
			Profiler.EndSample();
		}
	}

	private void SelectDragImage()
	{
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Mouse0))
#else
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
#endif
		{
			Vector2 one = Vector2.one;
			Profiler.BeginSample("RectTransformUtility");
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.Operational_Figure, Input.mousePosition, this.canvas.worldCamera, out one);
			List<ImageControl> list = new List<ImageControl>();
			List<ImageControl> list2 = new List<ImageControl>();
			Profiler.EndSample();
			Profiler.BeginSample("--------------------1");
			foreach (ImageControl current in this.Operational_Figure_Control.imageList)
			{
				if (one.x > current.transform.localPosition.x - (float)current.halfWidth && one.x < current.transform.localPosition.x + (float)current.halfWidth && one.y > current.transform.localPosition.y - (float)current.halfHeight && one.y < current.transform.localPosition.y + (float)current.halfHeight)
				{
					int x = (int)((float)current.halfWidth - (current.transform.localPosition.x - one.x));
					int y = (int)((float)current.halfHeight - (current.transform.localPosition.y - one.y));
					if (CommonConfiguration.IsShowPoint(current.showImageTexture.width, current.showImageTexture.height, x, y, CommonConfiguration.baseImages[current.imageIndex].baseImageType))
					{
						list.Add(current);
					}
					else
					{
						list2.Add(current);
					}
				}
			}
			Profiler.EndSample();
			List<ImageControl> list3 /*= new List<ImageControl>()*/;
			if (list.Count != 0)
			{
				list3 = list;
			}
			else
			{
				list3 = list2;
			}
			ImageControl imageControl = null;
			int num = -2147483648;
			Profiler.BeginSample("--------------------2");
			foreach (ImageControl current2 in list3)
			{
				if (current2.transform.GetSiblingIndex() > num)
				{
					num = current2.transform.GetSiblingIndex();
					imageControl = current2;
				}
			}
			Profiler.EndSample();
			Profiler.BeginSample("--------------------3");
			if (imageControl != null)
			{
				this.currentSelectImage = imageControl;
				imageControl.OnPointerDown();
			}
			Profiler.EndSample();
		}

#if UNITY_EDITOR
		if (Input.GetKeyUp(KeyCode.Mouse0) && this.currentSelectImage != null)
#else
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && this.currentSelectImage != null)
#endif
		{
			this.currentSelectImage.OnDragEnd();
			this.currentSelectImage = null;
		}
#if UNITY_EDITOR
		if (true)
#else
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
#endif
		{
			if (this.currentSelectImage != null)
			{
				Profiler.BeginSample("-----------drag---------");
#if UNITY_EDITOR
				this.currentSelectImage.OnDragIng(Input.mousePosition);
#else
		this.currentSelectImage.OnDragIng(Input.GetTouch(0).position);
#endif
				Profiler.EndSample();
			}
		}
}

	public void SetGameStart(int currentLevel , LevelDifficulty level_difficulty = LevelDifficulty.Simple)
	{
		GameData.Instance.currentGameLevel = currentLevel;
		GameData.Instance.Current_Difficulty = level_difficulty;
		this.gameStartTime = Time.realtimeSinceStartup;
		this.isGamePlay = true;
		//foreach (ImageControl current in this.Operational_Figure_Control.imageList)
		//{
		//	UnityEngine.Object.Destroy(current.gameObject);
		//}
		//this.Operational_Figure_Control.imageList.Clear();
		//var level_data = GameData.Instance.GetLevelData(currentLevel, level_difficulty);
		//for (int i = 0; i < level_data.ImageDatas.Count; i++)
		//{
		//	this.CreateImageOnCaoZuoPan(level_data.ImageDatas[i]);
		//}
		var level_data = GameData.Instance.GetLevelData(currentLevel, level_difficulty);
		this.CreateOperationalImage(level_data);
		this.CreateImageOnShiLiPan(level_data);
	}

	private void CreateOperationalImage(LevelData level_data)
	{
		this._ResetOprationalImage();
		for (int i = 0; i < level_data.ImageDatas.Count; i++)
		{
			this.CreateImageOnCaoZuoPan(level_data.ImageDatas[i]);
		}
	}

	private void _ResetOprationalImage()
	{
		foreach (ImageControl current in this.Operational_Figure_Control.imageList)
		{
			UnityEngine.Object.Destroy(current.gameObject);
		}
		this.Operational_Figure_Control.imageList.Clear();
	}

	public void RefreshJinDu(int currentLevel)
	{
		if (currentLevel > CommonConfiguration.gameLevelPostions.Count)
		{
			this.jinDuText.text = string.Concat(new object[]
			{
				"总进度 ",
				CommonConfiguration.gameLevelPostions.Count,
				"/",
				CommonConfiguration.gameLevelPostions.Count
			});
		}
		else
		{
			this.jinDuText.text = string.Concat(new object[]
			{
				"总进度 ",
				currentLevel,
				"/",
				CommonConfiguration.gameLevelPostions.Count
			});
		}
	}

	public void CreateImageOnCaoZuoPan(ImageData image_data)
	{
		//int num = this.Operational_Figure_Control.imageList.Count / 2;
		//int num2 = this.Operational_Figure_Control.imageList.Count % 2;
		//Vector2 position = new Vector2((float)(-CommonDefine.kuaiSize + CommonDefine.kuaiSize * num2), (float)(-120 - num * 120));

		this.Operational_Figure_Control.CreateBaseImage(image_data ,(int)image_data.ImageType, image_data.OperationalImagePosition, false);
	}

	public void CreateImageOnShiLiPan(LevelData levelData)
	{
		List<GameObject> allChilds = this.GetAllChilds(this.Fixed_Figure_Control.transform);
		foreach (GameObject current in allChilds)
		{
			if (!(current.name == "showTextureGo"))
			{
				UnityEngine.Object.Destroy(current);
			}
		}
		//this.shiLiDieJiaControl.transform.localScale = Vector3.one;
		Vector3 zero = Vector3.zero;
		foreach (var item in levelData.ImageDatas)
		{
			Vector3 value = item.ImagePosition;
			Vector3 vector = value;
			this.Fixed_Figure_Control.CreateBaseImage(item ,(int)item.ImageType, vector, true);
		}
		//foreach (KeyValuePair<int, Vector3> current2 in levelData)
		//{
		//	Vector3 value = current2.Value;
		//	Vector3 vector = value;
		//	//Vector3 vector = zero - value;
		//	//vector += new Vector3(0f, this.Fixed_Figure_Control.showTextureGo.transform.localPosition.y, 0f);
		//	this.Fixed_Figure_Control.CreateBaseImage(current2.Key, vector, true);
		//}
		//this.shiLiDieJiaControl.transform.parent = this.shiLiTransfrom;
		//this.shiLiDieJiaControl.transform.localScale = Vector3.one;
		//this.shiLiDieJiaControl.transform.parent = this.caoZuoPanTransfrom.parent;
		//this.shiLiDieJiaControl.transform.localPosition = new Vector3(0f, this.shiLiTransfrom.localPosition.y - this.shiLiDieJiaControl.showTextureGo.transform.localPosition.y * this.shiLiDieJiaControl.transform.localScale.x, 0f);

		foreach (ImageControl current3 in this.Fixed_Figure_Control.imageList)
		{
			UnityEngine.Object.Destroy(current3);
		}
		this.Fixed_Figure_Control.imageList.Clear();
	}

	private List<GameObject> GetAllChilds(Transform rootTransfrom)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < rootTransfrom.childCount; i++)
		{
			list.Add(rootTransfrom.GetChild(i).gameObject);
		}
		return list;
	}

	private List<GameObject> CreateDian(Transform parentTransfrom ,float sacle =1)
	{
		float num = 200f * sacle;
		float num2 = CommonConfiguration.DotDistance * sacle;
		int num3 = (int)(num * 2f / num2);
		int num4 = num3 * num3;
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < num4; i++)
		{
			int num5 = i / num3;
			int num6 = i % num3;
			//Vector3 vector = new Vector3(-num + (float)num6 * num2, num - (float)num5 * num2, 0f) + parentTransfrom.localPosition;
			//if (Vector3.Distance(vector, parentTransfrom.localPosition) < 190f * sacle)
			//{
			//	//GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.preDian, this.dianRootTransfrom);
			//	GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.preDian, parentTransfrom);
			//	gameObject.transform.localPosition = vector;
			//	gameObject.transform.localScale = Vector3.one*0.1f;/* parentTransfrom.localScale;*/
			//	list.Add(gameObject);
			//}
			Vector3 vector = new Vector3(-num + (float)num6 * num2, num - (float)num5 * num2, 0f);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.preDian, parentTransfrom);
			gameObject.transform.localPosition = vector;
			gameObject.transform.localScale = Vector3.one * 0.1f;/* parentTransfrom.localScale;*/
			list.Add(gameObject);
		}
		return list;
	}

	//private List<GameObject> CreateDian(Transform parentTransfrom, float sacle = 1)
	//{
	//	float num = 200f * parentTransfrom.localScale.x;
	//	float num2 = 20f * parentTransfrom.localScale.x;
	//	int num3 = (int)(num * 2f / num2);
	//	int num4 = num3 * num3;
	//	List<GameObject> list = new List<GameObject>();
	//	for (int i = 0; i < num4; i++)
	//	{
	//		int num5 = i / num3;
	//		int num6 = i % num3;
	//		Vector3 vector = new Vector3(-num + (float)num6 * num2, num - (float)num5 * num2, 0f) + parentTransfrom.localPosition;
	//		if (Vector3.Distance(vector, parentTransfrom.localPosition) < 190f * parentTransfrom.localScale.x)
	//		{
	//			//GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.preDian, this.dianRootTransfrom);
	//			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.preDian, parentTransfrom);
	//			gameObject.transform.localPosition = vector;
	//			gameObject.transform.localScale = Vector3.one * 0.1f;/* parentTransfrom.localScale;*/
	//			list.Add(gameObject);
	//		}
	//	}
	//	return list;
	//}
	public void DoGameOver()
	{
		this.isGamePlay = false;
		UnityEngine.Debug.Log("Finished");
		//this.startPanel.ShowPanel(true);
		this.audioSource.PlayOneShot(this.success);
	}

	public void ShowTip()
	{
		GameData.Instance.doing_show_tip = true;
		var level_data = GameData.Instance.GetLevelData(GameData.Instance.currentGameLevel, GameData.Instance.Current_Difficulty);
		this.CreateOperationalImage(level_data);
		//if(MoveIEnumerator == null)
		//{
		//	MoveIEnumerator = Move();
		//}else
		//{
		//	StopCoroutine(MoveIEnumerator);
		//}
		MoveIEnumerator = Move();
		StartCoroutine(MoveIEnumerator);

	}
	IEnumerator MoveIEnumerator = null;

	public IEnumerator Move()
	{
		bool is_done = false;
		yield return new WaitForSeconds(0.2f);
		foreach (ImageControl current in this.Operational_Figure_Control.imageList)
		{
			UnityEngine.Debug.Log("----移动");
			is_done = false;
			//调用DOmove方法来让图片移动
			Tweener tweener = current.GetComponent<RectTransform>().DOLocalMove(current.image_data.ImagePosition, 0.3f);
			//设置这个Tween不受Time.scale影响
			tweener.SetUpdate(true);
			//设置移动类型
			tweener.SetEase(Ease.Linear);
			tweener.OnUpdate(() => { current.DragToPos(current.transform.localPosition); });
			tweener.OnStepComplete(() => 
			{
				is_done = true;
				UnityEngine.Debug.Log("----移动fff");
			});
			yield return new WaitUntil(()=>is_done == true);
		}
		UnityEngine.Debug.Log("----移动完成");
		GameData.Instance.doing_show_tip = false;
	}

	private string sceneid = "";
	public void OnGUI()
	{
		//用户名  
		GUI.Label(new Rect(20, 20, 50, 50), "关卡");
		sceneid = GUI.TextField(new Rect(80, 20, 100, 50), sceneid, 15);//15为最大字符串长度  
		GUILayout.BeginHorizontal();
		GUILayout.EndHorizontal();
		if (GUI.Button(new Rect(0, 100, 200, 50), "开始"))
		{
			GameScene.gameSceneInsta.SetGameStart(int.Parse(sceneid));
			this.RefreshJinDu(CommonConfiguration.currentLevel);
			
		}

		if (GUI.Button(new Rect(0, 250, 200, 50), "提示"))
		{
			ShowTip();
		}
	}

}
