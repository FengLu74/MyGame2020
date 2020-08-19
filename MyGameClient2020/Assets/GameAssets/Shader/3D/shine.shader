// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3578,x:33147,y:32689,varname:node_3578,prsc:2|emission-8636-OUT,alpha-5948-A;n:type:ShaderForge.SFN_Tex2d,id:3488,x:32672,y:32420,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3488,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:32c5f8fc0d72a484684aca437c4b00ff,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:7832,x:32792,y:32706,varname:node_7832,prsc:2|A-3488-RGB,B-6036-OUT;n:type:ShaderForge.SFN_Color,id:5018,x:32066,y:32999,ptovrint:False,ptlb:color,ptin:_color,varname:node_5018,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Vector1,id:3357,x:32066,y:33151,varname:node_3357,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:6036,x:32478,y:32773,varname:node_6036,prsc:2|A-1644-R,B-5018-RGB,C-3357-OUT;n:type:ShaderForge.SFN_Tex2d,id:5948,x:32372,y:33079,ptovrint:False,ptlb:texture_alpha,ptin:_texture_alpha,varname:node_5948,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1644,x:32037,y:32493,ptovrint:False,ptlb:shine,ptin:_shine,varname:node_1644,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:75d7130deb9905249ada87d896b10c5c,ntxv:0,isnm:False|UVIN-7217-UVOUT;n:type:ShaderForge.SFN_Rotator,id:7217,x:31770,y:32429,varname:node_7217,prsc:2|UVIN-7186-OUT,ANG-155-OUT;n:type:ShaderForge.SFN_Time,id:5940,x:29750,y:32338,varname:node_5940,prsc:2;n:type:ShaderForge.SFN_Append,id:8456,x:31204,y:32344,varname:node_8456,prsc:2|A-5541-OUT,B-5541-OUT;n:type:ShaderForge.SFN_Add,id:7186,x:31522,y:32356,varname:node_7186,prsc:2|A-9196-UVOUT,B-8456-OUT;n:type:ShaderForge.SFN_TexCoord,id:9196,x:31402,y:32119,varname:node_9196,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:3090,x:29813,y:32755,ptovrint:False,ptlb:time,ptin:_time,varname:node_3090,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Fmod,id:6948,x:30129,y:32663,varname:node_6948,prsc:2|A-1129-OUT,B-3090-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:3373,x:30373,y:32687,varname:node_3373,prsc:2,min:0,max:0.7|IN-6948-OUT;n:type:ShaderForge.SFN_Subtract,id:5541,x:30519,y:32865,varname:node_5541,prsc:2|A-3373-OUT,B-4469-OUT;n:type:ShaderForge.SFN_Slider,id:155,x:31313,y:32628,ptovrint:False,ptlb:angle,ptin:_angle,varname:node_155,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7,max:1;n:type:ShaderForge.SFN_ValueProperty,id:4469,x:30243,y:32944,ptovrint:False,ptlb:length,ptin:_length,varname:node_4469,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.35;n:type:ShaderForge.SFN_Multiply,id:1129,x:30070,y:32396,varname:node_1129,prsc:2|A-5940-TSL,B-6605-OUT;n:type:ShaderForge.SFN_Vector1,id:6605,x:29846,y:32478,varname:node_6605,prsc:2,v1:10;n:type:ShaderForge.SFN_VertexColor,id:230,x:32774,y:32881,varname:node_230,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8636,x:32980,y:32790,varname:node_8636,prsc:2|A-7832-OUT,B-230-RGB;proporder:3488-5948-5018-1644-3090-155-4469;pass:END;sub:END;*/

Shader "MGame/3D/Transparentshine" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _texture_alpha ("texture_alpha", 2D) = "white" {}
        _color ("color", Color) = (1,0,0,1)
        _shine ("shine", 2D) = "white" {}
        _time ("time", Float ) = 10
		_yn("yn", Float) = 1
        _angle ("angle", Range(0, 1)) = 0.7
        _length ("length", Float ) = 0.35
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
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
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _color;
            uniform sampler2D _texture_alpha; uniform float4 _texture_alpha_ST;
            uniform sampler2D _shine; uniform float4 _shine_ST;
			uniform float _yn;
            uniform float _time;
            uniform float _angle;
            uniform float _length;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_7217_ang = _angle;
                float node_7217_spd = 1.0;
                float node_7217_cos = cos(node_7217_spd*node_7217_ang);
                float node_7217_sin = sin(node_7217_spd*node_7217_ang);
                float2 node_7217_piv = float2(0.5,0.5);
                float4 node_5940 = _Time + _TimeEditor;
                float node_5541 = (clamp(fmod((node_5940.r*10.0), _yn*_time),0,0.7)-_length);
                float2 node_7217 = (mul((i.uv0+float2(node_5541,node_5541))-node_7217_piv,float2x2( node_7217_cos, -node_7217_sin, node_7217_sin, node_7217_cos))+node_7217_piv);
                float4 _shine_var = tex2D(_shine,TRANSFORM_TEX(node_7217, _shine));
                float3 emissive = ((_MainTex_var.rgb+(_shine_var.r*_color.rgb*2.0))*i.vertexColor.rgb);
                float3 finalColor = emissive;
                float4 _texture_alpha_var = tex2D(_texture_alpha,TRANSFORM_TEX(i.uv0, _texture_alpha));
                return fixed4(finalColor,_texture_alpha_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
