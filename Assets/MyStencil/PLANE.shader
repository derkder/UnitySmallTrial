Shader "Custom/StencilTest" {
    Properties{
        _StencilComp ("Stencil Comparison", Float) = 0 //默认不开启
    }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Pass {
            // 使用模板缓冲区进行测试
            Stencil {
                Ref 1        // 参考值为1
                Comp [_StencilComp]  // 如果模板值不等于1，则通过测试
                Pass Keep     // 不修改模板缓冲区
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
                return fixed4(0, 1, 0, 1); // 如果没有通过模板测试，显示红色
            }
            ENDCG
        }
    }
}
