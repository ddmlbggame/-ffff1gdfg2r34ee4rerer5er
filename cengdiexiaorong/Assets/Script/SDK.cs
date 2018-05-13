using System.Collections;
using System.Collections.Generic;
using Umeng;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Advertisements;
using System;

public class SDK : MonoBehaviour {

	public static SDK Instance;

	public string unity_ads_game_id = "1798175";

	public string gamecenter_board_id = "需要填写挑战模式排行榜";


	private void Awake()
	{
		#region 友盟
		GA.Start();
		//调试时开启日志 发布时设置为false
		GA.SetLogEnabled(true);
		#endregion
		InitGameCenter();
		// 初始化unity广告
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(unity_ads_game_id, true);
		}
		Instance = this;
	}
	
	//// Use this for initialization
	//void Start () {

	//	#region 友盟
	//	GA.Start();
	//	//调试时开启日志 发布时设置为false
	//	GA.SetLogEnabled(true);
	//	#endregion
	//}

	// 进入关卡
	public void StartLevel(string level_name)
	{
		GA.StartLevel(level_name);
		
	}

	public void FinishLevel(string level_name)
	{
		GA.FinishLevel(level_name);
	}


	private void InitGameCenter()
	{
		Social.localUser.Authenticate(HandleAuthenticated);
	}

	/// <summary>  
	/// 初始化 GameCenter 结果回调函数  
	/// </summary>  
	/// <param name="success">If set to <c>true</c> success.</param>  
	private void HandleAuthenticated(bool success)
	{
		Debug.Log("*** HandleAuthenticated: success = " + success);
		///初始化成功  
		if (success)
		{
			string userInfo = "Username: " + Social.localUser.userName +
				"\nUser ID: " + Social.localUser.id +
				"\nIsUnderage: " + Social.localUser.underage;
			Debug.Log(userInfo);
		}
		else
		{
			///初始化失败  

		}

	}

	// 成就设置
	public void ReportProgress()
	{
		if (Social.localUser.authenticated)
		{
			Social.ReportProgress("XXXX", 15, HandleProgressReported);
		}
	}

	// 排行榜分数设置
	public void ReportScore(long score)
	{
		if (Social.localUser.authenticated)
		{
			Debug.Log("ReportScore" + score);
			Social.ReportScore(score, gamecenter_board_id, HandleScoreReported);
		}
	}

	// 打开排行榜
	public void ShowLeaderboardUI()
	{
		GA.Event("打开排行榜");
		if (Social.localUser.authenticated)
		{
			Social.ShowLeaderboardUI();
		}
	}
	//打开成就

	public void ShowAchievementsUI()
	{
		if (Social.localUser.authenticated)
		{
			Social.ShowAchievementsUI();
		}
	}

	//上传排行榜分数  
	public void HandleScoreReported(bool success)
	{
		Debug.Log("*** HandleScoreReported: success = " + success);
	}
	//设置 成就  
	private void HandleProgressReported(bool success)
	{
		Debug.Log("*** HandleProgressReported: success = " + success);
	}


	/// <summary>  
	/// 加载成就回调  
	/// </summary>  
	/// <param name="achievements">Achievements.</param>  
	private void HandleAchievementsLoaded(IAchievement[] achievements)
	{
		Debug.Log("* HandleAchievementsLoaded");
		foreach (IAchievement achievement in achievements)
		{
			Debug.Log("* achievement = " + achievement.ToString());
		}
	}

	/// <summary>  
	///   
	/// 成就回调描述  
	/// </summary>  
	/// <param name="achievementDescriptions">Achievement descriptions.</param>  
	private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions)
	{
		Debug.Log("*** HandleAchievementDescriptionsLoaded");
		foreach (IAchievementDescription achievementDescription in achievementDescriptions)
		{
			Debug.Log("* achievementDescription = " + achievementDescription.ToString());
		}
	}


	#region unity 广告

	public void ShowRewardedVideo()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show("rewardedVideo", options);
	}

	public static Action RewardUnityAds;
	void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			Debug.Log("Video completed - Offer a reward to the player");
			// Reward your player here.
			if (RewardUnityAds != null)
			{
				RewardUnityAds();
			}

		}
		else if (result == ShowResult.Skipped)
		{
			Debug.LogWarning("Video was skipped - Do NOT reward the player");

		}
		else if (result == ShowResult.Failed)
		{
			Debug.LogError("Video failed to show");
		}
	}

	#endregion
}
