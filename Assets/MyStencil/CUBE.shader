Shader "Custom/OpaqueWithStencil" {
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent"  }
        Pass {
            // 启用模板缓冲区
            Stencil {
                Ref 1        // 写入的模板值
                Comp Always  // 始终写入
                Pass Replace // 替换模板缓冲区的值
            }

            Blend SrcAlpha OneMinusSrcAlpha

            // 标准不透明渲染
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : SV_POSITION; 
            };

            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                return fixed4(1, 1, 1, 1); 
            }
            ENDCG
        }
    }
}
