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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            uniform StructuredBuffer<float2> buffer : register(t1);

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float3 worldPos : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
            
            float4 _PlayerColor;
            float4 _ObstacleColor;
            float _BlendingRadius;
            float _BlendingLength;
            float _ColorBalance;

            float4 GetObstacleColor()
            {
                return float4(_ObstacleColor.rgb, max(_PlayerColor.a, _ObstacleColor.a));
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.color = lerp(_PlayerColor, GetObstacleColor(), _ColorBalance) * v.color;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                const int width = buffer[0].x;

                const float upperBlendingRadius = _BlendingRadius + _BlendingLength;
                float minDist = upperBlendingRadius + 1;

                for (int index = 1; index < width + 1; index++)
                    minDist = min(minDist, distance(i.worldPos.xy, buffer[index]));
                
                float blend = (minDist - _BlendingRadius) / (upperBlendingRadius - _BlendingRadius);
                blend = saturate(blend);
                float4 col = lerp(GetObstacleColor(), i.color, blend);

                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }            
            ENDCG
        }
    }
}