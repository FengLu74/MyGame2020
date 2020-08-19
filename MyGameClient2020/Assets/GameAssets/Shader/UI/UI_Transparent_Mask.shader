// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MGame/UI/Transparent_Mask"
{
	Properties
	{
		[HideInInspector]_MainTex("RGB图集", 2D) = "white" {}	
		_MaskTex("mask", 2D) = "white" {}		
		_Color("Tint", Color) = (1,1,1,1)
		_Mult("mult",Float)=1
		_gray("gray",Float) = 1

		[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
		[HideInInspector]_Stencil("Stencil ID", Float) = 0
		[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
		[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
		[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255

		[HideInInspector]_ColorMask("Color Mask", Float) = 15
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

				#pragma multi_compile __ UNITY_UI_ALPHACLIP

				struct appdata_t
				{
					float4 vertex   : POSITION;
					fixed4 color : COLOR;
					half2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					half2 texcoord  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
				};

				fixed4 _Color;
				fixed _Mult;
				fixed _gray;
				sampler2D _MaskTex;

				bool _UseClipRect;
				float4 _ClipRect;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.worldPosition = IN.vertex;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					return OUT;
				}

				sampler2D _MainTex; half4 _MainTex_ST;

				bool GetValue(fixed4 v)
				{
					return v.a >0;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					fixed4 color;
					color = tex2D(_MainTex, IN.texcoord);
					fixed4 maskTex = tex2D(_MaskTex, IN.texcoord);
					if (IN.color.r == 0)
					{
						color.rgb = dot(color.rgb, fixed3(0.3,0.59,0.11));
					}
					else
					{
						color = _Mult*color * IN.color;
					}

					if (_UseClipRect)
						color *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

					#ifdef UNITY_UI_ALPHACLIP
					clip(color.a - 0.001);
					#endif

					color.r= (floor(_gray)*color.r)+((1- floor(_gray))*_gray);
					color.g = (floor(_gray)*color.g)+((1 - floor(_gray))*_gray);
					color.b = (floor(_gray)*color.b)+((1 - floor(_gray))*_gray);
                    color *=maskTex;
					return color;
				}

				

			ENDCG
			}
		}
}
