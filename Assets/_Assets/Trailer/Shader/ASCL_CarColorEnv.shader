Shader "ASCL/CarColorEnv" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_DiffuseBoost ("Diffuse Boost", Range(0.03, 3)) = 1
		_MainTex ("Diffuse Map", 2D) = "white" {}
		_SpecColor ("Spec Lighting Color", Vector) = (0.5,0.5,0.5,1)
		_Gloss ("(R)Specular Boost", Range(0.03, 10)) = 0.078125
		_Spec ("(G)Gloss Boost", Range(0.01, 0.3)) = 0.078125
		_Cube ("Cubemap", Cube) = "" {}
		_EnvBoost ("(A)Reflection Boost", Range(0.03, 1)) = 0.078125
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "VertexLit"
}