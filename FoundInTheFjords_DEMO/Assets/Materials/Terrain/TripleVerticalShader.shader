Shader "Unlit/TripleVerticalShader"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _TopColour("Top Gradient Colour: ", Color) = (0.125,0,0.125,1)
        _MiddleColour("Middle Gradient Colour: ", Color) = (0.75,0.75,0.25,1)
        _BottomColour("Bottom Gradient Colour: ", Color) = (0.5,0.125,0,0)
        _MiddlePosition("Location relative to the bottom: ", float) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //// make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);

                //Pass through the UV Coordinates
                o.uv = v.uv;
                return o;
            }

            fixed4 _TopColour;
            fixed4 _MiddleColour;
            fixed4 _BottomColour;
            float _MiddlePosition;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                
                //return col;

                //Interpolate the colour between each vertex
                fixed4 col;
                if (i.uv.y < _MiddlePosition) {
                    col = lerp(_BottomColour, _MiddleColour, i.uv.y / _MiddlePosition);
                    }
                else{
                    col = lerp (_MiddleColour, _TopColour, (i.uv.y - _MiddlePosition) / (1 - _MiddlePosition));
                    }

                //// apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;

            }
            ENDCG
        }
    }
}
