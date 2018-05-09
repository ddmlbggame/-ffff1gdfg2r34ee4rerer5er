// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CullImage" {


	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)


		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
		_Radius("序列帧播放速度",Float) = 0.1


	}


		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}


		Stencil
	{
		Ref[_Stencil]
		Comp[_StencilComp]
		Pass[_StencilOp]
		ReadMask[_StencilReadMask]
		WriteMask[_StencilWriteMask]
	}


		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]


		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag


#include "UnityCG.cginc"
#include "UnityUI.cginc"


		struct appdata_t
	{
		float4 vertex : POSITION;
		float4 color : COLOR;
		float2 texcoord : TEXCOORD0;
	};


	struct v2f
	{
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
		half2 texcoord : TEXCOORD0;
		float4 worldPosition : TEXCOORD1;
	};


	fixed4 _Color;
	fixed4 _TextureSampleAdd;


	bool _UseClipRect;
	float4 _ClipRect;


	bool _UseAlphaClip;
	float i;
	float p;
	float j;
	float k;
	float _Radius;


	v2f vert(appdata_t IN)
	{
		v2f OUT;
		OUT.worldPosition = IN.vertex;


		OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);


		OUT.texcoord = IN.texcoord;


#ifdef UNITY_HALF_TEXEL_OFFSET
		OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
#endif


		OUT.color = IN.color * _Color;
		return OUT;
	}


	sampler2D _MainTex;


	fixed4 frag(v2f IN) : SV_Target
	{


		half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
		float2 uv = IN.texcoord.xy;
		float4 c = IN.color;
		// 左下四方块
		if (uv.x < _Radius && uv.y < _Radius)
		{
			float2 r;
			r.x = uv.x - _Radius;
			r.y = uv.y - _Radius;
			float rr = length(r);
			// 裁剪
			if (rr > _Radius)
			{
				c.a = 0;
			}
		}
		// 左上四方块
		else if (uv.x < _Radius && uv.y > 1 - _Radius)
		{
			float2 r;
			r.x = uv.x - _Radius;
			r.y = uv.y + _Radius - 1;
			float rr = length(r);
			// 裁剪
			if (rr > _Radius)
			{
				c.a = 0;
			}
		}
		// 右下四方块
		else if (uv.x > 1 - _Radius && uv.y < _Radius)
		{
			float2 r;
			r.x = uv.x + _Radius - 1;
			r.y = uv.y - _Radius;
			float rr = length(r);
			// 裁剪
			if (rr > _Radius)
			{
				c.a = 0;
			}
		}
		// 右上四方块
		else if (uv.x > 1 - _Radius && uv.y > 1 - _Radius)
		{
			float2 r;
			r.x = uv.x + _Radius - 1;
			r.y = uv.y + _Radius - 1;
			float rr = length(r);
			// 裁剪
			if (rr > _Radius)
			{
				c.a = 0;
			}
		}


		fixed4 col = tex2D(_MainTex, IN.texcoord) * c;
		clip(col.a - 0.01);




		if (_UseClipRect)
			color *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);


		if (_UseAlphaClip)
			clip(color.a - 0.001);
		return col;
	}


		ENDCG
	}
	}
}