// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34378,y:32695,varname:node_3138,prsc:2|emission-4227-RGB;n:type:ShaderForge.SFN_ValueProperty,id:6881,x:32169,y:32782,ptovrint:False,ptlb:Uspeed,ptin:_Uspeed,varname:node_6881,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:172,x:33319,y:32587,varname:node_172,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:4618,x:32192,y:32876,varname:node_4618,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2573,x:32210,y:33039,ptovrint:False,ptlb:Vspeed,ptin:_Vspeed,varname:node_2573,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:2294,x:32472,y:32796,varname:node_2294,prsc:2|A-6881-OUT,B-4618-T;n:type:ShaderForge.SFN_Multiply,id:552,x:32472,y:32961,varname:node_552,prsc:2|A-4618-T,B-2573-OUT;n:type:ShaderForge.SFN_Append,id:9659,x:32708,y:32893,varname:node_9659,prsc:2|A-2294-OUT,B-552-OUT;n:type:ShaderForge.SFN_Tex2d,id:1339,x:33113,y:32854,ptovrint:False,ptlb:FLOWmap,ptin:_FLOWmap,varname:node_1339,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2567-OUT;n:type:ShaderForge.SFN_Add,id:2567,x:32917,y:32811,varname:node_2567,prsc:2|A-3063-UVOUT,B-9659-OUT;n:type:ShaderForge.SFN_Append,id:4098,x:33351,y:32896,varname:node_4098,prsc:2|A-1339-R,B-1339-G;n:type:ShaderForge.SFN_Multiply,id:3128,x:33567,y:32934,varname:node_3128,prsc:2|A-4098-OUT,B-2664-OUT,C-7541-R;n:type:ShaderForge.SFN_ValueProperty,id:2664,x:33337,y:33210,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_2664,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:3088,x:33832,y:32918,varname:node_3088,prsc:2|A-172-UVOUT,B-3128-OUT;n:type:ShaderForge.SFN_Tex2d,id:4227,x:34100,y:32899,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_4227,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3088-OUT;n:type:ShaderForge.SFN_TexCoord,id:3063,x:32708,y:32708,varname:node_3063,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_VertexColor,id:4495,x:33113,y:33047,varname:node_4495,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:7541,x:33269,y:33060,ptovrint:False,ptlb:mask,ptin:_mask,varname:node_7541,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:6881-2573-1339-2664-4227-7541;pass:END;sub:END;*/

Shader "MGame/Effect/particles/water" {
    Properties {
        _Uspeed ("Uspeed", Float ) = 1
        _Vspeed ("Vspeed", Float ) = 0
        _FLOWmap ("FLOWmap", 2D) = "white" {}
        _Intensity ("Intensity", Float ) = 0
        _MainTex ("MainTex", 2D) = "white" {}
        _mask ("mask", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _Uspeed;
            uniform float _Vspeed;
            uniform sampler2D _FLOWmap; uniform float4 _FLOWmap_ST;
            uniform float _Intensity;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _mask; uniform float4 _mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_4618 = _Time;
                float2 node_2567 = (i.uv1+float2((_Uspeed*node_4618.g),(node_4618.g*_Vspeed)));
                float4 _FLOWmap_var = tex2D(_FLOWmap,TRANSFORM_TEX(node_2567, _FLOWmap));
                float4 _mask_var = tex2D(_mask,TRANSFORM_TEX(i.uv0, _mask));
                float2 node_3088 = (i.uv0+(float2(_FLOWmap_var.r,_FLOWmap_var.g)*_Intensity*_mask_var.r));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_3088, _MainTex));
                float3 emissive = _MainTex_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
