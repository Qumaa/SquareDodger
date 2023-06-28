Shader "Unlit/Player Shader"
{
    Properties
    {
        _PlayerColor ("Player Color", Color) = (0.9, 0.9, 0.9)
        _ObstacleColor ("Obstacle Color", Color) = (0.9, 0, 0)
        _ColorBalance ("Color Balance", Range(0, 1)) = 0
        _BlendingRadius ("Lower Blending Radius", float) = 1
        _BlendingLength ("Blending Width", float) = 1
        
        // in HLSL: (uppercase property name)_(uppercase enum value name)
        [KeywordEnum(Off, Vert, Frag)] _ColorBlend ("Color Blending Mode", float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "PreviewType"="Plane"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Stencil
        {
            Comp Equal
            Pass IncrSat
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #pragma multi_compile _COLORBLEND_OFF _COLORBLEND_VERT _COLORBLEND_FRAG

            uniform StructuredBuffer<float2> PosBuffer : register(t1);

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 playerColor : COLOR;
                float4 obstacleColor : TEXCOORD0;

                #ifdef _COLORBLEND_VERT
                float blend : TEXCOORD1;
                #endif
                
                #ifdef _COLORBLEND_FRAG
                float3 worldPos : TEXCOORD2;
                #endif
            };

            float4 _PlayerColor;
            float4 _ObstacleColor;
            float _BlendingRadius;
            float _BlendingLength;
            float _ColorBalance;

            float CalculateBlendForPosition(float2 pos)
            {
                const int width = PosBuffer[0].x;

                float minDist = _BlendingRadius + _BlendingLength;

                for (int index = 1; index < width + 1; index++)
                    minDist = min(minDist, distance(pos, PosBuffer[index]));

                float blend = (minDist - _BlendingRadius) / _BlendingLength;
                return saturate(blend);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                #if !defined (_COLORBLEND_OFF)
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);

                #ifdef _COLORBLEND_VERT
                o.blend = CalculateBlendForPosition(worldPos);
                #elif defined (_COLORBLEND_FRAG)
                o.worldPos = worldPos;
                #endif
                
                #endif
                                
                o.obstacleColor = float4(_ObstacleColor.rgb, max(_PlayerColor.a, _ObstacleColor.a));
                o.playerColor = lerp(_PlayerColor, o.obstacleColor, _ColorBalance) * v.color;                
                
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                #ifdef _COLORBLEND_OFF
                return i.playerColor;                
                #elif defined (_COLORBLEND_VERT)
                return lerp(i.obstacleColor, i.playerColor, i.blend);
                #elif defined (_COLORBLEND_FRAG)
                return lerp(i.obstacleColor, i.playerColor, CalculateBlendForPosition(i.worldPos));
                #endif
            }
            ENDCG
        }
    }
}