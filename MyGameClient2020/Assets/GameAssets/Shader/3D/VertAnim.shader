Shader "MGame/3D/VertAnim" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_DataTex("Data Texture",2D) = "black"{}
		_FrameRate("FrameRate", Range(0,1)) = 0.0
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		ZWrite On

		Pass
	{
		CGPROGRAM

		//precomplie conditions
		// Whether use Normal movement data
#define NormalChange
		//Tangent Movement data, for normal mapping
#define TangentChange
		//Use Id from Model vertex color not from texture

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	sampler2D _DataTex;
	float4 _DataTex_TexelSize;
	half _FrameRate;
	fixed4 _Color;


	void HandPos(inout appdata_full v)
	{
		float4 index_rgba = v.color;

		float index = index_rgba.r * 256 * 256 * 255 + index_rgba.g * 256 * 255 + index_rgba.b * 255;

		float textureDim = _DataTex_TexelSize.w;
		float4 info = tex2Dlod(_DataTex, float4(1.0f - 0.5f / textureDim, 0.5f / textureDim, 0, 0));
		float frameNum = info.a * 256 * 255 + info.r * 255;

		float cur_frame = _FrameRate*frameNum; //fmod(_Time.y*12.0f, frameNum);
		float weight = frac(cur_frame);
		float pre_frame = floor(cur_frame);
		float next_frame = fmod((pre_frame + 1), frameNum);

		float pre_loc = (index*frameNum + pre_frame) * 3;
		float pre_x = fmod(pre_loc, textureDim);
		float pre_y = floor(pre_loc / textureDim);

		float next_loc = (index*frameNum + next_frame) * 3;
		float next_x = fmod(next_loc, textureDim);
		float next_y = floor(next_loc / textureDim);

		float2 pre_uv = float2((float(pre_x) + 0.5f) / textureDim, 1.0f - (float(pre_y) + 0.5f) / textureDim);
		float4 pre_data = tex2Dlod(_DataTex, float4(pre_uv, 0, 0));
		float3 pre_trans = float3(pre_data.r*2.0f - 1.0f, pre_data.g*2.0f - 1.0f, pre_data.b*2.0f - 1.0f)*pre_data.a*255.0f;

		float2 next_uv = float2((float(next_x) + 0.5f) / textureDim, 1.0f - (float(next_y) + 0.5f) / textureDim);
		float4 next_data = tex2Dlod(_DataTex, float4(next_uv, 0, 0));
		float3 next_trans = float3(next_data.r*2.0f - 1.0f, next_data.g*2.0f - 1.0f, next_data.b*2.0f - 1.0f)*next_data.a*255.0f;

		float3 trans = lerp(pre_trans, next_trans, weight);

		v.vertex.xyz += trans * float3(-1, 1, 1);

#ifdef NormalChange // have normal change data
		float pre_loc_normal = pre_loc + 1;
		float pre_x_normal = fmod(pre_loc_normal, textureDim);
		float pre_y_normal = floor(pre_loc_normal / textureDim);

		float next_loc_normal = next_loc + 1;
		float next_x_normal = fmod(next_loc_normal, textureDim);
		float next_y_normal = floor(next_loc_normal / textureDim);

		float2 pre_uv_normal = float2((float(pre_x_normal) + 0.5f) / float(textureDim), 1.0f - (float(pre_y_normal) + 0.5f) / float(textureDim));
		float4 pre_data_normal = tex2Dlod(_DataTex, float4(pre_uv_normal, 0, 0));
		float3 pre_trans_normal = float3(pre_data_normal.r*2.0f - 1.0f, pre_data_normal.g*2.0f - 1.0f, pre_data_normal.b*2.0f - 1.0f)*pre_data_normal.a*255.0f;

		float2 next_uv_normal = float2((float(next_x_normal) + 0.5f) / float(textureDim), 1.0f - (float(next_y_normal) + 0.5f) / float(textureDim));
		float4 next_data_normal = tex2Dlod(_DataTex, float4(next_uv_normal, 0, 0));
		float3 next_trans_normal = float3(next_data_normal.r*2.0f - 1.0f, next_data_normal.g*2.0f - 1.0f, next_data_normal.b*2.0f - 1.0f)*next_data_normal.a*255.0f;

		float3 trans_normal = lerp(pre_trans_normal, next_trans_normal, weight);
		v.normal += trans_normal*float3(-1, 1, 1);
		v.normal = normalize(v.normal);
#endif


#ifdef TangentChange // have tangent change data
		float pre_loc_tangent = pre_loc + 2;
		float pre_x_tangent = fmod(pre_loc_tangent, textureDim);
		float pre_y_tangent = floor(pre_loc_tangent / textureDim);

		float next_loc_tangent = next_loc + 2;
		float next_x_tangent = fmod(next_loc_tangent, textureDim);
		float next_y_tangent = floor(next_loc_tangent / textureDim);

		float2 pre_uv_tangent = float2((float(pre_x_tangent) + 0.5f) / float(textureDim), 1.0f - (float(pre_y_tangent) + 0.5f) / float(textureDim));
		float4 pre_data_tangent = tex2Dlod(_DataTex, float4(pre_uv_tangent, 0, 0));
		float3 pre_trans_tangent = float3(pre_data_tangent.r*2.0f - 1.0f, pre_data_tangent.g*2.0f - 1.0f, pre_data_tangent.b*2.0f - 1.0f)*pre_data_tangent.a*255.0f;

		float2 next_uv_tangent = float2((float(next_x_tangent) + 0.5f) / float(textureDim), 1.0f - (float(next_y_tangent) + 0.5f) / float(textureDim));
		float4 next_data_tangent = tex2Dlod(_DataTex, float4(next_uv_tangent, 0, 0));
		float3 next_trans_tangent = float3(next_data_tangent.r*2.0f - 1.0f, next_data_tangent.g*2.0f - 1.0f, next_data_tangent.b*2.0f - 1.0f)*next_data_tangent.a*255.0f;

		float3 trans_tangent = lerp(pre_trans_tangent, next_trans_tangent, weight);
		v.tangent += float4(trans_tangent, 0)*float4(-1, 1, 1, 1);
		v.tangent = normalize(v.tangent);
#endif
	}

	v2f vert(appdata_full v)
	{
		v2f o;
		HandPos(v);
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col = tex2D(_MainTex, i.uv)*_Color;
		return col;
	}
		ENDCG
	}
	}
}
