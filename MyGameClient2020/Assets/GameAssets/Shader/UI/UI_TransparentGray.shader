// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MGame/UI/TransparentGray"
{
	Properties
	{
		_MainTex("RGB图集", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		showGray("showGray", Float) = -1

			//MASK SUPPORT ADD
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
			// Soft Mask support
		[PerRendererData] _SoftMask("Mask", 2D) = "white" {}


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

			//MASK SUPPORT ADD
			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}
			

			Pass
			{
				Cull Off
				Lighting Off
				ZWrite Off
				Fog { Mode Off }
				Blend SrcAlpha OneMinusSrcAlpha
				ColorMask[_ColorMask]

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"
// Soft Mask Support 
				#include "Assets/ThirdPartResource/SoftMask/Shaders/SoftMask.cginc"

				#pragma multi_compile __ UNITY_UI_ALPHACLIP SOFTMASK_SIMPLE SOFTMASK_SLICED SOFTMASK_TILED

				struct appdata_t
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					half2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					half2 texcoord : TEXCOORD0;

					// Soft Mask Support 
					SOFTMASK_COORDS(2)
				};

				fixed4 _Color;
				fixed showGray;
				v2f vert(appdata_t IN)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(IN.vertex);
					o.texcoord = IN.texcoord;

#ifdef UNITY_HALF_TEXEL_OFFSET     
					o.vertex.xy -= (_ScreenParams.zw - 1.0);
#endif 
					o.color = IN.color * _Color;
					
                     // Soft Mask Support
					SOFTMASK_CALCULATE_COORDS(o, IN.vertex) 
					return o;


				}

				sampler2D _MainTex; half4 _MainTex_ST;

				fixed4 frag(v2f IN) : SV_Target
				{ 
					fixed4 color;
					color = tex2D(_MainTex, IN.texcoord);

					// Soft Mask Support
					color.a *= SOFTMASK_GET_MASK(IN); 
					
					if (IN.color.r == 0 || showGray == -1)
					{
						color.rgb = dot(color.rgb, fixed3(0.3,0.59,0.11));
					}
					else
					{
						color = color * IN.color;
					}
					return color;

					/*half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
					float grey = dot(color.rgb, fixed3(0.22, 0.707, 0.071));
					return half4(grey,grey,grey,color.a);*/


				}
				ENDCG
			}
		}
		FallBack "Diffuse"
}
