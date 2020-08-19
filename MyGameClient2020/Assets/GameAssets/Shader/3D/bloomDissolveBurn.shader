// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MGame/3D/Sprites/burnBloom"
{
	Properties
	{
		_flickerColor("flickerColor", Color) = (0,0,0,0)
		_DisolveGuide("Disolve Guide", 2D) = "white" {}
		_BloomFactor("BloomFactor", Range( 0 , 1)) = 0
		_BurnRamp("Burn Ramp", 2D) = "white" {}
		_Spreadspeed("Spread speed", Float) = 1
		_MainTex("MainTex", 2D) = "white" {}
		_TriggerTime("Trigger Time", Range( -3 , 1.5)) = 0
		_BurnSpeed("Burn Speed", Float) = 1
		_flickValue("flickValue", Range( 0 , 1)) = 0
		_bloomintensity("bloomintensity", Range( 0 , 0.99)) = 0.3
		_bloomintensityALL("bloomintensityALL", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		
		Pass
		{
			
			Name "First"
			CGINCLUDE
			#pragma target 3.0
			ENDCG
			Blend SrcAlpha OneMinusSrcAlpha
			BlendOp Add , Add
			Cull Off
			ColorMask RGB
			ZWrite Off
			ZTest LEqual
			Offset 0 , 0
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
			};

			uniform float _TriggerTime;
			uniform float _Spreadspeed;
			uniform float _BurnSpeed;
			uniform sampler2D _DisolveGuide;
			uniform float4 _DisolveGuide_ST;
			uniform sampler2D _BurnRamp;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float4 _flickerColor;
			uniform float _flickValue;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord.xyz = ase_worldPos;
				
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float3 objToWorld19 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 ase_worldPos = i.ase_texcoord.xyz;
				float clampResult29 = clamp( ( ( _TriggerTime + ( distance( objToWorld19 , ase_worldPos ) * _Spreadspeed ) ) * _BurnSpeed ) , 0.0 , 1.0 );
				float2 uv_DisolveGuide = i.ase_texcoord1.xy * _DisolveGuide_ST.xy + _DisolveGuide_ST.zw;
				float clampResult36 = clamp( (-4.0 + (( (-0.4 + (( 1.0 - clampResult29 ) - 0.0) * (0.4 - -0.4) / (1.0 - 0.0)) + tex2D( _DisolveGuide, uv_DisolveGuide ).r ) - 0.0) * (4.0 - -4.0) / (1.0 - 0.0)) , 0.0 , 1.0 );
				float temp_output_37_0 = ( 1.0 - clampResult36 );
				float2 appendResult38 = (float2(temp_output_37_0 , 0.0));
				float4 tex2DNode40 = tex2D( _BurnRamp, appendResult38 );
				float2 uv_MainTex = i.ase_texcoord1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode41 = tex2D( _MainTex, uv_MainTex );
				float4 temp_output_42_0 = ( temp_output_37_0 * tex2DNode40 * i.ase_color * tex2DNode41 );
				float3 lerpResult48 = lerp( (temp_output_42_0).rgb , (( i.ase_color * _flickerColor )).rgb , _flickValue);
				float4 appendResult84 = (float4(lerpResult48 , ( tex2DNode41.a * (temp_output_42_0).a )));
				
				
				finalColor = appendResult84;
				return finalColor;
			}
			ENDCG
		}

		
		Pass
		{
			Name "Second"
			
			CGINCLUDE
			#pragma target 3.0
			ENDCG
			Blend SrcAlpha One
			BlendOp Min , Min
			Cull Back
			ColorMask A
			ZWrite Off
			ZTest LEqual
			Offset 0 , 0
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
			};

			uniform sampler2D _BurnRamp;
			uniform float _TriggerTime;
			uniform float _Spreadspeed;
			uniform float _BurnSpeed;
			uniform sampler2D _DisolveGuide;
			uniform float4 _DisolveGuide_ST;
			uniform float _bloomintensity;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _BloomFactor;
			uniform float _bloomintensityALL;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord.xyz = ase_worldPos;
				
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float3 objToWorld19 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 ase_worldPos = i.ase_texcoord.xyz;
				float clampResult29 = clamp( ( ( _TriggerTime + ( distance( objToWorld19 , ase_worldPos ) * _Spreadspeed ) ) * _BurnSpeed ) , 0.0 , 1.0 );
				float2 uv_DisolveGuide = i.ase_texcoord1.xy * _DisolveGuide_ST.xy + _DisolveGuide_ST.zw;
				float clampResult36 = clamp( (-4.0 + (( (-0.4 + (( 1.0 - clampResult29 ) - 0.0) * (0.4 - -0.4) / (1.0 - 0.0)) + tex2D( _DisolveGuide, uv_DisolveGuide ).r ) - 0.0) * (4.0 - -4.0) / (1.0 - 0.0)) , 0.0 , 1.0 );
				float temp_output_37_0 = ( 1.0 - clampResult36 );
				float2 appendResult38 = (float2(temp_output_37_0 , 0.0));
				float4 tex2DNode40 = tex2D( _BurnRamp, appendResult38 );
				float grayscale83 = Luminance(tex2DNode40.rgb);
				float temp_output_51_0 = step( grayscale83 , _bloomintensity );
				float temp_output_62_0 = temp_output_51_0;
				float temp_output_63_0 = temp_output_51_0;
				float temp_output_64_0 = temp_output_51_0;
				float2 uv_MainTex = i.ase_texcoord1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode41 = tex2D( _MainTex, uv_MainTex );
				float clampResult12 = clamp( ( tex2DNode40.a * step( 0.01 , ( temp_output_62_0 + temp_output_63_0 + temp_output_64_0 ) ) * _BloomFactor ) , 0.0 , 1.0 );
				float4 appendResult17 = (float4(temp_output_62_0 , temp_output_63_0 , temp_output_64_0 , ( 1.0 - ( tex2DNode41.a * clampResult12 ) )));
				float4 temp_cast_1 = (clampResult36).xxxx;
				float4 lerpResult68 = lerp( appendResult17 , temp_cast_1 , _bloomintensityALL);
				
				
				finalColor = lerpResult68;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17101
-31;284;1906;1004;1013.861;1632.975;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;18;-4901.674,-1336.32;Inherit;False;1838.208;722.9153;world position;7;29;27;26;25;24;22;21;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;20;-4911.354,-928.1121;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TransformPositionNode;19;-4971.108,-1114.849;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DistanceOpNode;21;-4638.2,-1096.898;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-4713.891,-707.1165;Float;False;Property;_Spreadspeed;Spread speed;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-4593.782,-1339.896;Float;False;Property;_TriggerTime;Trigger Time;6;0;Create;True;0;0;False;0;0;1.5;-3;1.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-4355.424,-936.1681;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-4010.274,-901.8357;Float;False;Property;_BurnSpeed;Burn Speed;7;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-3956.944,-1036.225;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-3761.557,-1064.263;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;28;-2890.955,-1276.774;Inherit;False;908.2314;498.3652;Dissolve - Opacity Mask;4;33;32;31;30;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ClampOpNode;29;-3591.329,-1080.38;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;30;-2773.831,-1203.714;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;32;-2517.013,-1228.93;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.4;False;4;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;31;-2613.142,-1028.908;Inherit;True;Property;_DisolveGuide;Disolve Guide;1;0;Create;True;0;0;False;0;None;be5053f8ddc52e24c901a7ede139fd65;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-2243.268,-1220.428;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;34;-1902.042,-1325.438;Inherit;False;1110.841;589.6776;Burn Effect;7;40;39;38;37;36;35;44;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCRemapNode;35;-1881.573,-1240.701;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-4;False;4;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;36;-1676.376,-1269.117;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;37;-1518.572,-1274.561;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;38;-1402.307,-1170.336;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;66;-58.09751,-1059.295;Inherit;False;1430.381;482.137; Emission_alpha;7;17;64;63;62;51;52;83;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;40;-1225.833,-1286.704;Inherit;True;Property;_BurnRamp;Burn Ramp;3;0;Create;True;0;0;False;0;None;429d0caa58073624f9df819a49b9520a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;52;199.4479,-803.5707;Float;False;Property;_bloomintensity;bloomintensity;9;0;Create;True;0;0;False;0;0.3;0.99;0;0.99;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;83;206.6176,-916.5948;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;51;481.4481,-985.5707;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;62;666.332,-877.9155;Inherit;False;True;False;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;65;-347.4395,-317.0267;Inherit;False;1711.131;561.3173; Emission;10;16;15;12;13;14;11;7;10;9;8;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ComponentMaskNode;63;667.2836,-781.0444;Inherit;False;False;True;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;64;650.2836,-700.0444;Inherit;False;False;False;True;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-100.0822,-169.5751;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-6.283232,-264.6751;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;7;205.7168,-251.6751;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;1.716776,33.32477;Float;False;Property;_BloomFactor;BloomFactor;2;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;411.4861,55.35571;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;403.6859,141.1562;Float;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;439.7174,-96.67525;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;12;754.446,-67.9082;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;41;-1119.418,-751.5421;Inherit;True;Property;_MainTex;MainTex;5;0;Create;True;0;0;False;0;None;6621ccd06cd81424683e061b46d8646f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;931.2473,-129.0082;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;16;1091.573,-126.1142;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;69;705.134,-396.7602;Float;False;Property;_bloomintensityALL;bloomintensityALL;10;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;17;975.7317,-755.2756;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.VertexColorNode;39;-1132.631,-1098.951;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-270.4402,-1094.881;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;48;122.3951,-1349.705;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;44;-1112.204,-945.3971;Float;False;Property;_flickerColor;flickerColor;0;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;85;-261.1609,-1253.377;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;84;266.7451,-1199.28;Inherit;False;COLOR;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;68;1269.133,-505.3212;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-667.5994,-671.6656;Float;False;Property;_flickValue;flickValue;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-678.4197,-1323.729;Inherit;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;45;-786.3936,-855.1072;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-486.9467,-1211.373;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;86;-330.0604,-1393.777;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;1017.98,-1342.663;Float;False;True;2;ASEMaterialInspector;0;9;MGame/3D/Sprites/burnBloom;003dfa9c16768d048b74f75c088119d8;True;First;0;0;First;2;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;0;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;True;1;False;-1;1;False;-1;True;False;True;2;False;-1;True;True;True;True;False;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;True;2;0;;0;0;Standard;0;0;2;True;True;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2;1705.112,-577.7363;Float;False;False;2;ASEMaterialInspector;0;9;ASESampleTemplates/DoublePassUnlit;003dfa9c16768d048b74f75c088119d8;True;Second;0;1;Second;2;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;0;True;8;5;False;-1;1;False;-1;0;1;False;-1;0;False;-1;True;4;False;-1;4;False;-1;True;False;True;0;False;-1;True;False;False;False;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;True;2;0;;0;0;Standard;0;0
WireConnection;21;0;19;0
WireConnection;21;1;20;0
WireConnection;24;0;21;0
WireConnection;24;1;22;0
WireConnection;26;0;23;0
WireConnection;26;1;24;0
WireConnection;27;0;26;0
WireConnection;27;1;25;0
WireConnection;29;0;27;0
WireConnection;30;0;29;0
WireConnection;32;0;30;0
WireConnection;33;0;32;0
WireConnection;33;1;31;1
WireConnection;35;0;33;0
WireConnection;36;0;35;0
WireConnection;37;0;36;0
WireConnection;38;0;37;0
WireConnection;40;1;38;0
WireConnection;83;0;40;0
WireConnection;51;0;83;0
WireConnection;51;1;52;0
WireConnection;62;0;51;0
WireConnection;63;0;51;0
WireConnection;64;0;51;0
WireConnection;9;0;62;0
WireConnection;9;1;63;0
WireConnection;9;2;64;0
WireConnection;7;0;8;0
WireConnection;7;1;9;0
WireConnection;11;0;40;4
WireConnection;11;1;7;0
WireConnection;11;2;10;0
WireConnection;12;0;11;0
WireConnection;12;1;13;0
WireConnection;12;2;14;0
WireConnection;15;0;41;4
WireConnection;15;1;12;0
WireConnection;16;0;15;0
WireConnection;17;0;62;0
WireConnection;17;1;63;0
WireConnection;17;2;64;0
WireConnection;17;3;16;0
WireConnection;47;0;41;4
WireConnection;47;1;45;0
WireConnection;48;0;86;0
WireConnection;48;1;85;0
WireConnection;48;2;43;0
WireConnection;85;0;46;0
WireConnection;84;0;48;0
WireConnection;84;3;47;0
WireConnection;68;0;17;0
WireConnection;68;1;36;0
WireConnection;68;2;69;0
WireConnection;42;0;37;0
WireConnection;42;1;40;0
WireConnection;42;2;39;0
WireConnection;42;3;41;0
WireConnection;45;0;42;0
WireConnection;46;0;39;0
WireConnection;46;1;44;0
WireConnection;86;0;42;0
WireConnection;1;0;84;0
WireConnection;2;0;68;0
ASEEND*/
//CHKSM=09FD8E8F25E98B9F8026E953B8D0C1A549B91358