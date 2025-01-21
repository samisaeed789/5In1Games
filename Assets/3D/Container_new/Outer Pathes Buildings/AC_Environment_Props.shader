Shader "AC/Environment/Props" {
	Properties {
		_Metallic ("Metallic", Range(0, 1)) = 0
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_SpecularMul ("Spec Multiplier", Range(0, 1)) = 0.5
		[HideInInspector] _SpecColor ("Spec Color", Vector) = (1,1,1,1)
		[Space(10)] _Color ("Color (NotInUse)", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
		[Space(10)] [NoScaleOffset] _DetailAlbedoMap ("Detail Albedo x2 UV2", 2D) = "gray" {}
		[Space(10)] [Toggle(USE_EMISSION_MAP)] _EMISSION ("Use Emission", Float) = 0
		_EmissionColor ("Emission Color", Vector) = (0,0,0,1)
		[NoScaleOffset] _EmissionMap ("Emission Map", 2D) = "gray" {}
		[Space(10)] [Toggle(USE_ALBEDO_ALPHA_ROUGHNESS)] _UseRoughnessAlpha ("Roughness AlbedoAlpha", Float) = 0
		[ToggleOff] _SpecularHighlights ("Specular Highlights", Float) = 1
		[ToggleOff] _GlossyReflections ("Glossy Reflections", Float) = 1
		[Space(10)] [Toggle(USE_VERTEX_LIGHT)] _UseVertexLight ("Use Vertex Lights", Float) = 0
		[Space(10)] [Toggle(USE_CUTOUT)] _Cutout ("Use Cutout", Float) = 0
		[Enum(Both,0,Back,1,Front,2)] _Cull ("Backface Culling", Float) = 2
		_OffsetFactor ("Z-Offset Factor", Float) = 0
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
	Fallback "AC/MISC/ShadowCaster"
}