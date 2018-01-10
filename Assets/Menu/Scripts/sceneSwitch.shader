Shader "Unlit/sceneSwitch"
{
	Properties
	{
		_MainTex ("MainTexture", 2D) = "white" {}
		_TransferTexture("TransferTexture", 2D) = "white" {}
		_Cutoff("Cutoff", Range(0,1)) = 0
		_Fade("Fade", Range(0,1)) = 0
		_Colour("Colour", Color) = (1,1,1,1)
		_col("Col",Color) = (1,1,1,1)

		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Cutoff;
			float _Fade;
			fixed4 _Colour;
			fixed4 _col;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex,i.uv);
				// sample the texture
				if (i.uv.x < _Cutoff)
				{
					return _Colour = lerp(col,_Colour,_Fade);
				}
	
				return col;
			}
			ENDCG
		}
	}
}
