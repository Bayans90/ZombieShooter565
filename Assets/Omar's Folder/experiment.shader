Shader "Unlit/new one"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorStart ("Start", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"// teling unity what to render first trans - opque
                "Queue" ="Transparent"
         }

        Pass
        {
            Cull Off
            zWrite off //depht buffer
            Blend One One //add
            //Blend DstColor Zero //multiple

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal( v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float xOffset = cos( i.uv.x*6.14 * 8) * 0.01;
                float t = cos((i.uv.y + xOffset + _Time.y*0.1)*6.14*5)*0.5 +0.5;
                t *= 1-i.uv.y;
                t *= col; // takes texture and does it
                return t * (abs(i.normal.y) < 0.999);
            }
            ENDCG
        }
    }
}
