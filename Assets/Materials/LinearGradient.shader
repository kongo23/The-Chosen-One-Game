﻿Shader "ShtoporGames/Unlit/LinearGradient"
{
	Properties {
        _TopColor ("Top color", Color) = (1,1,1,1)
		_BottomColor ("Bottom color", Color) = (1,1,1,1)
		_Speed ("Speed", float) = 0.2
    }
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			fixed4 _TopColor;
			fixed4 _BottomColor;
			uniform float  _Speed;

			struct v2f {
				float4 position : SV_POSITION;
				fixed4 color : COLOR;
			};

			v2f vert (appdata_full v)
			{
				v2f o;
				o.position = mul (UNITY_MATRIX_MVP, v.vertex);
				o.color = lerp(_TopColor,_BottomColor, v.texcoord.y );
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float4 color = i.color;
				color.a = 1;
				return color;
			}
			ENDCG
		}
	}
}
