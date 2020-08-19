// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MGame/3D/Sprites/burning1001004"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_flickerColor("flickerColor", Color) = (0,0,0,0)
		_flickValue("flickValue", Range( 0 , 1)) = 0
		_LiquedHeightY("Liqued HeightY", Range( -1 , 6)) = 3.450066
		_LiquedHeightX("Liqued HeightX", Range( 0 , 5)) = 3.450066
		_Float1("Float 1", Float) = 1
		_Float0("Float 0", Float) = 0.5
		[Toggle(_X1_ON)] _X1("X1", Float) = 0
		[Toggle(_X2_ON)] _X2("X2", Float) = 0
		_xory("x or y", Vector) = (1,0,0,0)
		[Toggle(_X3_ON)] _X3("X3", Float) = 0
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
			#pragma shader_feature _X1_ON
			#pragma shader_feature _X2_ON
			#pragma shader_feature _X3_ON


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
			uniform float3 _xory;
			uniform float _LiquedHeightY;
			uniform float _LiquedHeightX;
			uniform float _Float0;
			uniform float _Float1;
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
				float3 objToWorld214 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 ase_worldPos = IN.ase_texcoord1.xyz;
				float3 temp_output_198_0 = ( objToWorld214 - ase_worldPos );
				#ifdef _X1_ON
				float staticSwitch237 = (temp_output_198_0).y;
				#else
				float staticSwitch237 = (temp_output_198_0).x;
				#endif
				#ifdef _X2_ON
				float staticSwitch240 = (_xory).y;
				#else
				float staticSwitch240 = (_xory).x;
				#endif
				#ifdef _X3_ON
				float staticSwitch244 = _LiquedHeightX;
				#else
				float staticSwitch244 = _LiquedHeightY;
				#endif
				float clampResult213 = clamp( ( ( ( staticSwitch237 + staticSwitch240 ) - ( ( staticSwitch240 * staticSwitch244 ) * _Float0 ) ) / _Float1 ) , 0.0 , 1.0 );
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode139 = tex2D( _MainTex, uv_MainTex );
				float4 temp_output_229_0 = ( IN.color * ( IN.color.a * clampResult213 ) * tex2DNode139 * _flickerColor );
				float4 lerpResult234 = lerp( temp_output_229_0 , ( IN.color * _flickerColor ) , ( _flickValue * _flickerColor.a * tex2DNode139.a * (temp_output_229_0).a ));
				
				fixed4 c = lerpResult234;
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
1927;7;1906;1004;3527.913;1073.924;1;True;False
Node;AmplifyShaderEditor.Vector3Node;202;-2992.477,-616.6511;Float;False;Property;_xory;x or y;9;0;Create;True;0;0;False;0;1,0,0;1,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TransformPositionNode;214;-3178.139,-955.963;Float;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;196;-3172.08,-806.5552;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ComponentMaskNode;203;-2525.893,-586.0013;Float;False;True;False;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;198;-2830.388,-841.9248;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;206;-2819.073,-471.3554;Float;False;Property;_LiquedHeightY;Liqued HeightY;3;0;Create;True;0;0;False;0;3.450066;6;-1;6;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;239;-2536.066,-658.9597;Float;False;False;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;241;-2817.38,-376.9581;Float;False;Property;_LiquedHeightX;Liqued HeightX;4;0;Create;True;0;0;False;0;3.450066;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;238;-2531.376,-952.1533;Float;False;False;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;244;-2413.521,-430.3901;Float;False;Property;_X3;X3;10;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;199;-2518.792,-860.9042;Float;False;True;False;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;240;-2287.38,-710.9581;Float;False;Property;_X2;X2;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;237;-2242.407,-939.9189;Float;False;Property;_X1;X1;7;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;205;-2101.073,-592.3559;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;208;-2144.073,-420.3554;Float;False;Property;_Float0;Float 0;6;0;Create;True;0;0;False;0;0.5;0.56;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;207;-1912.074,-577.3556;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;200;-1948.788,-771.9042;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;211;-1741.074,-522.3554;Float;False;Property;_Float1;Float 1;5;0;Create;True;0;0;False;0;1;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;209;-1716.074,-699.3566;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;210;-1509.074,-747.3566;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;227;-964.0137,-925.0202;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;213;-1251.064,-682.9309;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;139;-807.7072,-503.7717;Float;True;Global;_MainTex;MainTex;1;0;Create;False;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;155;-862.9359,-162.7605;Float;False;Property;_flickerColor;flickerColor;0;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;228;-747.3325,-698.4633;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;229;-107.7208,-879.4827;Float;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;233;107.9437,-513.4561;Float;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;158;-863.0192,-271.5589;Float;False;Property;_flickValue;flickValue;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;235;-241.6264,-417.1576;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;232;-296.3155,-219.6111;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;234;35.98254,-307.3607;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;141;485.7732,-368.799;Float;False;True;2;Float;ASEMaterialInspector;0;6;MGame/3D/Sprites/burning1001004;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;203;0;202;0
WireConnection;198;0;214;0
WireConnection;198;1;196;0
WireConnection;239;0;202;0
WireConnection;238;0;198;0
WireConnection;244;1;206;0
WireConnection;244;0;241;0
WireConnection;199;0;198;0
WireConnection;240;1;203;0
WireConnection;240;0;239;0
WireConnection;237;1;199;0
WireConnection;237;0;238;0
WireConnection;205;0;240;0
WireConnection;205;1;244;0
WireConnection;207;0;205;0
WireConnection;207;1;208;0
WireConnection;200;0;237;0
WireConnection;200;1;240;0
WireConnection;209;0;200;0
WireConnection;209;1;207;0
WireConnection;210;0;209;0
WireConnection;210;1;211;0
WireConnection;213;0;210;0
WireConnection;228;0;227;4
WireConnection;228;1;213;0
WireConnection;229;0;227;0
WireConnection;229;1;228;0
WireConnection;229;2;139;0
WireConnection;229;3;155;0
WireConnection;233;0;229;0
WireConnection;235;0;227;0
WireConnection;235;1;155;0
WireConnection;232;0;158;0
WireConnection;232;1;155;4
WireConnection;232;2;139;4
WireConnection;232;3;233;0
WireConnection;234;0;229;0
WireConnection;234;1;235;0
WireConnection;234;2;232;0
WireConnection;141;0;234;0
ASEEND*/
//CHKSM=0B167EC037809F1059A8864BDC7E1A213A8058AE