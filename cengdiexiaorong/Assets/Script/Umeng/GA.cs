using System;
using UnityEngine;

namespace Umeng
{
	public class GA : Analytics
	{
		public enum Gender
		{
			Unknown,
			Male,
			Female
		}

		public enum PaySource
		{
			AppStore = 1,
			支付宝,
			网银,
			财付通,
			移动,
			联通,
			电信,
			Paypal,
			Source9,
			Source10,
			Source11,
			Source12,
			Source13,
			Source14,
			Source15,
			Source16,
			Source17,
			Source18,
			Source19,
			Source20
		}

		public enum BonusSource
		{
			玩家赠送 = 1,
			Source2,
			Source3,
			Source4,
			Source5,
			Source6,
			Source7,
			Source8,
			Source9,
			Source10
		}

		public static void SetUserLevel(int level)
		{
			Analytics.Agent.CallStatic("setPlayerLevel", new object[]
			{
				level
			});
		}

		[Obsolete("SetUserLevel(string level) 已弃用, 请使用 SetUserLevel(int level)")]
		public static void SetUserLevel(string level)
		{
			Debug.LogWarning("SetUserLevel(string level) 已弃用, 请使用 SetUserLevel(int level)");
		}

		[Obsolete("SetUserInfo已弃用, 请使用ProfileSignIn")]
		public static void SetUserInfo(string userId, GA.Gender gender, int age, string platform)
		{
			Analytics.Agent.CallStatic("setPlayerInfo", new object[]
			{
				userId,
				age,
				(int)gender,
				platform
			});
		}

		public static void StartLevel(string level)
		{
			Analytics.Agent.CallStatic("startLevel", new object[]
			{
				level
			});
		}

		public static void FinishLevel(string level)
		{
			Analytics.Agent.CallStatic("finishLevel", new object[]
			{
				level
			});
		}

		public static void FailLevel(string level)
		{
			Analytics.Agent.CallStatic("failLevel", new object[]
			{
				level
			});
		}

		public static void Pay(double cash, GA.PaySource source, double coin)
		{
			Analytics.Agent.CallStatic("pay", new object[]
			{
				cash,
				coin,
				(int)source
			});
		}

		public static void Pay(double cash, int source, double coin)
		{
			if (source < 1 || source > 100)
			{
				throw new ArgumentException();
			}
			Analytics.Agent.CallStatic("pay", new object[]
			{
				cash,
				coin,
				source
			});
		}

		public static void Pay(double cash, GA.PaySource source, string item, int amount, double price)
		{
			Analytics.Agent.CallStatic("pay", new object[]
			{
				cash,
				item,
				amount,
				price,
				(int)source
			});
		}

		public static void Buy(string item, int amount, double price)
		{
			Analytics.Agent.CallStatic("buy", new object[]
			{
				item,
				amount,
				price
			});
		}

		public static void Use(string item, int amount, double price)
		{
			Analytics.Agent.CallStatic("use", new object[]
			{
				item,
				amount,
				price
			});
		}

		public static void Bonus(double coin, GA.BonusSource source)
		{
			Analytics.Agent.CallStatic("bonus", new object[]
			{
				coin,
				(int)source
			});
		}

		public static void Bonus(string item, int amount, double price, GA.BonusSource source)
		{
			Analytics.Agent.CallStatic("bonus", new object[]
			{
				item,
				amount,
				price,
				(int)source
			});
		}

		public static void ProfileSignIn(string userId)
		{
			Analytics.Agent.CallStatic("onProfileSignIn", new object[]
			{
				userId
			});
		}

		public static void ProfileSignIn(string userId, string provider)
		{
			Analytics.Agent.CallStatic("onProfileSignIn", new object[]
			{
				provider,
				userId
			});
		}

		public static void ProfileSignOff()
		{
			Analytics.Agent.CallStatic("onProfileSignOff", new object[0]);
		}
	}
}
