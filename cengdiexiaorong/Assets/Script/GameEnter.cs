﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnter : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		UIManager.Instance._Init(this.transform);
	}
	
}
