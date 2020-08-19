Shader "MGame/Effect/Particles/Mobile/Additive" {
	Properties {
		_MainTex ("Particle Texture", 2D) = "white" {}
		_BloomTex("BloomTex", 2D) = "white" {}
		_BloomFactor("Bloom Factor", Range(0,1)) = 0
		_Color ("Tint", Color) = (1,1,1,1)
	}

	Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "PreviewType"="Plane" "RenderType" = "Effect" }

		Cull Off Lighting Off ZWrite Off Fog { Mode Off }

		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}

		SubShader {
			Pass 
			{
				Blend SrcAlpha One
				ColorMask RGB
				SetTexture [_MainTex] {
					combine texture * primary
				}
			}
			Pass
			{
				
				ColorMask A
				BlendOp Min
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest 
				#include "UnityCG.cginc"
				
				sampler2D _MainTex;
				sampler2D _BloomTex;
				half4 _MainTex_ST;
				half4 _BloomTex_ST;
				float _BloomFactor;
				float4 _Color;

				struct v2f {
					half4 pos : SV_POSITION;
					half2 uv : TEXCOORD0;
					half2 uvMain : TEXCOORD1;
					fixed4 vertexColor : COLOR;
				};

				v2f vert(appdata_full v) {
					v2f o;

					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _BloomTex);
					o.uvMain = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.vertexColor = v.color;

					return o;
				}

				fixed4 frag(v2f i) : COLOR
				{
					fixed4 color = tex2D(_BloomTex, i.uv.xy);
					fixed4 mainColor = tex2D(_MainTex, i.uvMain.xy);
					color.a = clamp(color.a * step(0.01, color.r + color.g + color.b) * _BloomFactor, 0, 1)* mainColor.a;
					color.a = 1 - color.a*_Color.a;
					return color;
				}

				ENDCG
			}
		}
	}
}
