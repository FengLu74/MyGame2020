// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33074,y:32745,varname:node_3138,prsc:2|emission-4816-RGB,alpha-4816-A,voffset-2380-OUT;n:type:ShaderForge.SFN_Time,id:4042,x:31983,y:33070,varname:node_4042,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:4816,x:32585,y:32786,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_4816,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Frac,id:4887,x:32278,y:33091,varname:node_4887,prsc:2|IN-7366-OUT;n:type:ShaderForge.SFN_Append,id:2380,x:32917,y:33048,varname:node_2380,prsc:2|A-3440-OUT,B-6203-OUT,C-1960-OUT;n:type:ShaderForge.SFN_Vector1,id:1960,x:32688,y:33225,varname:node_1960,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:3440,x:32727,y:32952,varname:node_3440,prsc:2,v1:0;n:type:ShaderForge.SFN_OneMinus,id:6203,x:32701,y:33025,varname:node_6203,prsc:2|IN-4928-OUT;n:type:ShaderForge.SFN_Multiply,id:4928,x:32459,y:33091,varname:node_4928,prsc:2|A-8722-OUT,B-4887-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8722,x:32343,y:32837,ptovrint:False,ptlb:Height,ptin:_Height,varname:node_8722,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Multiply,id:7366,x:32248,y:33237,varname:node_7366,prsc:2|A-9183-OUT,B-4042-T;n:type:ShaderForge.SFN_ValueProperty,id:9183,x:32103,y:33004,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_9183,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:4816-8722-9183;pass:END;sub:END;*/

Shader "MGame/UI/Finger" {
    Properties {
        _texture ("texture", 2D) = "white" {}
        _Height ("Height", Float ) = 5
        _speed ("speed", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
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
            uniform float _Height;
            uniform float _speed;
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
                float4 node_4042 = _Time;
                v.vertex.xyz += float3(0.0,(1.0 - (_Height*frac((_speed*node_4042.g)))),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(i.uv0, _texture));
                float3 emissive = _texture_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,_texture_var.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _Height;
            uniform float _speed;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float4 node_4042 = _Time;
                v.vertex.xyz += float3(0.0,(1.0 - (_Height*frac((_speed*node_4042.g)))),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
