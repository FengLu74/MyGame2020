Shader "MGame/UI/liuguangsoftMask"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("颜色", Color) = (1,1,1,0.5)
		
		[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
		[HideInInspector]_Stencil("Stencil ID", Float) = 0
		[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
		[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
		[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255

		[HideInInspector]_ColorMask("Color Mask", Float) = 15

		_Rotation("旋转", Float) = -0.98
		_width("宽度", Float) = -0.15
		_strength("强度", Float) = -0.08
		_Intervals("流光间隔时间", Float) = -0.27
		_time("时间", Float) = 0.48
		// Soft Mask support
		[PerRendererData] _SoftMask("Mask", 2D) = "white" {}

	}

	SubShader
	{

		Tags { "Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"}
		
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
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha One
		ColorMask [_ColorMask]

		
		Pass
		{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			
			// Soft Mask Support 
			#include "Assets/ThirdPartResource/SoftMask/Shaders/SoftMask.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP SOFTMASK_SIMPLE SOFTMASK_SLICED SOFTMASK_TILED
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				// Soft Mask Support 
				SOFTMASK_COORDS(2)
				
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform float _time;
			uniform float _Intervals;
			uniform float _Rotation;
			uniform float _width;
			uniform float _strength;


			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				OUT.worldPosition = IN.vertex;
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				// Soft Mask Support
				
				SOFTMASK_CALCULATE_COORDS(OUT, IN.vertex) 
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 appendResult35 = (float2(0.0 , ( (-7.17 + (frac( ( _Time.y * _time ) ) - 0.0) * (5.69 - -7.17) / (1.0 - 0.0)) * _Intervals )));
				float cos29 = cos( _Rotation );
				float sin29 = sin( _Rotation );
				float2 rotator29 = mul( IN.texcoord.xy - appendResult35 , float2x2( cos29 , -sin29 , sin29 , cos29 )) + appendResult35;
				float clampResult54 = clamp( ( ( abs( ( (rotator29).x + -0.5 ) ) + _width ) / _strength ) , 0.0 , 1.0 );
				float lerpResult64 = lerp( clampResult54 , 0.0 , _Color.a);
				float4 appendResult3 = (float4(lerpResult64 , lerpResult64 , lerpResult64 , 1.0));
				
				half4 color = ( appendResult3 * _Color );
				// Soft Mask Support
				color.a *= SOFTMASK_GET_MASK(IN); 

				return color*IN.color;
			}
			ENDCG
		}
	}

}

