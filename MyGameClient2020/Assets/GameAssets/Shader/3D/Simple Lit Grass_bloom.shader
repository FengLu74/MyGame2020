// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MGame/3D/meshrenderGrass_bloom"
{
	Properties
	{
		[Enum(Vertex Red,0,ADS Object,1)][Space(10)]_MaskType("Mask Type", Float) = 0
		[BInteractive(_MaskType, 1)]_MaskTypee("# MaskTypee", Float) = 0
		[Enum(X Axis,0,Y Axis,1,Z Axis,2)]_MaskAxis("Mask Axis", Float) = 1
		[BInteractive(ON)]_MaskTypeeEnd("# MaskTypee End", Float) = 0
		[Space(10)]_MaskMin("Mask Min", Float) = 0
		_MaskMax("Mask Max", Float) = 1
		[HideInInspector]_Show_MaskGeneric("Show_MaskGeneric", Float) = 1
		[Toggle]_MotionNoise("Motion Noise", Float) = 1
		[BBanner(ADS Simple Lit, Grass)]_ADSSimpleLitGrass("< ADS Simple Lit Grass >", Float) = 1
		[Toggle]_GrassSize("Grass Size", Float) = 1
		[BCategory(Surface)]_SURFACEE("[ SURFACEE ]", Float) = 0
		[Enum(Opaque,0,Cutout,1,Fade,2,Transparent,3)]_Mode("Blend Mode", Float) = 0
		[Enum(Off,0,Front,1,Back,2)]_CullMode("Cull Mode", Float) = 0
		[BInteractive(_Mode, 1)]_Modee("# _Modee", Float) = 0
		_Cutoff("Cutout", Range( 0 , 1)) = 0.5
		[BCategory(Main)]_MAINN("[ MAINN ]", Float) = 0
		[NoScaleOffset]_MainTex("Main Texture", 2D) = "white" {}
		_texture_light("texture_light", 2D) = "white" {}
		_light("light", Range( 0 , 1)) = 0
		[Space(10)]_MainUVs("Main UVs", Vector) = (1,1,0,0)
		[BCategory(Globals)]_GLOBALSS("[ GLOBALSS ]", Float) = 0
		[BCategory(Motion)]_MOTIONN("[ MOTIONN ]", Float) = 0
		_MotionAmplitude("Motion Amplitude", Float) = 1
		_MotionSpeed("Motion Speed", Float) = 1
		_MotionScale("Motion Scale", Float) = 1
		[BInteractive(_MotionSpace, 0)]_MotionSpaceee("# MotionSpaceee", Float) = 0
		_MotionOffset("Motion Offset", Vector) = (0,0,0,0)
		[BInteractive(ON)]_MotionSpaceeeEnd("# MotionSpaceee End", Float) = 0
		[KeywordEnum(World,Local)] _MotionSpace("Motion Space", Float) = 0
		[BInteractive(_MotionSpace, 1)]_MotionSpacee("# MotionSpacee", Float) = 0
		_LocalDirection("Motion Local Direction", Vector) = (0,0,1,0)
		[BInteractive(ON)]_MotionSpaceeEnd("# MotionSpacee End", Float) = 0
		[HideInInspector]_ZWrite("_ZWrite", Float) = 1
		[HideInInspector]_SrcBlend("_SrcBlend", Float) = 1
		[HideInInspector]_DstBlend("_DstBlend", Float) = 10
		[BCategory(Advanced)]_ADVANCEDD("[ ADVANCEDD ]", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" }
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Back
		ColorMask RGB
		ZWrite Off
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#pragma shader_feature _MOTIONSPACE_WORLD _MOTIONSPACE_LOCAL


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform half _Show_MaskGeneric;
			uniform half _MaskTypeeEnd;
			uniform half _MaskTypee;
			uniform half _MotionSpacee;
			uniform half _MotionSpaceee;
			uniform half _MotionSpaceeeEnd;
			uniform half _MotionSpaceeEnd;
			uniform half _ADVANCEDD;
			uniform half _ADSSimpleLitGrass;
			uniform half _ZWrite;
			uniform half _Mode;
			uniform half _SrcBlend;
			uniform half _MOTIONN;
			uniform half _Modee;
			uniform half _GLOBALSS;
			uniform half _DstBlend;
			uniform half _SURFACEE;
			uniform half _Cutoff;
			uniform half _CullMode;
			uniform half _MAINN;
			uniform half ADS_GlobalScale;
			uniform half _MotionScale;
			uniform half ADS_GlobalSpeed;
			uniform half _MotionSpeed;
			uniform half ADS_GlobalAmplitude;
			uniform half _MotionAmplitude;
			uniform half ADS_NoiseTex_ON;
			uniform float _MotionNoise;
			uniform sampler2D ADS_NoiseTex;
			uniform half ADS_NoiseSpeed;
			uniform half3 ADS_GlobalDirection;
			uniform half ADS_NoiseScale;
			uniform half ADS_NoiseContrast;
			uniform half3 _MotionOffset;
			uniform half3 _LocalDirection;
			uniform half _MaskAxis;
			uniform half _MaskType;
			uniform half _MaskMin;
			uniform half _MaskMax;
			uniform half ADS_GrassSizeTex_ON;
			uniform half _GrassSize;
			uniform half ADS_GrassSizeMin;
			uniform half ADS_GrassSizeMax;
			uniform sampler2D ADS_GrassSizeTex;
			uniform half4 ADS_GrassSizeScaleOffset;
			uniform sampler2D _MainTex;
			uniform half4 _MainUVs;
			uniform sampler2D _texture_light;
			uniform float4 _texture_light_ST;
			uniform float _light;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#if defined(_MOTIONSPACE_WORLD)
				float3 staticSwitch216_g986 = ase_worldPos;
				#elif defined(_MOTIONSPACE_LOCAL)
				float3 staticSwitch216_g986 = v.vertex.xyz;
				#else
				float3 staticSwitch216_g986 = ase_worldPos;
				#endif
				half MotionScale60_g986 = ( ADS_GlobalScale * _MotionScale );
				half MotionSpeed62_g986 = ( ADS_GlobalSpeed * _MotionSpeed );
				float mulTime90_g986 = _Time.y * MotionSpeed62_g986;
				float2 appendResult115_g981 = (float2(ase_worldPos.x , ase_worldPos.z));
				float2 panner73_g981 = ( _Time.y * ( ADS_NoiseSpeed * (-ADS_GlobalDirection).xz ) + ( appendResult115_g981 * ADS_NoiseScale ));
				float ifLocalVar94_g981 = 0;
				UNITY_BRANCH 
				if( ( ADS_NoiseTex_ON * _MotionNoise ) > 0.01 )
				ifLocalVar94_g981 = saturate( pow( abs( tex2Dlod( ADS_NoiseTex, float4( panner73_g981, 0, 0.0) ).r ) , ADS_NoiseContrast ) );
				else if( ( ADS_NoiseTex_ON * _MotionNoise ) < 0.01 )
				ifLocalVar94_g981 = 1.0;
				half MotionlAmplitude58_g986 = ( ADS_GlobalAmplitude * _MotionAmplitude * ifLocalVar94_g981 );
				#if defined(_MOTIONSPACE_WORLD)
				float3 staticSwitch214_g986 = ( ADS_GlobalDirection + _MotionOffset );
				#elif defined(_MOTIONSPACE_LOCAL)
				float3 staticSwitch214_g986 = _LocalDirection;
				#else
				float3 staticSwitch214_g986 = ( ADS_GlobalDirection + _MotionOffset );
				#endif
				half3 MotionDirection59_g986 = staticSwitch214_g986;
				float temp_output_25_0_g983 = _MaskAxis;
				float lerpResult24_g983 = lerp( v.ase_texcoord3.x , v.ase_texcoord3.y , saturate( temp_output_25_0_g983 ));
				float lerpResult21_g983 = lerp( lerpResult24_g983 , v.ase_texcoord3.z , step( 2.0 , temp_output_25_0_g983 ));
				half THREE27_g983 = lerpResult21_g983;
				float lerpResult42_g982 = lerp( v.color.r , THREE27_g983 , _MaskType);
				float temp_output_7_0_g984 = _MaskMin;
				float lerpResult31_g982 = lerp( 0.0 , 1.0 , saturate( ( ( lerpResult42_g982 - temp_output_7_0_g984 ) / ( _MaskMax - temp_output_7_0_g984 ) ) ));
				half MotionMask137_g986 = lerpResult31_g982;
				float3 temp_output_94_0_g986 = ( ( ( ( sin( ( ( ( staticSwitch216_g986 * MotionScale60_g986 ) + mulTime90_g986 ) + ( v.color.g * 1.756 ) ) ) * MotionlAmplitude58_g986 ) + ( MotionlAmplitude58_g986 * saturate( MotionScale60_g986 ) ) ) * MotionDirection59_g986 ) * MotionMask137_g986 );
				#if defined(_MOTIONSPACE_WORLD)
				float3 staticSwitch215_g986 = mul( unity_WorldToObject, float4( temp_output_94_0_g986 , 0.0 ) ).xyz;
				#elif defined(_MOTIONSPACE_LOCAL)
				float3 staticSwitch215_g986 = temp_output_94_0_g986;
				#else
				float3 staticSwitch215_g986 = mul( unity_WorldToObject, float4( temp_output_94_0_g986 , 0.0 ) ).xyz;
				#endif
				float lerpResult116_g985 = lerp( ADS_GrassSizeMin , ADS_GrassSizeMax , tex2Dlod( ADS_GrassSizeTex, float4( ( ( (ase_worldPos).xz * (ADS_GrassSizeScaleOffset).xy ) + (ADS_GrassSizeScaleOffset).zw ), 0, 0.0) ).r);
				float3 temp_cast_5 = (0.0).xxx;
				float3 ifLocalVar96_g985 = 0;
				UNITY_BRANCH 
				if( ( ADS_GrassSizeTex_ON * _GrassSize ) > 0.5 )
				ifLocalVar96_g985 = ( lerpResult116_g985 * v.ase_texcoord3.xyz );
				else if( ( ADS_GrassSizeTex_ON * _GrassSize ) < 0.5 )
				ifLocalVar96_g985 = temp_cast_5;
				
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = ( staticSwitch215_g986 + ifLocalVar96_g985 );
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				float2 appendResult564 = (float2(_MainUVs.x , _MainUVs.y));
				float2 appendResult565 = (float2(_MainUVs.z , _MainUVs.w));
				half2 MainUVs587 = ( ( i.ase_texcoord.xy * appendResult564 ) + appendResult565 );
				float4 tex2DNode18 = tex2D( _MainTex, MainUVs587 );
				float2 uv_texture_light = i.ase_texcoord.xy * _texture_light_ST.xy + _texture_light_ST.zw;
				float4 appendResult1148 = (float4(( tex2DNode18 + ( tex2D( _texture_light, uv_texture_light ) * _light ) ).rgb , tex2DNode18.a));
				half4 MainTex487 = appendResult1148;
				
				
				finalColor = MainTex487;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17101
2804;137;1906;1004;702.9394;3540.758;1;True;False
Node;AmplifyShaderEditor.Vector4Node;563;-1641.903,-2956.959;Half;False;Property;_MainUVs;Main UVs;25;0;Create;True;0;0;False;1;Space(10);1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexCoordVertexDataNode;561;-1641.903,-3180.959;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;564;-1385.904,-2956.959;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;565;-1385.904,-2876.959;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;562;-1193.904,-3180.959;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;575;-985.9033,-3180.959;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;587;-809.9033,-3180.959;Half;False;MainUVs;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;588;-610.3357,-3186.152;Inherit;False;587;MainUVs;1;0;OBJECT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1135;-333.9202,-2739.865;Inherit;False;Property;_light;light;24;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1134;-435.9202,-2955.865;Inherit;True;Property;_texture_light;texture_light;23;0;Create;True;0;0;False;0;None;3917852a56aca48449b4f61807d1e748;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1136;146.7461,-2916.212;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;18;-359.7941,-3174.469;Inherit;True;Property;_MainTex;Main Texture;22;1;[NoScaleOffset];Create;False;0;0;False;0;None;24de31cdb9a219c4b9ad1f7e9a80b6ae;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;1132;332.0798,-3082.865;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;1130;-1293.915,-3635.861;Inherit;False;ADS Mask Generic;0;;982;2cfc3815568565c4585aebb38bd7a29b;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1128;-1296.123,-3481.053;Inherit;False;ADS Motion Noise;8;;981;047eb809542f42d40b4b5066e22cee72;0;0;1;FLOAT;85
Node;AmplifyShaderEditor.DynamicAppendNode;1148;510.0606,-2961.758;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;487;687.0965,-3118.959;Half;False;MainTex;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.FunctionNode;1131;-1002.765,-3633.002;Inherit;False;ADS Motion Global;28;;986;a8838de3869103540a427ac470da4da6;0;3;136;FLOAT;0;False;133;FLOAT;0;False;218;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;1129;-988.2303,-3479.821;Inherit;False;ADS Grass Size;12;;985;6675a46c54a0e244fb369c824eead1af;0;0;1;FLOAT3;85
Node;AmplifyShaderEditor.RangedFloatNode;743;-640.0001,-3819.941;Half;False;Property;_CullMode;Cull Mode;17;1;[Enum];Create;True;3;Off;0;Front;1;Back;2;0;True;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;862;-128,-3819.941;Half;False;Property;_Cutoff;Cutout;19;0;Create;False;3;Off;0;Front;1;Back;2;0;True;0;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1119;-576,-4128;Half;False;Property;_ADVANCEDD;[ ADVANCEDD ];42;0;Create;True;0;0;True;1;BCategory(Advanced);0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1118;-752,-4128;Half;False;Property;_MOTIONN;[ MOTIONN ];27;0;Create;True;0;0;True;1;BCategory(Motion);0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;409;907.0966,-3161.959;Half;False;Property;_Color;Main Color;21;0;Create;False;0;0;False;0;1,1,1,1;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;553;-1120,-3819.941;Half;False;Property;_DstBlend;_DstBlend;41;1;[HideInInspector];Create;True;0;0;True;0;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1117;-928,-4128;Half;False;Property;_GLOBALSS;[ GLOBALSS ];26;0;Create;True;0;0;True;1;BCategory(Globals);0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1057;1163.097,-3065.96;Half;False;MainColorAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1084;-1292.546,-3721.455;Inherit;False;487;MainTex;1;0;OBJECT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;925;-384,-3819.941;Half;False;Property;_ZWrite;_ZWrite;39;1;[HideInInspector];Create;True;2;Off;0;On;1;0;True;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1116;-1088,-4128;Half;False;Property;_MAINN;[ MAINN ];20;0;Create;True;0;0;True;1;BCategory(Main);0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;616;217.5729,-2695.498;Half;False;MainTexAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;549;-896.0001,-3819.941;Half;False;Property;_Mode;Blend Mode;16;1;[Enum];Create;False;4;Opaque;0;Cutout;1;Fade;2;Transparent;3;0;True;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1113;-1280,-4128;Half;False;Property;_SURFACEE;[ SURFACEE ];15;0;Create;True;0;0;True;1;BCategory(Surface);0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;550;-1280,-3819.941;Half;False;Property;_SrcBlend;_SrcBlend;40;1;[HideInInspector];Create;True;0;0;True;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1114;-1280,-4224;Half;False;Property;_ADSSimpleLitGrass;< ADS Simple Lit Grass >;11;0;Create;True;0;0;True;1;BBanner(ADS Simple Lit, Grass);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;486;1163.097,-3161.959;Half;False;MainColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1095;-649.9716,-3571.287;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1115;-1280,-4032;Half;False;Property;_Modee;# _Modee;18;0;Create;True;0;0;True;1;BInteractive(_Mode, 1);0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1125;-188.5463,-3718.455;Float;False;True;2;ASEMaterialInspector;0;1;MGame/3D/meshrenderGrass_bloom;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;2;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;False;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Transparent=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;0
Node;AmplifyShaderEditor.CommentaryNode;712;-1641.903,-3308.96;Inherit;False;1022.348;100;Main UVs;0;;0.4980392,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1112;-1280,-4352;Inherit;False;920.27;100;Drawers;0;;1,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;683;-1280,-3947.941;Inherit;False;1407.459;100;Rendering;0;;1,0,0.503,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;760;-489.9032,-3308.96;Inherit;False;1352.083;100;Main Texture and Color;0;;0,0.751724,1,1;0;0
WireConnection;564;0;563;1
WireConnection;564;1;563;2
WireConnection;565;0;563;3
WireConnection;565;1;563;4
WireConnection;562;0;561;0
WireConnection;562;1;564;0
WireConnection;575;0;562;0
WireConnection;575;1;565;0
WireConnection;587;0;575;0
WireConnection;1136;0;1134;0
WireConnection;1136;1;1135;0
WireConnection;18;1;588;0
WireConnection;1132;0;18;0
WireConnection;1132;1;1136;0
WireConnection;1148;0;1132;0
WireConnection;1148;3;18;4
WireConnection;487;0;1148;0
WireConnection;1131;136;1130;0
WireConnection;1131;133;1128;85
WireConnection;1057;0;409;4
WireConnection;616;0;18;4
WireConnection;486;0;409;0
WireConnection;1095;0;1131;0
WireConnection;1095;1;1129;85
WireConnection;1125;0;1084;0
WireConnection;1125;1;1095;0
ASEEND*/
//CHKSM=8FA1EE7B941AE53F9D03496464E3E239D18ED33C