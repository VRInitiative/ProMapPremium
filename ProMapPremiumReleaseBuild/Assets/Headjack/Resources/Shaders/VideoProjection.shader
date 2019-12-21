Shader "Hidden/Headjack/EquiToCubeProjection" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "black" {}
	}
	SubShader
	{
			Tags { "RenderType" = "Opaque" "Queue" = "Background+1" }
			LOD 100
			cull back
			ZWrite off
			Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile MESH PROJECT LATLONG FISHEYE
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			float4 _MainTex_ST;
			uniform float _MinBlackLevel;
#if MESH
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			v2f vert(float4 vertex : POSITION, float2 uv : TEXCOORD0)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(vertex);
				o.uv = (uv * _MainTex_ST.xy) + _MainTex_ST.zw;
				return o;
			}
			fixed4 frag(v2f i) : SV_Target
			{
				return tex2D(_MainTex,i.uv);
			}
#endif
#if PROJECT
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 pos : TEXCOORD0;
			};
			v2f vert(float4 vertex : POSITION)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(vertex);
				o.pos = vertex;
				return o;
			}
			float2 WorldToEqui(float3 wp)
			{
				return float2(atan2(wp.x, wp.z)*0.15915495087, asin(wp.y)*0.3183099524);
			}
			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = (WorldToEqui(normalize(i.pos))) + 0.5;
#if SHADER_API_METAL
				uv.y = 1 - uv.y;
#endif
				return max(_MinBlackLevel, tex2D(_MainTex, uv*_MainTex_ST.xy + _MainTex_ST.zw));
			}
#endif
#if LATLONG
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 pos : TEXCOORD0;
				float2 latlong : TEXCOORD1;
			};
			uniform float _PROJ_LAT, _PROJ_LONG;
			v2f vert(float4 vertex : POSITION)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(vertex);
				o.pos = vertex;
				o.latlong = float2(360.0 / _PROJ_LAT, 180.0 / _PROJ_LONG);
				return o;
			}

			float2 WorldToEqui(float3 wp)
			{
				return float2(atan2(wp.x, wp.z)*0.15915495087, asin(wp.y)*0.3183099524);
			}
			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = (WorldToEqui(normalize(i.pos)));
				uv = uv * i.latlong + 0.5;
#if SHADER_API_METAL
				uv.y = 1 - uv.y;
#endif
				return tex2D(_MainTex, uv*_MainTex_ST.xy + _MainTex_ST.zw)*(1- any(floor(uv)));
			}
#endif
#if FISHEYE
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 pos : TEXCOORD0;
				float3 data : TEXCOORD1; //(x=Aspect) (y=Aperture) (z=AngleInRadians)
			};
			half _PROJ_FADE, _PROJ_ANGLE, _PROJ_HEIGHT;
			float4 _MainTex_TexelSize;
			v2f vert(float4 vertex : POSITION)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(vertex);
				o.pos = mul(unity_ObjectToWorld, vertex);
				o.data.x = _MainTex_TexelSize.y / _MainTex_TexelSize.x;
				float width = (_PROJ_HEIGHT * _MainTex_TexelSize.w) / _MainTex_TexelSize.z;
				o.data.y = 1.0 / ((_PROJ_ANGLE / 360.0 * 3.1415 * 2) / width);
				o.data.z = (_PROJ_ANGLE / 360.0) * 3.1415f;
				return o;
			}
			fixed4 frag(v2f i) : SV_Target
			{
				i.pos = normalize(i.pos);
				fixed2 r = float2(atan2(length(i.pos.xy),i.pos.z)*i.data.y,atan2(i.pos.y,i.pos.x));
				fixed2 uv = float2(r.x*cos(r.y),r.x*sin(r.y)*i.data.x) + _MainTex_ST.zw;
				float angle = acos(dot(i.pos, float3(0, 0, 1)));
				float border = 1 - pow(1 - saturate(i.data.z - angle), _PROJ_FADE * 16);
				fixed4 c = tex2D(_MainTex,uv + 0.5)*border;
				return max(_MinBlackLevel, c);
			}
#endif
			ENDCG
		}
	}
}
 