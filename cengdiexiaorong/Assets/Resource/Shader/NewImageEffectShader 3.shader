Shader "TestShader/NewImageEffectShader 3"
{
	/*Properties
	{
		_r("r",float) = 1
	_g("g", float) = 1
	_b("b", float) = 1
	_colors("colors",color)
	}*/
	SubShader
	{
		/*Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha*/
		//ColorMask[_ColorMask]
		pass
	{
		CGPROGRAM
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
#pragma vertex vert
#pragma fragment frag
#include "unitycg.cginc"
#include "UnityUI.cginc"
			float _r =1;
		float _g = 0;
		float _b =1;
		//uniform float4 _Points[3];  // 数组变量
		//uniform float _Points_Num;  // 数组长度变量

		struct xxx
		{
			float4 pos:POSITION; //位置
			float4 col:COLOR;    //颜色
		};

		xxx vert(appdata_base data)
		{
			xxx x;
			x.pos = UnityObjectToClipPos(data.vertex);
		/*	if (pow((data.vertex.x - 0), 2) + pow((data.vertex.y - 0), 2) <1000)
			{
				x.col = float4(1, 1, 1, 1);
				return x;
			}*/
			//x.col = float4(_r, _g, _b, 1);

			x.col = float4(1, 1, 1, 1);
			return x;
		}

		float4 frag(xxx x) :COLOR
		{
			x.col = float4(0, 1, 1, 1);
			/*float4 p4 = _Points[0];
			x.col = p4;*/
		//// 遍历
		//for (int j = 0; j<_Points_Num; j++)
		//{
		//	float4 p4 = _Points[j]; // 索引取值
		//							// 自定义处理
		//}
			return x.col;
		}
		/*void frag(out float4 col:COLOR)
		{
			col = float4(1, 1, 0, 1);
		}*/
			ENDCG
	}
	}
		FallBack "Diffuse"
}
