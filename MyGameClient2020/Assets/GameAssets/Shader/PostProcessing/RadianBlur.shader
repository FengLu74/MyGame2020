﻿Shader "MGame/PostProcessing/RadianBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_RadianCenterX("RadianCenterX",Range(0,1))=0
		_RadianCenterY("RadianCenterY",Range(0,1))=0
		_BlurRatio("BlurRatio",Range(0,0.1))=0
		_ClearDis("ClearDis",Range(0,0.5))=0

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float _BlurRatio;
			float _RadianCenterY;
			float _RadianCenterX;
			float _ClearDis;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				half2 blurCenter=half2(_RadianCenterX,_RadianCenterY);
				float distanceToCenter=distance(blurCenter,i.uv);
				float2 blurDir=normalize(i.uv-blurCenter);
				fixed4 blurCol=fixed4(0,0,0,1);
				int blurR=5;
				for(int k=0;k<blurR;k++)
				{
					half2 tempUv=i.uv+blurDir*k*max(0,distanceToCenter-_ClearDis)*_BlurRatio;
					blurCol+=tex2D(_MainTex,tempUv);
				}
				blurCol=blurCol/blurR;
				return blurCol;
			}
			ENDCG
		}
	}
}