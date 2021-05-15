Shader "Custom/TransparentSurface" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        sampler2D _OverlayTex;

        struct Input {
        float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            half4 d = tex2D(_OverlayTex, IN.uv_MainTex);
            o.Albedo = d.rgb;
            o.Alpha  = c.r;
        }
        ENDCG
    }
}