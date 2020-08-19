// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MGame/UI/Ripple"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.48
		_Texture0("Texture 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform sampler2D _Texture0;
		uniform float _Cutoff = 0.48;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = float4(1,1,1,0).rgb;
			o.Alpha = 1;
			float2 uv_TexCoord1 = i.uv_texcoord * float2( 6,1 );
			float2 panner5 = ( 1.0 * _Time.y * float2( 0.3,-0.01 ) + uv_TexCoord1);
			float2 uv_TexCoord2 = i.uv_texcoord * float2( 9,1.83 ) + float2( 0,-3.74 );
			float2 panner6 = ( 1.0 * _Time.y * float2( 0.2,0.05 ) + uv_TexCoord2);
			clip( ( i.vertexColor * ( ( ( tex2D( _Texture0, panner5 ).r + tex2D( _Texture0, panner6 ).g ) - -0.03 ) / ( 0.75 - -0.03 ) ) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
7;237;1116;796;916.9741;93.49905;1.906896;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-207.6691,103.8918;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;6,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-363.078,586.095;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;9,1.83;False;1;FLOAT2;0,-3.74;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;6;222.5336,719.6392;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,0.05;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;5;395.9002,60.56966;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.3,-0.01;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;4;32.29637,301.5087;Float;True;Property;_Texture0;Texture 0;1;0;Create;True;0;0;False;0;88ff92a29724bde42befd0b82319af34;6dd660602be7e9842a79a96e8a8fbb7d;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;3;654.6849,139.94;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;671.2018,405.526;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;1260.375,516.674;Float;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;False;0;0.75;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;1030.773,305.8434;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;1062.375,571.674;Float;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;-0.03;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;1411.375,599.674;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;9;1266.749,402.8494;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;13;1681.474,562.3638;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;15;1468.474,504.3638;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;1838.099,280.5495;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;16;1530.375,-41.73621;Float;False;Constant;_Color0;Color 0;2;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;21;2270.344,182.7494;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;MGame/UI/Ripple;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.48;True;True;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;2;0
WireConnection;5;0;1;0
WireConnection;3;0;4;0
WireConnection;3;1;5;0
WireConnection;7;0;4;0
WireConnection;7;1;6;0
WireConnection;8;0;3;1
WireConnection;8;1;7;2
WireConnection;12;0;11;0
WireConnection;12;1;10;0
WireConnection;9;0;8;0
WireConnection;9;1;10;0
WireConnection;13;0;9;0
WireConnection;13;1;12;0
WireConnection;18;0;15;0
WireConnection;18;1;13;0
WireConnection;21;2;16;0
WireConnection;21;10;18;0
ASEEND*/
//CHKSM=979DF1E846CC1B3C82A3DCF7D93E6B9FAA8469C8