Shader "Unlit/Player Shader"
{
    Properties
    {
        _PlayerColor ("Player Color", Color) = (0.9, 0.9, 0.9)
        _ObstacleColor ("Obstacle Color", Color) = (0.9, 0, 0)
        _ColorBalance ("Color Balance", Range(0, 1)) = 0
        _BlendingRadius ("Lower Blending Radius", float) = 1
        _BlendingLength ("Blending Width", float) = 1
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

            uniform StructuredBuffer<float2> PosBuffer : register(t1);

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float3 worldPos : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 playerColor : COLOR;
                float4 obstacleColor : TEXCOORD1;
                float blend : TEXCOORD2;
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
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                
                o.obstacleColor = float4(_ObstacleColor.rgb, max(_PlayerColor.a, _ObstacleColor.a));
                o.playerColor = lerp(_PlayerColor, o.obstacleColor, _ColorBalance) * v.color;

                o.blend = CalculateBlendForPosition(o.worldPos);
                
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return lerp(i.obstacleColor, i.playerColor, i.blend);
            }
            ENDCG
        }
    }
}