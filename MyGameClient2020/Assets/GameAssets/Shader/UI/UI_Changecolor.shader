
Shader "MGame/UI/Changecolor" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        Hue ("Hue", Range(0, 1)) = 0
        Hue_intensity ("Hue_intensity", Range(0, 1)) = 0
        lightness ("lightness", Range(0, 1)) = 0
        Saturation ("Saturation", Range(-1, 1)) = 0
        [MaterialToggle] B_01 ("B_01", Float ) = 0
        [MaterialToggle] B_02 ("B_02", Float ) = 0
        [MaterialToggle] B_03 ("B_03", Float ) = 0
        [MaterialToggle] B_04 ("B_04", Float ) = 0
        [MaterialToggle] H_black_white ("H_black_white", Float ) = 0
        [MaterialToggle] H_white_black ("H_white_black", Float ) = 0
        [MaterialToggle] V_black_white ("V_black_white", Float ) = 0
        [MaterialToggle] V_white_black ("V_white_black", Float ) = 0
        iMin ("iMin", Range(0, 1)) = 0
        iMax ("iMax", Range(0, 1)) = 0
        oMin ("oMin", Range(0, 1)) = 0
        oMax ("oMax", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5


		[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
		[HideInInspector]_Stencil("Stencil ID", Float) = 0
		[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
		[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
		[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255

		[HideInInspector]_ColorMask("Color Mask", Float) = 15
    }
    SubShader {
        Tags
		{
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}


        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
			ColorMask[_ColorMask]

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float Hue;
            uniform float Hue_intensity;
            uniform float lightness;
            uniform float Saturation;
            uniform fixed B_01;
            uniform fixed H_black_white;
            uniform fixed V_white_black;
            uniform fixed B_03;
            uniform fixed B_04;
            uniform fixed H_white_black;
            uniform fixed V_black_white;
            uniform fixed B_02;
            uniform float iMin;
            uniform float iMax;
            uniform float oMin;
            uniform float oMax;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
				fixed4 color : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
				fixed4 color : TEXCOORD1;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
				o.color = v.color;
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting: 
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_3313 = (lerp(_MainTex_var.rgb,dot(_MainTex_var.rgb,float3(0.3,0.59,0.11)),Saturation)*(lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(Hue+float3(0.0,-1.0/3.0,1.0/3.0)))-1),Hue_intensity)*lightness));
                float node_179 = i.uv0.r;
                float node_7835 = i.uv0.g;
                float node_9433 = min(node_179,node_7835);
                float3 emissive = lerp(_MainTex_var.rgb,(oMin + ( (node_3313 - iMin) * (oMax - oMin) ) / (iMax - iMin)),(((B_02*(1.0 - node_9433))+(B_04*(1.0 - max(node_179,node_7835)))+(node_9433*B_01)+(B_03*max(node_179,node_7835))+(H_white_black*(1.0 - node_179)))+(V_black_white*(1.0 - node_7835))+(H_black_white*node_179)+(V_white_black*node_7835)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,_MainTex_var.a)*i.color;
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }     
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
