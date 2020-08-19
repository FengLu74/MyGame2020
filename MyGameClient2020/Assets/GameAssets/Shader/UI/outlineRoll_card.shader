Shader "MGame/UI/outlineRoll_card"
{
    Properties
    {
        [PerRendererData]_MainTex ("原始图", 2D) = "white" {}
        [NoScaleOffset]_Ramp ("品质流光", 2D) = "white" {}
        _OutlineColor("描边颜色",Color) = (1,1,1,1)
        _OutlineWidth("描边宽度",Range(0,6)) = 0
        _Speed("流动速度",float) = 0
        [Toggle]_outline("描边开关",float) = 0
        [Toggle]_quality("品质开关",float) = 0
        
    }


    SubShader
    {
        Tags { "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _OUTLINE_ON
            #pragma multi_compile _ _QUALITY_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float2 uv3 : TEXCOORD3;
                float2 uv4 : TEXCOORD4;
                float2 uvramp : TEXCOORD5;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            sampler2D _Ramp;
            float4 _Ramp_ST;
            float4 _OutlineColor;
            float _OutlineWidth, _Speed;
            float4 _Vertex;

            v2f vert (appdata v)
            {
                v2f o =(v2f) 0;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //做UV的上下左右偏移
                float2 pianyi1 = v.uv+float2(( _OutlineWidth * 0.01 ) , 0);
                float2 pianyi2 = v.uv+float2(0, ( _OutlineWidth * 0.01 ) );
                float2 pianyi3= v.uv+float2(( _OutlineWidth * 0.01 ) , 0)*-1;
                float2 pianyi4 = v.uv+float2(0, ( _OutlineWidth * 0.01 ) )*-1;
                o.uv = v.uv;
                o.color = v.color;
                o.uv1 = pianyi1 ;
                o.uv2 = pianyi2 ;
                o.uv3 = pianyi3 ;
                o.uv4 = pianyi4 ;
                //品质流光的偏移速度
                o.uvramp = v.uv * _Ramp_ST.xy + _Ramp_ST.zw + float2(_Speed,0)*_Time.y;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //设置uv的偏移值分解步骤
                // float kuandu = _OutlineWidth*0.01;
                // float2 bcd = float2(kuandu,0);
                // float2 abc = i.uv * 1 + bcd;
                //设置uv的偏移值(移到了vert里面了为了节约性能)
                // float2 pianyi1 = i.uv+float2(( _OutlineWidth * 0.01 ) , 0);
                // float2 pianyi2 = i.uv+float2(0, ( _OutlineWidth * 0.01 ) );
                // float2 pianyi3= i.uv+float2(( _OutlineWidth * 0.01 ) , 0)*-1;
                // float2 pianyi4 = i.uv+float2(0, ( _OutlineWidth * 0.01 ) )*-1;
                
                fixed4 colx = fixed4 (0,0,0,0);
                fixed4 ramp = fixed4 (1,1,1,1);
                fixed4 col = tex2D(_MainTex, i.uv);

                #if _OUTLINE_ON
                    // 采样四个方向的图片，包括本身那张贴图
                    fixed4 col1 = tex2D(_MainTex, i.uv1);
                    fixed4 col2 = tex2D(_MainTex, i.uv2);
                    fixed4 col3 = tex2D(_MainTex, i.uv3);
                    fixed4 col4 = tex2D(_MainTex, i.uv4);
                    
                    //四方方向的挤出来的偏移出来的描边*描边颜色
                    colx = (clamp(col1.a+col2.a+col3.a+col4.a,0,1)-col.a)*_OutlineColor;
                #endif
                
                #if _QUALITY_ON
                    //采样品质渐变图
                    ramp = tex2D(_Ramp, i.uvramp);
                #endif

                //相加获得描边和原始图的最终结果
                fixed4 bcd = colx*ramp + col*col.a;
                fixed4 c = i.color;

                return bcd*c;
            }
            ENDCG
        }
    }
}
