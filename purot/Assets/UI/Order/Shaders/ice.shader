Shader "Custom/sample" {
        Properties
        {
            _Color ("Color", Color) = (1,1,1,1)
            _Alpha("Alpha", float) = 0.0
        }
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard alpha:fade
		#pragma target 3.0

		struct Input {
			float3 worldNormal;
      			float3 viewDir;
			float2 uv : TEXCOORD0;
		};
                
                fixed4 _Color;
                half _Alpha;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed3 c = _Color.rgb;
			o.Albedo = c;
			float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
     			o.Alpha =  alpha*_Alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}