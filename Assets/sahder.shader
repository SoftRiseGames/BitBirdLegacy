Shader "Custom/CRTWithDistortionAndMask"
{
    Properties
    {
        _MainTex ("Mask Texture", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.4
        _Distortion ("Curvature", Range(0, 1)) = 0.1
        _TransparencyThreshold ("White Threshold", Range(0, 1)) = 0.95
        _EffectColor ("Effect Color", Color) = (0, 0, 0, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ScanlineIntensity;
            float _Distortion;
            float _TransparencyThreshold;
            float4 _EffectColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Bombelik efekti için UV koordinatlarını bükme
                float2 curvedUV = i.uv - 0.5;
                curvedUV *= 1.0 + _Distortion * dot(curvedUV, curvedUV);
                curvedUV += 0.5;

                // Maske görselini oku
                fixed4 maskCol = tex2D(_MainTex, curvedUV);

                // Beyazlık kontrolü (luminance kullanılarak)
                float luminance = dot(maskCol.rgb, float3(0.299, 0.587, 0.114));
                if (luminance > _TransparencyThreshold)
                    discard; // Beyazsa tamamen atla (saydam + efekt yok)

                // CRT efektini uygulama (scanline)
                float scanline = sin(i.uv.y * 800.0) * _ScanlineIntensity;
                float3 effect = _EffectColor.rgb * scanline;

                // Efekti göstermek
                return float4(effect, 1.0); // Efekti tamamen opak yap
            }
            ENDCG
        }
    }
}
