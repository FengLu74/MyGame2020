// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "MGame/3D/Sprites/Default"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("基础颜色叠加属性", Color) = (1,1,1,1)
		_FlickMultColor("颜色曝光属性", Color) = (0,0,0,1)
		[Space(30)]
		_FlickerColor("闪白的颜色", Color) = (0,0,0,1)
		_FlickValue("闪白", Range(0,1)) = 0
		[Space(20)]
		[Header(Depth FOG)]
		[Space(8)]
		_jinshengColor("景深雾的颜色", Color) = (1,1,1,0)
		[HideInInspector]_jinshengrotate("景深雾的旋转",Float) = 0.5
		_jinshengx("景深雾X轴",Float) = 0
		_jinshengy("景深雾Y轴",Float) = 0
		[Space(20)]
		[Header(Height FOG)]
		[Space(8)]
		_gaoduColor("高度雾气颜色", Color) = (1,1,1,0)
		_gaodux("高度雾X轴",Float) = 0
		_gaoduy("高度雾Y轴",Float) = 0

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

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag
			#pragma target 2.0
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float4 uv : TEXCOORD0;
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float4 uv : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				
			};

			fixed4 _FlickerColor;
			fixed _FlickValue;
			fixed4 _FlickMultColor;
			fixed _jinshengrotate;
			fixed _jinshengx;
			fixed _jinshengy;
			fixed _gaodux;
			fixed _gaoduy;
			fixed4 _Color;
			fixed4 _gaoduColor;
			fixed4 _jinshengColor;
			sampler2D _MainTex;


			v2f Vert(appdata v)
			{
				v2f o = (v2f) 0;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv.xy = v.uv.xy;
				o.worldPosition = v.vertex;
				float4 WP =  mul(unity_ObjectToWorld, v.vertex).xyzw;
				
				//景深雾气 Rotator做的旋转矩阵，正常来讲只要覆盖一个渐变颜色就可以模拟景深雾，但是由于因为旋转了30度，所以这边加了一个矩阵变换
				float cos5 = cos( _jinshengrotate );
				float sin5 = sin( _jinshengrotate );
				float2 rotator5 = mul( float2(WP.zw) - float2( 0,0 ) , float2x2( cos5 , -sin5 , sin5 , cos5 )) + float2( 0,0 );
				float jianshengfog = saturate ((rotator5.y + _jinshengx)/_jinshengy);
				//高度雾气
				float gaodufog = saturate ( (v.vertex.xyz.y + _gaodux) / _gaodux);
				

				// float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				//景深雾气
				o.uv.z = jianshengfog;
				//高度雾气
				o.uv.w = gaodufog;
				o.color = v.color;
				

				return o;
			}

			fixed4 Frag(v2f i) : SV_Target
			{

				// i.vertex.w指的是高度雾气，i.uv.z指的是景深雾气
				float4 rawColor = tex2D(_MainTex,i.uv);
				float finalAlpha = rawColor.a;
				
				//高度雾的颜色与贴图的颜色进行差值
				fixed3 gaoducolor = lerp ( rawColor.rgb,_gaoduColor.rgb ,_gaoduColor.a * i.uv.w);
				//高度雾的alpha与贴图进行差值
				fixed gaodujinshengalpha = lerp (i.uv.w , i.uv.z , i.uv.w + i.uv.z);
				//景深雾的颜色与贴图的颜色进行差值
				fixed3 jinshengcolor = lerp (rawColor.rgb , _jinshengColor.rgb , _jinshengColor.a * i.uv.z);
				//景深雾和高度雾之间的差值
				fixed3 mixcolor = lerp (jinshengcolor , gaoducolor , gaodujinshengalpha);
				//闪白颜色差值
				fixed3 shanbaicolor = lerp (mixcolor , _FlickerColor.rgb , _FlickValue);
				fixed4 mixcolor1;
				mixcolor1 = fixed4 (shanbaicolor*_Color.rgb + _FlickMultColor.rgb * i.color.rgb, rawColor.a * _FlickerColor.a *_Color.a * _FlickMultColor.a *i.color.a);

				mixcolor1.rgb *= mixcolor1.a;
				return mixcolor1;

			}


			ENDCG
		}
	}
}
