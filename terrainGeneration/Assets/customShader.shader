Shader "Unlit/customShader"
{

	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			uniform float _maxHeight;
			
			#include "UnityCG.cginc"

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};


			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				vertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float4 colour;

				if (v.vertex.y > _maxHeight-50) {

					colour = float4(1,1,1,1);
				}
				else {
					colour = float4(0.09804, 0.54902, 0.09804, 1);
				}

				o.color = colour;				
				return o;
			}
			

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				return v.color;
			}
			ENDCG
		}
	}
}
