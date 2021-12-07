Shader "sfx/AlphaBlend_move"
{
	Properties
	{
		[HDR] _MainColor("MainColor", Color) = (0.7830189,0.7830189,0.7830189,0)
		_Texture("Texture", 2D) = "white" {}
		[KeywordEnum(RGBA,R,G,B,A)] _Tex("Tex", Float) = 1
		[Toggle(_UVON_ON)] _UVon("UVon", Float) = 0
		_U("U", Float) = 0
		_v("v", Float) = 0
		_MaskTex("MaskTex", 2D) = "white" {}
		[KeywordEnum(R,G,B,A)] _Mask("Mask", Float) = 0
		[Toggle(_MASKUV_ON)] _MaskUV("Mask UV", Float) = 0
		_U1("U1", Float) = 0
		_v1("v1", Float) = 0
		[Toggle(_DISSLOVESON_ON)] _Dissloveson("Dissloves on", Float) = 0
		_Dissloves("Dissloves", 2D) = "white" {}
		_Hardness("Hardness", Range(0 , 1)) = 1
		_Dissolvepower("Dissolve power", Range(0 , 1)) = 0
		[Enum(Particle,1,Material,0)]_Dissove("Dissove", Float) = 0
		_NoiseTex("NoiseTex", 2D) = "white" {}
		[Enum(Noise OFF,0,Noise ON,1)]_NoiseOpen("Noise Open", Float) = 0
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_NoisePower("Noise Power", Range(0 , 2)) = 1
		[HideInInspector] _texcoord("", 2D) = "white" {}

	}
	
	SubShader {
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" "RenderPipeline" = "UniversalPipeline" "IgnoreProjector" = "True" "PreviewType" = "Plane" "PerformanceChecks" = "False" }
        LOD 100
        Pass {
            Name "FORWARD"
			Tags{ "LightMode" = "UniversalForward" }
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ColorMask RGBA
			ZWrite Off
			ZTest LEqual
			Offset 0 , 0
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#define ASE_ABSOLUTE_VERTEX_POS 1
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _TEX_RGBA _TEX_R _TEX_G _TEX_B _TEX_A
			#pragma shader_feature_local _UVON_ON
			#pragma shader_feature_local _DISSLOVESON_ON
			#pragma shader_feature_local _MASK_R _MASK_G _MASK_B _MASK_A
			#pragma shader_feature_local _MASKUV_ON
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #pragma target 3.0

			CBUFFER_START(UnityPerMaterial)
           	uniform half4 _MainColor;
			uniform sampler2D _Texture;
			uniform half _U;
			uniform half _v;
			uniform half4 _Texture_ST;
			uniform sampler2D _NoiseTex;
			uniform half4 _Vector0;
			uniform half4 _NoiseTex_ST;
			uniform half _NoisePower;
			uniform half _NoiseOpen;
			uniform half _Hardness;
			uniform sampler2D _Dissloves;
			uniform half4 _Dissloves_ST;
			uniform half _Dissolvepower;
			uniform half _Dissove;
			uniform sampler2D _MaskTex;
			uniform half _U1;
			uniform half _v1;
			uniform half4 _MaskTex_ST;
			CBUFFER_END

            struct VertexInput {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;

				UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct VertexOutput {
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
					float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
            };

            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
				 
				o.ase_texcoord1 = v.ase_texcoord;
				o.ase_texcoord2 = v.ase_texcoord1;
				o.ase_color = v.color;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = vertexInput.positionCS;

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
					o.worldPos = TransformObjectToWorld(v.vertex).xyz;
				#endif

                return o;
            }
			half4 frag(VertexOutput i, float facing : VFACE) : COLOR {
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

				half4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half4 appendResult30 = (half4(_U , _v , 0.0 , 0.0));
				half2 uv0_Texture = i.ase_texcoord1.xy * _Texture_ST.xy + _Texture_ST.zw;
				half2 panner81 = (_Time.y * appendResult30.xy + uv0_Texture);
				half4 uv138 = i.ase_texcoord2;
				uv138.xy = i.ase_texcoord2.xy * float2(1,1) + float2(0,0);
				half4 appendResult39 = (half4(uv138.x , uv138.y , 0.0 , 0.0));
				#ifdef _UVON_ON
				half4 staticSwitch44 = (half4(uv0_Texture, 0.0 , 0.0) + appendResult39);
				#else
				half4 staticSwitch44 = half4(panner81, 0.0 , 0.0);
				#endif
				half2 uv0_NoiseTex = i.ase_texcoord1.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
				half2 panner119 = (_Time.y * _Vector0.xy + uv0_NoiseTex);
				half lerpResult168 = lerp(0.0, (tex2D(_NoiseTex, (panner119 * 2.0 + -1.0)).r * _NoisePower), _NoiseOpen);
				half4 tex2DNode4 = tex2D(_Texture, (staticSwitch44 + lerpResult168).xy);
				half4 temp_cast_5 = (tex2DNode4.r).xxxx;
				half4 temp_cast_6 = (tex2DNode4.r).xxxx;
				half4 temp_cast_7 = (tex2DNode4.g).xxxx;
				half4 temp_cast_8 = (tex2DNode4.b).xxxx;
				half4 temp_cast_9 = (tex2DNode4.a).xxxx;
				#if defined(_TEX_RGBA)
				half4 staticSwitch98 = tex2DNode4;
				#elif defined(_TEX_R)
				half4 staticSwitch98 = temp_cast_5;
				#elif defined(_TEX_G)
				half4 staticSwitch98 = temp_cast_7;
				#elif defined(_TEX_B)
				half4 staticSwitch98 = temp_cast_8;
				#elif defined(_TEX_A)
				half4 staticSwitch98 = temp_cast_9;
				#else
				half4 staticSwitch98 = temp_cast_5;
				#endif
				float2 uv_Dissloves = i.ase_texcoord1.xy * _Dissloves_ST.xy + _Dissloves_ST.zw;
				half3 uv1108 = i.ase_texcoord2.xyz;
				uv1108.xy = i.ase_texcoord2.xyz.xy * float2(1,1) + float2(0,0);
				half lerpResult114 = lerp(_Dissolvepower , uv1108.z , _Dissove);
				half smoothstepResult100 = smoothstep(_Hardness , 1.0 , ((tex2D(_Dissloves, uv_Dissloves).r + 1.0) - (lerpResult114 * (1.0 + (1.0 - _Hardness)))));
				#ifdef _DISSLOVESON_ON
				half staticSwitch53 = smoothstepResult100;
				#else
				half staticSwitch53 = 1.0;
				#endif
				half4 appendResult88 = (half4(_U1 , _v1 , 0.0 , 0.0));
				half2 uv0_MaskTex = i.ase_texcoord1.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				half2 panner90 = (_Time.y * appendResult88.xy + uv0_MaskTex);
				half4 uv095 = i.ase_texcoord1;
				uv095.xy = i.ase_texcoord1.xy * float2(1,1) + float2(0,0);
				half4 appendResult96 = (half4(0.0 , 0.0 , uv095.z , uv095.w));
				#ifdef _MASKUV_ON
				half4 staticSwitch94 = (half4(uv0_MaskTex, 0.0 , 0.0) + appendResult96);
				#else
				half4 staticSwitch94 = half4(panner90, 0.0 , 0.0);
				#endif
				half4 tex2DNode8 = tex2D(_MaskTex, staticSwitch94.xy);
				#if defined(_MASK_R)
				half staticSwitch9 = tex2DNode8.r;
				#elif defined(_MASK_G)
				half staticSwitch9 = tex2DNode8.g;
				#elif defined(_MASK_B)
				half staticSwitch9 = tex2DNode8.b;
				#elif defined(_MASK_A)
				half staticSwitch9 = tex2DNode8.a;
				#else
				half staticSwitch9 = tex2DNode8.r;
				#endif
				half4 appendResult7 = (half4((_MainColor * staticSwitch98 * i.ase_color * staticSwitch53 * staticSwitch9).rgb , (staticSwitch98 * i.ase_color.a * i.ase_color.a * staticSwitch9 * staticSwitch53).r));


				finalColor = appendResult7;
				return finalColor;
            }
            ENDHLSL
        }
    }
	
	SubShader
	{


		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" "RenderPipeline" = "UniversalPipeline"}
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		Offset 0 , 0

		Pass
		{
			Name "Unlit"
			Tags { "LightMode" = "UniversalForward" }
			HLSLPROGRAM

			#define ASE_ABSOLUTE_VERTEX_POS 1


		#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
		//only defining to not throw compilation error over Unity 5.5
		#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
		#endif
		#pragma vertex vert
		#pragma fragment frag
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

		#define ASE_NEEDS_FRAG_COLOR
		#pragma shader_feature_local _TEX_RGBA _TEX_R _TEX_G _TEX_B _TEX_A
		#pragma shader_feature_local _UVON_ON
		#pragma shader_feature_local _DISSLOVESON_ON
		#pragma shader_feature_local _MASK_R _MASK_G _MASK_B _MASK_A
		#pragma shader_feature_local _MASKUV_ON


		struct appdata
		{
			float4 vertex : POSITION;
			float4 color : COLOR;
			UNITY_VERTEX_INPUT_INSTANCE_ID
			float4 ase_texcoord : TEXCOORD0;
			float4 ase_texcoord1 : TEXCOORD1;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
			#endif
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
			};

			CBUFFER_START(UnityPerMaterial)
			uniform half4 _MainColor;
			uniform sampler2D _Texture;
			uniform half _U;
			uniform half _v;
			uniform half4 _Texture_ST;
			uniform sampler2D _NoiseTex;
			uniform half4 _Vector0;
			uniform half4 _NoiseTex_ST;
			uniform half _NoisePower;
			uniform half _NoiseOpen;
			uniform half _Hardness;
			uniform sampler2D _Dissloves;
			uniform half4 _Dissloves_ST;
			uniform half _Dissolvepower;
			uniform half _Dissove;
			uniform sampler2D _MaskTex;
			uniform half _U1;
			uniform half _v1;
			uniform half4 _MaskTex_ST;
			CBUFFER_END

			v2f vert(appdata v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1 = v.ase_texcoord;
				o.ase_texcoord2 = v.ase_texcoord1;
				o.ase_color = v.color;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = TransformObjectToHClip(v.vertex.xyz);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				half4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half4 appendResult30 = (half4(_U , _v , 0.0 , 0.0));
				half2 uv0_Texture = i.ase_texcoord1.xy * _Texture_ST.xy + _Texture_ST.zw;
				half2 panner81 = (_Time.y * appendResult30.xy + uv0_Texture);
				half4 uv138 = i.ase_texcoord2;
				uv138.xy = i.ase_texcoord2.xy * float2(1,1) + float2(0,0);
				half4 appendResult39 = (half4(uv138.x , uv138.y , 0.0 , 0.0));
				#ifdef _UVON_ON
				half4 staticSwitch44 = (half4(uv0_Texture, 0.0 , 0.0) + appendResult39);
				#else
				half4 staticSwitch44 = half4(panner81, 0.0 , 0.0);
				#endif
				half2 uv0_NoiseTex = i.ase_texcoord1.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
				half2 panner119 = (_Time.y * _Vector0.xy + uv0_NoiseTex);
				half lerpResult168 = lerp(0.0, (tex2D(_NoiseTex, (panner119 * 2.0 + -1.0)).r * _NoisePower), _NoiseOpen);
				half4 tex2DNode4 = tex2D(_Texture, (staticSwitch44 + lerpResult168).xy);
				half4 temp_cast_5 = (tex2DNode4.r).xxxx;
				half4 temp_cast_6 = (tex2DNode4.r).xxxx;
				half4 temp_cast_7 = (tex2DNode4.g).xxxx;
				half4 temp_cast_8 = (tex2DNode4.b).xxxx;
				half4 temp_cast_9 = (tex2DNode4.a).xxxx;
				#if defined(_TEX_RGBA)
				half4 staticSwitch98 = tex2DNode4;
				#elif defined(_TEX_R)
				half4 staticSwitch98 = temp_cast_5;
				#elif defined(_TEX_G)
				half4 staticSwitch98 = temp_cast_7;
				#elif defined(_TEX_B)
				half4 staticSwitch98 = temp_cast_8;
				#elif defined(_TEX_A)
				half4 staticSwitch98 = temp_cast_9;
				#else
				half4 staticSwitch98 = temp_cast_5;
				#endif
				float2 uv_Dissloves = i.ase_texcoord1.xy * _Dissloves_ST.xy + _Dissloves_ST.zw;
				half3 uv1108 = i.ase_texcoord2.xyz;
				uv1108.xy = i.ase_texcoord2.xyz.xy * float2(1,1) + float2(0,0);
				half lerpResult114 = lerp(_Dissolvepower , uv1108.z , _Dissove);
				half smoothstepResult100 = smoothstep(_Hardness , 1.0 , ((tex2D(_Dissloves, uv_Dissloves).r + 1.0) - (lerpResult114 * (1.0 + (1.0 - _Hardness)))));
				#ifdef _DISSLOVESON_ON
				half staticSwitch53 = smoothstepResult100;
				#else
				half staticSwitch53 = 1.0;
				#endif
				half4 appendResult88 = (half4(_U1 , _v1 , 0.0 , 0.0));
				half2 uv0_MaskTex = i.ase_texcoord1.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				half2 panner90 = (_Time.y * appendResult88.xy + uv0_MaskTex);
				half4 uv095 = i.ase_texcoord1;
				uv095.xy = i.ase_texcoord1.xy * float2(1,1) + float2(0,0);
				half4 appendResult96 = (half4(0.0 , 0.0 , uv095.z , uv095.w));
				#ifdef _MASKUV_ON
				half4 staticSwitch94 = (half4(uv0_MaskTex, 0.0 , 0.0) + appendResult96);
				#else
				half4 staticSwitch94 = half4(panner90, 0.0 , 0.0);
				#endif
				half4 tex2DNode8 = tex2D(_MaskTex, staticSwitch94.xy);
				#if defined(_MASK_R)
				half staticSwitch9 = tex2DNode8.r;
				#elif defined(_MASK_G)
				half staticSwitch9 = tex2DNode8.g;
				#elif defined(_MASK_B)
				half staticSwitch9 = tex2DNode8.b;
				#elif defined(_MASK_A)
				half staticSwitch9 = tex2DNode8.a;
				#else
				half staticSwitch9 = tex2DNode8.r;
				#endif
				half4 appendResult7 = (half4((_MainColor * staticSwitch98 * i.ase_color * staticSwitch53 * staticSwitch9).rgb , (staticSwitch98 * i.ase_color.a * i.ase_color.a * staticSwitch9 * staticSwitch53).r));


				finalColor = appendResult7;
				return finalColor;
			}
			ENDHLSL
		}
	}
}