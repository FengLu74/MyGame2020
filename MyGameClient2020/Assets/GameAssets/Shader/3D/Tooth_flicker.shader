// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3796,x:32719,y:32712,varname:node_3796,prsc:2|emission-2253-OUT,alpha-1810-OUT;n:type:ShaderForge.SFN_Tex2d,id:9347,x:32218,y:32793,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_9347,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:42c6cbafef4b45440b6e298646aa1ec5,ntxv:0,isnm:False|UVIN-9905-OUT;n:type:ShaderForge.SFN_TexCoord,id:9140,x:31168,y:32747,varname:node_9140,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2253,x:32537,y:32573,varname:node_2253,prsc:2|A-8274-RGB,B-9347-RGB;n:type:ShaderForge.SFN_Color,id:8274,x:32055,y:32421,ptovrint:False,ptlb:color,ptin:_color,varname:node_8274,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:1810,x:32515,y:32799,varname:node_1810,prsc:2|A-9347-A,B-8274-A;n:type:ShaderForge.SFN_Slider,id:4718,x:30804,y:33076,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_4718,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.502416,max:4;n:type:ShaderForge.SFN_Subtract,id:5937,x:31632,y:33087,varname:node_5937,prsc:2|A-2214-OUT,B-1438-OUT;n:type:ShaderForge.SFN_Vector1,id:2214,x:30999,y:32937,varname:node_2214,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1438,x:31309,y:33055,varname:node_1438,prsc:2|A-2214-OUT,B-4718-OUT;n:type:ShaderForge.SFN_Add,id:9905,x:31832,y:32839,varname:node_9905,prsc:2|A-5671-OUT,B-5937-OUT;n:type:ShaderForge.SFN_Multiply,id:5671,x:31572,y:32896,varname:node_5671,prsc:2|A-9140-UVOUT,B-4718-OUT;proporder:9347-8274-4718;pass:END;sub:END;*/

Shader "MGame/3D/Tooth" {
    Properties {
        _texture ("texture", 2D) = "white" {}
        _color ("color", Color) = (1,1,1,1)
        _Scale ("Scale", Range(0, 4)) = 1.502416
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float4 _color;
            uniform float _Scale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_2214 = 0.5;
                float2 node_9905 = ((i.uv0*_Scale)+(node_2214-(node_2214*_Scale)));
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(node_9905, _texture));
                float3 emissive = (_color.rgb*_texture_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,(_texture_var.a*_color.a));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
