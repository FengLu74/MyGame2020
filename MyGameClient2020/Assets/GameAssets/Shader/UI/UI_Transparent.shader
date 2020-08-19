// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MGame/UI/Transparent"
{
	Properties
	{
		_MainTex("RGB图集", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)

			//MASK SUPPORT ADD
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
		//MASK SUPPORT END
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
		ColorMask[_ColorMask]
		//MASK SUPPORT END

			Pass
			{
				Cull Off
				Lighting Off
				ZWrite Off
				Fog { Mode Off }
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

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
				};

				fixed4 _Color;
				fixed showGray;
				v2f vert(appdata_t IN)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(IN.vertex);
					o.texcoord = IN.texcoord;
					o.color = IN.color * _Color;
					return o;
				}

				sampler2D _MainTex; half4 _MainTex_ST;

				fixed4 frag(v2f IN) : SV_Target
				{ 
					fixed4 color;
					color = tex2D(_MainTex, IN.texcoord);
					
					if (IN.color.r == 0 || showGray == -1)
					{
						color.rgb = dot(color.rgb, fixed3(0.3,0.59,0.11));
					}
					else
					{
						color = color * IN.color;
					}

					return color;
				}
				ENDCG
			}
		}
}
