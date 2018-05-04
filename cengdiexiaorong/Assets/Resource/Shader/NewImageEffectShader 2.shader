// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ImageEffect/MaskIcon"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_Mask("Base (RGB)", 2D) = "white" {}
	_Mask2("Base2 (RGB)", 2D) = "white" {}


	_Color("Tint", Color) = (1,1,1,1)
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
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
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#include "UnityUI.cginc"

#pragma multi_compile __ UNITY_UI_ALPHACLIP

		struct a2v
	{
		fixed2 uv : TEXCOORD0;
		half4 vertex : POSITION;
		float4 color    : COLOR;
	};

	fixed4 _Color;

	struct v2f
	{
		fixed2 uv : TEXCOORD0;
		half4 vertex : SV_POSITION;
		float4 color    : COLOR;
		half2 texcoord  : TEXCOORD0;
	};

	sampler2D _MainTex;
	sampler2D _Mask;
	sampler2D _Mask2;
	v2f vert(a2v i)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(i.vertex);
		o.uv = i.uv;

		o.color = i.color * _Color;
		return o;
	}

	float4 colors[];

	fixed4 frag(v2f i) : COLOR
	{
		half4 color = tex2D(_MainTex, i.uv) * i.color;
		half4 mask = tex2D(_Mask, i.uv);
		half4 mask2 = tex2D(_Mask2, i.uv);
		color = fixed4(1, 1, 0, 1);

		//color.a *= mask.a;
		/*if (pow((i.uv.x - 0), 2) + pow((i.uv.y - 0), 2) <5)
		{
			return fixed4(0, 1, 0, 1);
		}*/

		/*if (i.uv.x > 2) {
			return fixed4(0, 1, 0, 1);
		}*/

	/*	color.a *= mask2.a;*/
		//color.a = color.a &mask.a;
		/*if (color.r == mask.r &&color.g == mask.g && color.b == mask.b)
		{
			color.a = 0;
		}
		else 
		{
			color.a = 1;
		}*/

		/*if (color.r == mask2.r &&color.g == mask2.g && color.b == mask2.b)
		{
			color.a = 0;
		}
		else
		{
			color.a = 1;
		}*/


		return color;
	}
		ENDCG
	}
	}
}
