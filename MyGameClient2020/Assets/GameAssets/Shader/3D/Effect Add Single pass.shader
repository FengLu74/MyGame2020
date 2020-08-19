// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "MGame/Effect/Particles/Mobile/Additive1" {
	Properties {
		
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)

	}
	SubShader {
		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }

		Pass {
			ZWrite Off
			Blend SrcAlpha One//use (SrcAlpha,One), not (One,One)
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			fixed4 _Color;
			
			struct myV2F{
				float4 pos:SV_POSITION;
				float2 uv    : TEXCOORD0;
			};
			
			myV2F vert(appdata_base v)  {
				myV2F v2f;
				v2f.pos=UnityObjectToClipPos (v.vertex);
				v2f.uv=v.texcoord;
				return v2f;
			}

			
			fixed4 frag(myV2F v2f) : COLOR {
				fixed4 c = tex2D (_MainTex, v2f.uv) ;
				
				return c*_Color;
			}

			ENDCG
		}
	}
}