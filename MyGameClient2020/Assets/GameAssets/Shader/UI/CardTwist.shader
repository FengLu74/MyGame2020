// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MGame/UI/CardTwist"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		_DissolveAmount("DissolveAmount", Range( 0 , 2.5)) = 0
		_DissolveWidth("DissolveWidth", Float) = 0.03
		[HDR]_DissolveColor("DissolveColor", Color) = (0,6.27451,8,1)
		_NoiseScale("Noise Scale", Float) = 0.72
		_position("position", Float) = 0
		_world("world", Float) = 0.1
		_Desaturate1("Desaturate", Range( 0 , 1)) = 1
		[Toggle]_DesaturateToggle1("DesaturateToggle", Float) = 0
		[Toggle]_TwistToggle("TwistToggle", Float) = 0

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		
		Stencil
		{
			Ref [_Stencil]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
			CompFront [_StencilComp]
			PassFront [_StencilOp]
			FailFront Keep
			ZFailFront Keep
			CompBack Always
			PassBack Keep
			FailBack Keep
			ZFailBack Keep
		}


		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		
		Pass
		{
			Name "Default"
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_COLOR

			
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
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord2 : TEXCOORD2;
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform float _DesaturateToggle1;
			uniform float _world;
			uniform float _NoiseScale;
			uniform float _position;
			uniform float4 _DissolveColor;
			uniform float _DissolveAmount;
			uniform float _DissolveWidth;
			uniform half _Desaturate1;
			uniform float _TwistToggle;
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID( IN );
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				OUT.worldPosition = IN.vertex;
				float3 ase_worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
				OUT.ase_texcoord2.xyz = ase_worldPos;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				OUT.ase_texcoord2.w = 0;
				
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv014 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult4 = (float2(-0.28 , -0.28));
				float3 ase_worldPos = IN.ase_texcoord2.xyz;
				float2 appendResult23 = (float2(ase_worldPos.x , ase_worldPos.y));
				float2 uv06 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner3 = ( 1.0 * _Time.y * appendResult4 + ( ( appendResult23 * _world ) + ( _NoiseScale * uv06 ) ));
				float4 tex2DNode2 = tex2D( _MainTex, panner3 );
				float2 appendResult37 = (float2(tex2DNode2.r , tex2DNode2.g));
				float4 tex2DNode12 = tex2D( _MainTex, ( uv014 + ( appendResult37 * _position ) ) );
				float2 uv028_g1 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float simplePerlin2D5_g1 = snoise( uv028_g1*5.0 );
				simplePerlin2D5_g1 = simplePerlin2D5_g1*0.5 + 0.5;
				float2 break10_g1 = float2( -3,0 );
				float temp_output_1_0_g2 = break10_g1.x;
				float temp_output_13_0_g1 = ( ( ( ( simplePerlin2D5_g1 * 0.5 ) + ( uv028_g1.y * -1.0 ) ) - temp_output_1_0_g2 ) / ( break10_g1.y - temp_output_1_0_g2 ) );
				float temp_output_14_0_g1 = step( _DissolveAmount , temp_output_13_0_g1 );
				float4 lerpResult18_g1 = lerp( ( IN.color * tex2DNode12 ) , _DissolveColor , ( temp_output_14_0_g1 - step( ( _DissolveAmount + _DissolveWidth ) , temp_output_13_0_g1 ) ));
				float4 temp_output_83_0 = lerpResult18_g1;
				float3 desaturateInitialColor85 = temp_output_83_0.rgb;
				float desaturateDot85 = dot( desaturateInitialColor85, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar85 = lerp( desaturateInitialColor85, desaturateDot85.xxx, _Desaturate1 );
				float4 appendResult90 = (float4((( _DesaturateToggle1 )?( float4( desaturateVar85 , 0.0 ) ):( temp_output_83_0 )).rgb , (( _TwistToggle )?( 0.0 ):( ( tex2DNode12.a * temp_output_14_0_g1 ) ))));
				
				half4 color = appendResult90;
				
				#ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18100
1968;132;1749;861;1509.388;3383.716;1.275925;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;22;-1405.578,-3241.92;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;23;-1207.45,-3219.457;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-1212.398,-3086.688;Float;False;Property;_world;world;6;0;Create;True;0;0;False;0;False;0.1;0.69;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1222.349,-2964.344;Float;False;Property;_NoiseScale;Noise Scale;4;0;Create;True;0;0;False;0;False;0.72;0.72;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1251.185,-2860.084;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-1185.014,-2594.209;Float;False;Constant;_Flame;Flame;2;0;Create;True;0;0;False;0;False;0.28;-0.34;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1014.2,-3187.959;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NegateNode;11;-951.093,-2667.469;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;28;-744.7169,-2997.391;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-854.452,-2905.619;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-676.2457,-2936.426;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;-781.447,-2726.393;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;92;-390.4014,-2919.279;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;3;-551.8916,-2782.65;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-356.2766,-2832.888;Inherit;True;Property;_Noise;Noise;5;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;11.51331,-2556.911;Float;False;Property;_position;position;5;0;Create;True;0;0;False;0;False;0;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;37;19.69194,-2786.818;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;287.9574,-2727.148;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-136.9335,-3049.611;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;38;451.8464,-2774.166;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;91;274.1788,-2977.253;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;607.6924,-2841.48;Inherit;True;Property;_MainTex;MainTex;6;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;29;506.5778,-3186.778;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;980.9615,-3012.256;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;83;1215.845,-2874.579;Inherit;False;DissolveBurn;0;;1;767994fa0aba2604992691245c9e7b6e;0;2;25;FLOAT;0;False;24;FLOAT4;0,0,0,0;False;2;FLOAT;26;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;84;1181.175,-2557.534;Half;False;Property;_Desaturate1;Desaturate;7;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;85;1543.569,-2565.738;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ToggleSwitchNode;86;1816.136,-2841.491;Inherit;False;Property;_DesaturateToggle1;DesaturateToggle;8;0;Create;True;0;0;False;0;False;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;87;1906.129,-2692.394;Inherit;False;Property;_TwistToggle;TwistToggle;9;0;Create;True;0;0;False;0;False;0;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;90;2189.741,-2785.773;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;89;2419.855,-2883.624;Float;False;True;-1;2;ASEMaterialInspector;0;4;MGame/UI/CardTwist;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;True;0;True;-9;True;True;0;True;-5;255;True;-8;255;True;-7;0;True;-4;0;True;-6;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;0;True;-11;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;23;0;22;1
WireConnection;23;1;22;2
WireConnection;24;0;23;0
WireConnection;24;1;25;0
WireConnection;11;0;10;0
WireConnection;28;0;24;0
WireConnection;5;0;7;0
WireConnection;5;1;6;0
WireConnection;26;0;28;0
WireConnection;26;1;5;0
WireConnection;4;0;11;0
WireConnection;4;1;11;0
WireConnection;3;0;26;0
WireConnection;3;2;4;0
WireConnection;2;0;92;0
WireConnection;2;1;3;0
WireConnection;37;0;2;1
WireConnection;37;1;2;2
WireConnection;36;0;37;0
WireConnection;36;1;20;0
WireConnection;38;0;14;0
WireConnection;38;1;36;0
WireConnection;12;0;91;0
WireConnection;12;1;38;0
WireConnection;30;0;29;0
WireConnection;30;1;12;0
WireConnection;83;25;12;4
WireConnection;83;24;30;0
WireConnection;85;0;83;0
WireConnection;85;1;84;0
WireConnection;86;0;83;0
WireConnection;86;1;85;0
WireConnection;87;0;83;26
WireConnection;90;0;86;0
WireConnection;90;3;87;0
WireConnection;89;0;90;0
ASEEND*/
//CHKSM=B6A3D32B0B0C815C8F8805E77E05AAE95C7AC5E6