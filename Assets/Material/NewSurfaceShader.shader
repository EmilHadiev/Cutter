Shader "Mobile/NewSurfaceShader"
{
    Properties 
        {
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _Color ("Color", Color) = (1,1,1,1) // Белый по умолчанию
        }
    
        SubShader 
        {
            Tags { "RenderType"="Opaque" }
            LOD 150
        
            CGPROGRAM
            #pragma surface surf Lambert noforwardadd finalcolor:mycolor
            #pragma target 2.0 // Для лучшей совместимости с WebGL

            sampler2D _MainTex;
            fixed4 _Color;

            struct Input 
            {
                float2 uv_MainTex;
            };

            // Функция для окончательной настройки цвета
            void mycolor(Input IN, SurfaceOutput o, inout fixed4 color)
            {
                color *= _Color;
            }

            void surf(Input IN, inout SurfaceOutput o) 
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
            }
            ENDCG
        }
    
    Fallback "Mobile/Diffuse"
}