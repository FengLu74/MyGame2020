Shader "MGame/Effect/Particles/full_Effects"
{
    Properties
    {
        [Header(RenderingMode)]
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("Src Blend",int) = 0
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("Dst Blend",int) = 0
        [Enum(UnityEngine.Rendering.CullMode)]_Cull ("Cull",int) = 0
        [Enum(Off,0,On,1)]_ZWrite("ZWrite",int) = 0
        [Enum(RGB, 14, RGBA, 15)]_ColorMask ("ColorMask", int) = 15
       
        [Header(Base)]
        [Toggle]_Premultiplied ("Premultiplied", int) = 0
        _Color("Color",Color) = (1,1,1,1)
        _Intensity("Intensity",Range(-4,4)) = 1
        _MainTex ("MainTex", 2D) = "white" {}
        _MainUVSpeedX("MainUVSpeed X",float) = 0
        _MainUVSpeedY("MainUVSpeed Y",float) = 0
        [Space(80)]

        [Header(Mask)]
        [Toggle]_MaskEnabled("Mask Enabled",int) = 0
        _MaskIntensity ("MaskIntensity", Range(-4, 4)) = 1
        _MaskTex("MaskTex",2D) = "white"{}
        _MaskUVSpeedX("MaskUVSpeed X",float) = 0
        _MaskUVSpeedY("MaskUVSpeed Y",float) = 0
        [Space(80)]
        [Header(Distort)]
        [MaterialToggle(DISTORTENABLED)]_DistortEnabled("Distort Enabled",int) = 0
        _DistortTex("DistortTex",2D) = "white"{}
        _Distort("Distort",Range(0,1)) = 0
        _DistortUVSpeedX("DistortUVSpeed X",float) = 0
        _DistortUVSpeedY("DistortUVSpeed Y",float) = 0
    }

    SubShader
    {
        Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}
        Blend [_SrcBlend] [_DstBlend]
        Cull [_Cull]
        ZWrite [_ZWrite]
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _PREMULTIPLIED_ON
            #pragma multi_compile _ _MASKENABLED_ON
            #pragma multi_compile _ DISTORTENABLED
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color: COLOR;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 uv2: TEXCOORD1;
                float4 color: COLOR;
            };

            sampler2D _MainTex; float4 _MainTex_ST;
            fixed4 _Color;
            half _Intensity;
            float _MainUVSpeedX,_MainUVSpeedY;
            sampler2D _MaskTex; float4 _MaskTex_ST;
            float _MaskUVSpeedX,_MaskUVSpeedY;
            sampler2D _DistortTex; float4 _DistortTex_ST;
            float _Distort,_DistortUVSpeedX,_DistortUVSpeedY;
            half _MaskIntensity;

            v2f vert (appdata v)
            {
                v2f o = (v2f) 0;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex) + float2(_MainUVSpeedX,_MainUVSpeedY) * _Time.y;
                o.color = v.color *  _Color;

                #if _MASKENABLED_ON
                    o.uv.zw = TRANSFORM_TEX(v.uv,_MaskTex) + float2(_MaskUVSpeedX,_MaskUVSpeedY) * _Time.y;
                #endif

                #if DISTORTENABLED
                    o.uv2 = TRANSFORM_TEX(v.uv,_DistortTex) + float2(_DistortUVSpeedX,_DistortUVSpeedY) * _Time.y;
                #endif
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c;
                c =  _Intensity * i.color;
                
                float2 distort = i.uv.xy;

                #if DISTORTENABLED
                    fixed4 distortTex = tex2D(_DistortTex,i.uv2);
                    distort = lerp(i.uv.xy,distortTex,_Distort);
                #endif

                fixed4 mainTex = tex2D(_MainTex, distort);
                c *= mainTex;

                #if _PREMULTIPLIED_ON
                c.rgb *= c.a;
                #endif

                #if _MASKENABLED_ON
                    fixed4 maskTex = tex2D(_MaskTex, i.uv.zw) * _MaskIntensity;
                    c *= maskTex;
                #endif

                return c;
            }
            ENDCG
        }
    }
}
