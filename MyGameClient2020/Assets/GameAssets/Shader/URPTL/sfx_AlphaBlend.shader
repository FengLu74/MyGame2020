Shader "sfx/BasicAlphaBlend"
{
	Properties
	{
		[HDR] _TintColor ("Tint Color", Color)=(1,1,1,1)
		_MainTex ("Particle Texture", 2D) = "white" {}	
	    _UVScroll("UV Scroll",Vector) = (0,0,0,0)
	}
	SubShader 
	{
		Tags {  "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "RenderPipeline"="UniversalPipeline"}	
		Blend SrcAlpha OneMinusSrcAlpha
		//ColorMask RGB
		Cull Off
		Lighting Off
		ZWrite Off
		Pass 
		{
			Name "BR_Effect_Basic_AlphaBlend" 
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			CBUFFER_START(UnityPerMaterial)
			sampler2D _MainTex;
			half4 _MainTex_ST;
			half2  _UVScroll;
			half4 _TintColor;
			CBUFFER_END

			
			struct VertexInput 
			{
				half4 vertex : POSITION;
				half4 color : COLOR;
				half2 uv : TEXCOORD0;
			};

			struct v2f 
			{
				half4 vertex : SV_POSITION;
				half4 color : COLOR;
				half2 uv : TEXCOORD0;
			};

			v2f vert (VertexInput v)
			{
				v2f o;				
				o.vertex = TransformObjectToHClip(v.vertex.xyz);
				o.color = v.color;
				o.uv = TRANSFORM_TEX(v.uv,_MainTex) + _Time.x * _UVScroll;
				return o;
			}
			
			half4 frag (v2f i) : SV_Target
			{
				half4 col = tex2D(_MainTex, i.uv)*_TintColor;
				col.rgb=2*i.color.rgb *col.rgb*_TintColor.rgb;				
				col.a=col.a*_TintColor.a*i.color.a;
				//clip(col.a - 0.01);
				return col;
			}
			ENDHLSL
		}
	}
	//Fallback "Mobile/Particles/Additive"
}

