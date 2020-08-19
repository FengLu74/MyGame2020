// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MGame/3D/meshrenderGrass"
{
	Properties
	{
		[NoScaleOffset]_MainTex("Main Texture", 2D) = "white" {}
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
		_Color("Color", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
	LOD 0

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
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_VERT_COLOR
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
#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
#endif
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord1 : TEXCOORD1;
			};

			uniform half _MotionSpacee;
			uniform half _MotionSpaceeeEnd;
			uniform half _MotionSpaceeEnd;
			uniform half _MotionSpaceee;
			uniform half ADS_GlobalScale;
			uniform half _MotionScale;
			uniform half ADS_GlobalSpeed;
			uniform half _MotionSpeed;
			uniform half ADS_GlobalAmplitude;
			uniform half _MotionAmplitude;
			uniform sampler2D ADS_NoiseTex;
			uniform half ADS_NoiseSpeed;
			uniform half3 _Vector0;
			uniform half ADS_NoiseScale;
			uniform half ADS_NoiseContrast;
			uniform half3 ADS_GlobalDirection;
			uniform half3 _MotionOffset;
			uniform half3 _LocalDirection;
			uniform half _Float3;
			uniform half _Float5;
			uniform sampler2D _TextureSample0;
			uniform half4 _Vector1;
			uniform float4 _Color;
			uniform sampler2D _MainTex;

			
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
				float2 appendResult1144 = (float2(ase_worldPos.x , ase_worldPos.z));
				float2 panner1143 = ( _Time.y * ( ADS_NoiseSpeed * (-_Vector0).xz ) + ( appendResult1144 * ADS_NoiseScale ));
				half MotionlAmplitude58_g986 = ( ADS_GlobalAmplitude * _MotionAmplitude * saturate( pow( abs( tex2Dlod( ADS_NoiseTex, float4( panner1143, 0, 0.0) ).r ) , ADS_NoiseContrast ) ) );
				#if defined(_MOTIONSPACE_WORLD)
				float3 staticSwitch214_g986 = ( ADS_GlobalDirection + _MotionOffset );
				#elif defined(_MOTIONSPACE_LOCAL)
				float3 staticSwitch214_g986 = _LocalDirection;
				#else
				float3 staticSwitch214_g986 = ( ADS_GlobalDirection + _MotionOffset );
				#endif
				half3 MotionDirection59_g986 = staticSwitch214_g986;
				float lerpResult1133 = lerp( 0.0 , 1.0 , saturate( v.color.r ));
				half MotionMask137_g986 = lerpResult1133;
				float3 temp_output_94_0_g986 = ( ( ( ( sin( ( ( ( staticSwitch216_g986 * MotionScale60_g986 ) + mulTime90_g986 ) + ( v.color.g * 1.756 ) ) ) * MotionlAmplitude58_g986 ) + ( MotionlAmplitude58_g986 * saturate( MotionScale60_g986 ) ) ) * MotionDirection59_g986 ) * MotionMask137_g986 );
				#if defined(_MOTIONSPACE_WORLD)
				float3 staticSwitch215_g986 = mul( unity_WorldToObject, float4( temp_output_94_0_g986 , 0.0 ) ).xyz;
				#elif defined(_MOTIONSPACE_LOCAL)
				float3 staticSwitch215_g986 = temp_output_94_0_g986;
				#else
				float3 staticSwitch215_g986 = mul( unity_WorldToObject, float4( temp_output_94_0_g986 , 0.0 ) ).xyz;
				#endif
				float lerpResult1165 = lerp( _Float3 , _Float5 , tex2Dlod( _TextureSample0, float4( ( ( (ase_worldPos).xz * (_Vector1).xy ) + (_Vector1).zw ), 0, 0.0) ).r);
				
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = ( staticSwitch215_g986 + ( lerpResult1165 * v.ase_texcoord3.xyz ) );
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
#endif
				float2 uv_MainTex18 = i.ase_texcoord1.xy;
				
				
				finalColor = ( _Color * tex2D( _MainTex, uv_MainTex18 ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18100
148;364;1920;835;2661.249;4662.492;1.698609;True;False
Node;AmplifyShaderEditor.CommentaryNode;1181;-3352.57,-3715.276;Inherit;False;2081.003;544;model Noise;16;1155;1154;1146;1140;1159;1147;1144;1138;1139;1152;1143;1158;1151;1150;1156;1149;Noise;1,0.8402693,0.1745283,1;0;0
Node;AmplifyShaderEditor.Vector3Node;1155;-3302.57,-3409.276;Half;False;Global;_Vector0;Vector 0;1;0;Create;True;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;1146;-3302.57,-3665.276;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NegateNode;1154;-3046.57,-3345.276;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1147;-2918.57,-3441.276;Half;False;Global;ADS_NoiseSpeed;ADS_NoiseSpeed;6;0;Create;True;0;0;False;0;False;0.05;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1140;-2918.57,-3537.276;Half;False;Global;ADS_NoiseScale;ADS_NoiseScale;6;0;Create;True;0;0;False;0;False;0.05;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1144;-3046.57,-3665.276;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;1180;-2770,-2915.045;Inherit;False;1771.591;505.0452;size;13;1167;1162;1178;1163;1171;1160;1169;1168;1161;1172;1165;1166;1176;Size;0.997499,0,1,1;0;0
Node;AmplifyShaderEditor.ComponentMaskNode;1159;-2918.57,-3345.276;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1138;-2662.57,-3665.276;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1139;-2662.57,-3441.276;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;1152;-2662.57,-3281.276;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1167;-2720,-2864;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector4Node;1162;-2720,-2704;Half;False;Global;_Vector1;Vector 1;1;0;Create;True;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;1143;-2470.57,-3665.276;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwizzleNode;1163;-2400,-2704;Inherit;False;FLOAT2;0;1;2;2;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwizzleNode;1178;-2400,-2864;Inherit;False;FLOAT2;0;2;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1158;-2150.568,-3665.276;Inherit;True;Global;ADS_NoiseTex;ADS_NoiseTex;0;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwizzleNode;1171;-2400,-2624;Inherit;False;FLOAT2;2;3;2;2;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;1179;-2407.614,-4302.085;Inherit;False;697.3455;377.3496;read model vertex color;5;1137;1134;1136;1135;1133;model vertex color;0.1079052,0.990566,0,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1160;-2208,-2864;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.AbsOpNode;1150;-1814.57,-3553.276;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1151;-2150.568,-3409.276;Half;False;Global;ADS_NoiseContrast;ADS_NoiseContrast;4;0;Create;False;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1169;-2016,-2864;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;1137;-2357.614,-4126.735;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;1156;-1638.57,-3489.276;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1161;-1824,-2768;Half;False;Global;_Float5;Float 5;4;0;Create;False;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1168;-1824,-2864;Half;False;Global;_Float3;Float 3;4;0;Create;False;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1136;-2094.268,-4252.085;Half;False;Constant;_Float0;Float 0;21;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1172;-1824,-2640;Inherit;True;Global;_TextureSample0;Texture Sample 0;1;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1134;-2093.268,-4158.081;Half;False;Constant;_Float4;Float 4;21;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;1135;-2090.268,-4082.079;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1165;-1473.517,-2865.045;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;1149;-1446.57,-3489.276;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;1166;-1477.028,-2655.291;Inherit;False;3;3;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;1133;-1894.268,-4173.081;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-1259.903,-4254.689;Inherit;True;Property;_MainTex;Main Texture;2;1;[NoScaleOffset];Create;False;0;0;False;0;False;-1;None;93282040d5b17004f95e510d0c3689df;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1182;-876.8958,-4409.373;Inherit;False;Property;_Color;Color;14;0;Create;True;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1176;-1167.409,-2856.269;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;1131;-1002.765,-3633.002;Inherit;False;ADS Motion Global;3;;986;a8838de3869103540a427ac470da4da6;0;3;136;FLOAT;0;False;133;FLOAT;0;False;218;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1095;-649.9716,-3571.287;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1183;-349.4136,-4210.862;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1125;-188.5463,-3718.455;Float;False;True;-1;2;ASEMaterialInspector;0;1;MGame/3D/meshrenderGrass;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;2;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;False;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
WireConnection;1154;0;1155;0
WireConnection;1144;0;1146;1
WireConnection;1144;1;1146;3
WireConnection;1159;0;1154;0
WireConnection;1138;0;1144;0
WireConnection;1138;1;1140;0
WireConnection;1139;0;1147;0
WireConnection;1139;1;1159;0
WireConnection;1143;0;1138;0
WireConnection;1143;2;1139;0
WireConnection;1143;1;1152;0
WireConnection;1163;0;1162;0
WireConnection;1178;0;1167;0
WireConnection;1158;1;1143;0
WireConnection;1171;0;1162;0
WireConnection;1160;0;1178;0
WireConnection;1160;1;1163;0
WireConnection;1150;0;1158;1
WireConnection;1169;0;1160;0
WireConnection;1169;1;1171;0
WireConnection;1156;0;1150;0
WireConnection;1156;1;1151;0
WireConnection;1172;1;1169;0
WireConnection;1135;0;1137;1
WireConnection;1165;0;1168;0
WireConnection;1165;1;1161;0
WireConnection;1165;2;1172;0
WireConnection;1149;0;1156;0
WireConnection;1133;0;1136;0
WireConnection;1133;1;1134;0
WireConnection;1133;2;1135;0
WireConnection;1176;0;1165;0
WireConnection;1176;1;1166;0
WireConnection;1131;136;1133;0
WireConnection;1131;133;1149;0
WireConnection;1095;0;1131;0
WireConnection;1095;1;1176;0
WireConnection;1183;0;1182;0
WireConnection;1183;1;18;0
WireConnection;1125;0;1183;0
WireConnection;1125;1;1095;0
ASEEND*/
//CHKSM=7D2D8A4191D13198ACBF4EB54EB79C6972A160E8