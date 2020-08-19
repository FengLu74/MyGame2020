// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MGame/3D/Sprites/burning2"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_flickerColor("flickerColor", Color) = (0,0,0,0)
		_DisolveGuide("Disolve Guide", 2D) = "white" {}
		_BurnRamp("Burn Ramp", 2D) = "white" {}
		_Spreadspeed("Spread speed", Float) = 1
		_TriggerTime("Trigger Time", Range( -3 , 1.5)) = 0
		_BurnSpeed("Burn Speed", Float) = 1
		_flickValue("flickValue", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord1 : TEXCOORD1;
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float _TriggerTime;
			uniform float _Spreadspeed;
			uniform float _BurnSpeed;
			uniform sampler2D _DisolveGuide;
			uniform float4 _DisolveGuide_ST;
			uniform sampler2D _BurnRamp;
			uniform float4 _MainTex_ST;
			uniform float4 _flickerColor;
			uniform float _flickValue;
			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				float3 ase_worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
				OUT.ase_texcoord1.xyz = ase_worldPos;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				OUT.ase_texcoord1.w = 0;
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float3 objToWorld166 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 ase_worldPos = IN.ase_texcoord1.xyz;
				float clampResult154 = clamp( ( ( _TriggerTime + ( distance( objToWorld166 , ase_worldPos ) * _Spreadspeed ) ) * _BurnSpeed ) , 0.0 , 1.0 );
				float2 uv_DisolveGuide = IN.texcoord.xy * _DisolveGuide_ST.xy + _DisolveGuide_ST.zw;
				float clampResult113 = clamp( (-40.0 + (( (-0.4 + (( 1.0 - clampResult154 ) - 0.0) * (0.4 - -0.4) / (1.0 - 0.0)) + tex2D( _DisolveGuide, uv_DisolveGuide ).r ) - 0.0) * (40.0 - -40.0) / (1.0 - 0.0)) , 0.0 , 1.0 );
				float temp_output_130_0 = ( 1.0 - clampResult113 );
				float2 appendResult115 = (float2(temp_output_130_0 , 0.0));
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode139 = tex2D( _MainTex, uv_MainTex );
				float4 temp_output_126_0 = ( temp_output_130_0 * tex2D( _BurnRamp, appendResult115 ) * IN.color * tex2DNode139 );
				float4 lerpResult159 = lerp( temp_output_126_0 , ( IN.color * _flickerColor ) , ( tex2DNode139.a * _flickValue * (temp_output_126_0).a ));
				
				fixed4 c = lerpResult159;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16800
1998;-11;1484;781;2208.694;780.2897;1.954595;True;False
Node;AmplifyShaderEditor.CommentaryNode;157;-3881.931,-48.26821;Float;False;1838.208;722.9153;world position;10;154;153;148;150;149;146;147;145;144;166;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TransformPositionNode;166;-3951.365,173.203;Float;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;144;-3891.61,359.9395;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DistanceOpNode;145;-3618.457,191.154;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;147;-3694.148,580.9351;Float;False;Property;_Spreadspeed;Spread speed;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;149;-3574.039,-51.84393;Float;False;Property;_TriggerTime;Trigger Time;5;0;Create;True;0;0;False;0;0;-0.44;-3;1.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;146;-3335.681,351.8835;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;150;-2990.531,386.2159;Float;False;Property;_BurnSpeed;Burn Speed;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;148;-2937.201,251.827;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;153;-2741.814,223.7896;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;128;-1871.211,11.27778;Float;False;908.2314;498.3652;Dissolve - Opacity Mask;4;71;2;73;111;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ClampOpNode;154;-2571.586,207.6718;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;71;-1754.086,84.3377;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1593.397,259.1438;Float;True;Property;_DisolveGuide;Disolve Guide;1;0;Create;True;0;0;False;0;None;be5053f8ddc52e24c901a7ede139fd65;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;111;-1497.269,59.12226;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.4;False;4;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;73;-1223.523,67.62424;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;129;-882.2972,-37.38573;Float;False;1110.841;589.6776;Burn Effect - Emission;7;113;115;114;130;112;137;139;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCRemapNode;112;-856.3851,31.02139;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-40;False;4;FLOAT;40;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;113;-656.632,18.93485;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;130;-498.8279,13.4911;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;115;-382.5628,117.7162;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;137;-112.8858,189.1012;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;114;-206.0883,1.347903;Float;True;Property;_BurnRamp;Burn Ramp;2;0;Create;True;0;0;False;0;None;429d0caa58073624f9df819a49b9520a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;139;-175.5853,348.6272;Float;True;Global;_MainTex;MainTex;3;0;Create;False;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;341.3245,-35.67737;Float;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;158;-124.9536,777.5856;Float;False;Property;_flickValue;flickValue;7;0;Create;True;0;0;False;0;0;0.189;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;155;-115.0236,586.4132;Float;False;Property;_flickerColor;flickerColor;0;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;167;318.4092,246.2134;Float;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;156;415.4311,448.9406;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;160;402.9829,615.3478;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;159;668.5773,339.5311;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;141;1078.246,83.05642;Float;False;True;2;Float;ASEMaterialInspector;0;6;MGame/3D/Sprites/burning2;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;145;0;166;0
WireConnection;145;1;144;0
WireConnection;146;0;145;0
WireConnection;146;1;147;0
WireConnection;148;0;149;0
WireConnection;148;1;146;0
WireConnection;153;0;148;0
WireConnection;153;1;150;0
WireConnection;154;0;153;0
WireConnection;71;0;154;0
WireConnection;111;0;71;0
WireConnection;73;0;111;0
WireConnection;73;1;2;1
WireConnection;112;0;73;0
WireConnection;113;0;112;0
WireConnection;130;0;113;0
WireConnection;115;0;130;0
WireConnection;114;1;115;0
WireConnection;126;0;130;0
WireConnection;126;1;114;0
WireConnection;126;2;137;0
WireConnection;126;3;139;0
WireConnection;167;0;126;0
WireConnection;156;0;137;0
WireConnection;156;1;155;0
WireConnection;160;0;139;4
WireConnection;160;1;158;0
WireConnection;160;2;167;0
WireConnection;159;0;126;0
WireConnection;159;1;156;0
WireConnection;159;2;160;0
WireConnection;141;0;159;0
ASEEND*/
//CHKSM=05523B873F380D6341998F546235010B84D917EE