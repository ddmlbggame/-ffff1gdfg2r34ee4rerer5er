using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShader : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		//this.GetComponent<Image>().material.SetFloat("_r",0);
		//this.GetComponent<Image>().material.SetFloat("_g", 0);
		//this.GetComponent<Image>().material.SetFloat("_b", 1);
		//Color[] colors = new Color[3];
		//colors[0] = Color.blue;
		//colors[1] = Color.red;
		//colors[2] = Color.yellow;
		//this.GetComponent<Image>().material.SetColorArray("_Points", colors);

		CommonDefine.InitGameData();
		Texture2D baseImagetexture = CommonDefine.CreateTexture(CommonDefine.baseImages[0]);
		this.GetComponent<Image>().material.SetTexture("_Mask", baseImagetexture);
		Texture2D baseImagetexture2 = CommonDefine.CreateTexture(CommonDefine.baseImages[1]);
		this.GetComponent<Image>().material.SetTexture("_Mask2", baseImagetexture2);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
