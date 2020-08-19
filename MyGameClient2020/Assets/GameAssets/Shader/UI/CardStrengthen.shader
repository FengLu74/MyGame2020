// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MGame/UI/CardStrengthen"
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
		[HDR]_MainColor("MainColor", Color) = (0.7203201,0.6936417,0,0)
		_Details_POS("Details_POS", Vector) = (0.3,0.15,0,0)
		[HDR]_DetailsColor("DetailsColor", Color) = (1,0.6736357,0,0)
		_Details("Details", 2D) = "white" {}
		_Desaturate("Desaturate", Range( 0 , 1)) = 1
		[Toggle]_StrengthenToggle("StrengthenToggle", Float) = 0
		[Toggle]_DesaturateToggle("DesaturateToggle", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

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
				
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform float _DesaturateToggle;
			uniform float4 _MainTex_ST;
			uniform float4 _DetailsColor;
			uniform sampler2D _Details;
			uniform float2 _Details_POS;
			uniform float4 _MainColor;
			uniform float4 _DissolveColor;
			uniform float _DissolveAmount;
			uniform float _DissolveWidth;
			uniform half _Desaturate;
			uniform float _StrengthenToggle;
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
				
				
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode12 = tex2D( _MainTex, uv_MainTex );
				float cos23 = cos( ( (( _DesaturateToggle )?( 0.0 ):( _Time.y )) * 0.2 ) );
				float sin23 = sin( ( (( _DesaturateToggle )?( 0.0 ):( _Time.y )) * 0.2 ) );
				float2 rotator23 = mul( (IN.texcoord.xy*float2( 2,2 ) + _Details_POS) - float2( 0.5,0.5 ) , float2x2( cos23 , -sin23 , sin23 , cos23 )) + float2( 0.5,0.5 );
				float4 tex2DNode28 = tex2D( _Details, rotator23 );
				float2 uv028_g9 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float simplePerlin2D5_g9 = snoise( uv028_g9*5.0 );
				simplePerlin2D5_g9 = simplePerlin2D5_g9*0.5 + 0.5;
				float2 break10_g9 = float2( -3,0 );
				float temp_output_1_0_g10 = break10_g9.x;
				float temp_output_13_0_g9 = ( ( ( ( simplePerlin2D5_g9 * 0.5 ) + ( uv028_g9.y * -1.0 ) ) - temp_output_1_0_g10 ) / ( break10_g9.y - temp_output_1_0_g10 ) );
				float temp_output_14_0_g9 = step( _DissolveAmount , temp_output_13_0_g9 );
				float4 lerpResult18_g9 = lerp( ( ( ( tex2DNode12 * _DetailsColor ) * ( tex2DNode12 + tex2D( _MainTex, (IN.texcoord.xy*1.0 + float2( 0.34,0 )) ) ) ) + ( ( ( tex2DNode28 * tex2DNode28.a ) * tex2DNode12 ) * _MainColor ) ) , _DissolveColor , ( temp_output_14_0_g9 - step( ( _DissolveAmount + _DissolveWidth ) , temp_output_13_0_g9 ) ));
				float4 temp_output_72_0 = lerpResult18_g9;
				float3 desaturateInitialColor65 = temp_output_72_0.rgb;
				float desaturateDot65 = dot( desaturateInitialColor65, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar65 = lerp( desaturateInitialColor65, desaturateDot65.xxx, _Desaturate );
				float4 appendResult76 = (float4((( _DesaturateToggle )?( float4( desaturateVar65 , 0.0 ) ):( temp_output_72_0 )).rgb , (( _StrengthenToggle )?( 0.0 ):( ( tex2DNode12.a * temp_output_14_0_g9 ) ))));
				
				half4 color = appendResult76;
				
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
1920;0;1920;1019;1185.875;1771.72;1.624385;True;False
Node;AmplifyShaderEditor.TimeNode;20;-126.414,-1322.85;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexCoordVertexDataNode;27;-260.3823,-1088.886;Inherit;True;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;26;17.42019,-1610.149;Inherit;False;Constant;_Scale;Scale;1;0;Create;True;0;0;False;0;False;2,2;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;22;90.44412,-1101.54;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;False;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;25;46.88626,-1467.943;Inherit;False;Property;_Details_POS;Details_POS;5;0;Create;True;0;0;False;0;False;0.3,0.15;-0.3,0.15;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ToggleSwitchNode;71;352.9925,-1086.35;Inherit;False;Property;_DesaturateToggle;DesaturateToggle;8;0;Create;True;0;0;False;0;False;0;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;290.3011,-1339.83;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;24;263.3983,-1629.366;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;29;183.0662,-198.1738;Inherit;False;Constant;_Vector2;Vector 2;2;0;Create;True;0;0;False;0;False;0.34,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RotatorNode;23;670.7021,-1455.726;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;28;1110.133,-1566.925;Inherit;True;Property;_Details;Details;7;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;77;622.3591,-895.6565;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;61;944.4977,-1032.381;Inherit;False;370;280;Comment;1;12;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;19;448.1363,-277.3177;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT2;0.34,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;12;994.4977,-982.3813;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;908.938,-372.9559;Inherit;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;16;1031.902,-635.0297;Inherit;False;Property;_DetailsColor;DetailsColor;6;1;[HDR];Create;True;0;0;False;0;False;1,0.6736357,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;1940.54,-1304.638;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;1870.227,-999.9988;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;2136.239,-1212.938;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;1881.251,-849.9576;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;8;1910.973,-619.7204;Inherit;False;Property;_MainColor;MainColor;4;1;[HDR];Create;True;0;0;False;0;False;0.7203201,0.6936417,0,0;1,0.7599125,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;2137.667,-1035.906;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;2359.795,-937.7596;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1;2428.386,-1154.378;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;72;2737.028,-915.4535;Inherit;False;DissolveBurn;0;;9;767994fa0aba2604992691245c9e7b6e;0;2;25;FLOAT;0;False;24;FLOAT4;0,0,0,0;False;2;FLOAT;26;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;66;2835.77,-751.8402;Half;False;Property;_Desaturate;Desaturate;9;0;Create;True;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;65;3202.034,-835.5139;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ToggleSwitchNode;73;3674.454,-892.7001;Inherit;False;Property;_StrengthenToggle;StrengthenToggle;10;0;Create;True;0;0;False;0;False;0;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;67;3474.601,-1171.257;Inherit;False;Property;_DesaturateToggle;DesaturateToggle;11;0;Create;True;0;0;False;0;False;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;76;4038.711,-790.6859;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;75;4324.256,-1027.377;Float;False;True;-1;2;ASEMaterialInspector;0;4;MGame/UI/CardStrengthen;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;True;0;True;-9;True;True;0;True;-5;255;True;-8;255;True;-7;0;True;-4;0;True;-6;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;0;True;-11;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;71;0;20;2
WireConnection;21;0;71;0
WireConnection;21;1;22;0
WireConnection;24;0;27;0
WireConnection;24;1;26;0
WireConnection;24;2;25;0
WireConnection;23;0;24;0
WireConnection;23;2;21;0
WireConnection;28;1;23;0
WireConnection;19;0;27;0
WireConnection;19;2;29;0
WireConnection;12;0;77;0
WireConnection;18;0;77;0
WireConnection;18;1;19;0
WireConnection;11;0;28;0
WireConnection;11;1;28;4
WireConnection;15;0;12;0
WireConnection;15;1;16;0
WireConnection;9;0;11;0
WireConnection;9;1;12;0
WireConnection;14;0;12;0
WireConnection;14;1;18;0
WireConnection;10;0;15;0
WireConnection;10;1;14;0
WireConnection;2;0;9;0
WireConnection;2;1;8;0
WireConnection;1;0;10;0
WireConnection;1;1;2;0
WireConnection;72;25;12;4
WireConnection;72;24;1;0
WireConnection;65;0;72;0
WireConnection;65;1;66;0
WireConnection;73;0;72;26
WireConnection;67;0;72;0
WireConnection;67;1;65;0
WireConnection;76;0;67;0
WireConnection;76;3;73;0
WireConnection;75;0;76;0
ASEEND*/
//CHKSM=50BB1052107ED0798D59ACD4AAA57A0DDA4247B5