Shader "Overlay/Fill"
{
    Properties
    {
        _Amount ("Amount", Range (0, 1)) = 1

        _Min ("Min", Range (0, 255)) = 100
        _Max ("Max", Range (0, 255)) = 255

        _Texture ("Texture", 2D) = "white"
        _Mask ("Mask", 2D) = "white"

        [Toggle] _Passthrough ("Passthrough Mode", Float) = 0
        _Background ("Background Sprite", 2D) = "white"
    }

    SubShader
    {
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature _MATERIAL_MASKED

            #include "UnityCG.cginc"

            // STRUCTS
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

            // UTILS

            float avg(float4 color)
            {
                return (color.r + color.g + color.b) / 3;
            }

            float map(float value, float a1, float a2, float b1, float b2)
            {
                return b1 + (value - a1) * (b2 - b1) / (a2 - a1);
            }


            // VERT

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // FRAG

            float _Amount;

            float _Min;
            float _Max;

            float _Passthrough;

            sampler2D _Mask;
            sampler2D _Texture;
            sampler2D _Background;

            float4 frag(v2f i) : SV_Target
            {
                float4 color = tex2D(_Mask, i.uv);
                float value = map(avg(color), 0, 1, 0, 255);

                if (value < _Min || value > _Max)
                    if (_Passthrough == 0) return tex2D(_Background, i.uv);
                    else return float4(0, 0, 0, 0);

                float amount = map(_Amount, 0, 1, _Min, _Max);

                if (value > amount)
                    if (_Passthrough == 0) return tex2D(_Background, i.uv);
                    else return float4(0, 0, 0, 0);

                return tex2D(_Texture, i.uv);
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
