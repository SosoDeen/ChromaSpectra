Shader "Custom/Outline" {
    Properties{
        _MainTex("Albedo Texture", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0, 1)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Range(0, 0.5)) = 0.03
    }

        SubShader{
            Tags {
                "RenderType" = "Opaque"
            }

            Pass {
                Cull Front

                CGPROGRAM
                #pragma vertex VertexProgram
                #pragma fragment FragmentProgram

                struct appdata_t {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                float4 _OutlineColor;
                half _OutlineWidth;

                v2f VertexProgram(appdata_t v) {
                    v.vertex.xyz += v.normal * _OutlineWidth;
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.texcoord = v.texcoord;
                    return o;
                }

                half4 FragmentProgram(v2f i) : SV_TARGET {
                    return _OutlineColor;
                }
                ENDCG
            }

            CGPROGRAM
            #pragma surface surf Lambert

            struct Input {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            half4 _OutlineColor;

            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
                //o.Smoothness = _Glossiness; // Set the smoothness based on the _Glossiness property
                //o.Metallic = _Metallic;
                //o.Alpha = 1.0; // You can set the alpha value as needed
            }
            ENDCG
        }
}
