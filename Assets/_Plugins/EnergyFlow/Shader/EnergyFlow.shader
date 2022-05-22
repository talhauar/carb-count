// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "EnergyFlow"
{
	Properties 
	{
_MainColor("Main Color", Color) = (1,1,1,1)
_Gloss("Gloss", Range(0.01,2) ) = 0.5
_Specular("Spec Color", Color) = (1,1,1,1)
_ReflPower("Reflect Power", Range(0,1) ) = 0
_MainTex("Base Texture (A) Mask", 2D) = "black" {}
_UVPanY("UV Y Speed", Range(-4,4) ) = 0
_UVPanX("UV X Speed", Range(-4,4) ) = 0
_EnergyColor("Energy Color", Color) = (1,1,1,1)
_EnergyTex("Energy Tex & Glow (RGB)", 2D) = "black" {}
_Normal("Normal Map", 2D) = "bump" {}
_Cube("Reflect Cube", Cube) = "black" {}

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}
		
		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
LOD 100
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 3.0


float4 _MainColor;
float _Gloss;
float4 _Specular;
float _ReflPower;
sampler2D _MainTex;
float _UVPanY;
float _UVPanX;
float4 _EnergyColor;
sampler2D _EnergyTex;
sampler2D _Normal;
samplerCUBE _Cube;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_MainTex;
float2 uv_EnergyTex;
float2 uv_Normal;
float3 simpleWorldRefl;

			};

			void vert (inout appdata_full v, out Input o) {
				UNITY_INITIALIZE_OUTPUT(Input, o);
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);

o.simpleWorldRefl = -reflect( normalize(WorldSpaceViewDir(v.vertex)), normalize(mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL)));

			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {

				
float4 Tex2D1=tex2D(_MainTex,(IN.uv_MainTex.xyxy).xy);
float4 Multiply5=Tex2D1 * _MainColor;
float4 Multiply1=_Time * _UVPanX.xxxx;
float4 UV_Pan0=float4((IN.uv_EnergyTex.xyxy).x + Multiply1.x,(IN.uv_EnergyTex.xyxy).y,(IN.uv_EnergyTex.xyxy).z,(IN.uv_EnergyTex.xyxy).w);
float4 Multiply4=_Time * _UVPanY.xxxx;
float4 UV_Pan1=float4((IN.uv_EnergyTex.xyxy).x,(IN.uv_EnergyTex.xyxy).y + Multiply4.x,(IN.uv_EnergyTex.xyxy).z,(IN.uv_EnergyTex.xyxy).w);
float4 Add0=UV_Pan0 + UV_Pan1;
float4 Tex2D3=tex2D(_EnergyTex,Add0.xy);
float4 Multiply6=Tex2D3 * _EnergyColor;
float4 Invert0= float4(1.0, 1.0, 1.0, 1.0) - Tex2D1.aaaa;
float4 Lerp0=lerp(Multiply5,Multiply6,Invert0);
float4 Tex2DNormal0=float4(UnpackNormal( tex2D(_Normal,(IN.uv_Normal.xyxy).xy)).xyz, 1.0 );
float4 Lerp3=lerp(Tex2DNormal0,Invert0,Invert0);
float4 Add2=Tex2DNormal0 + float4( IN.simpleWorldRefl.x, IN.simpleWorldRefl.y,IN.simpleWorldRefl.z,1.0 );
float4 TexCUBE0=texCUBE(_Cube,Add2);
float4 Multiply0=TexCUBE0 * _ReflPower.xxxx;
float4 Lerp2=lerp(Multiply0,Multiply6,Invert0);
float4 Add1=_Gloss.xxxx + Invert0;
float4 Lerp4=lerp(_Specular,Tex2D1.aaaa,Invert0);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Lerp0;
o.Normal = Lerp3;
o.Emission = Lerp2;
o.Specular = Add1;
o.Gloss = Lerp4;
o.Alpha = Invert0 * _EnergyColor.a;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}