Shader "MGame/UI/UVAnim"
{
	Properties
	{
		_u("u", Float) = 4
		_v("v", Float) = 4
		_index("index", Float) = 4
		_Color("Tint", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		Blend  SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			fixed _u;
			fixed _v;

			fixed _index;
			fixed4 _Color;

			struct appdata
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
				float4 vertex : SV_POSITION;
			};
			 
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = v.color * _Color;
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				_u = floor(_u);
				_v = floor(_v);
				fixed row = ceil(_index / _u) - 1;
				fixed cel = _index - row*_v -1;
				fixed2 newUV = fixed2(i.uv.x / _v+cel/_v, i.uv.y / _u+(_u-row-1)/_u);
				fixed4 col = tex2D(_MainTex, newUV)* i.color;
				return col;
			}
			ENDCG
		}
	}
}
