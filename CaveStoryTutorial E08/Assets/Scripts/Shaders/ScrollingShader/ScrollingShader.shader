Shader "Unlit/ScrollingShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DispTex ("Displacement Texture", 2D) = "white" {}
		_Speed ("Scroll Speed", Float) = 5
	}
	SubShader
	{

		Cull Off ZWrite Off ZTest Always
		
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
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
				float2 uv1 : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _DispTex;
			float _Speed;
			float4 _MainTex_ST;
			
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv1 = v.uv;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				fixed4 disp = tex2D(_DispTex, i.uv1);

				
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv + float2((_Time.x * _Speed * disp.r), 0));
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
