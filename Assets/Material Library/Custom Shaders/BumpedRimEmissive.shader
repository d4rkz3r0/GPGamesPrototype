// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_LightmapInd', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D
// Upgrade NOTE: replaced tex2D unity_LightmapInd with UNITY_SAMPLE_TEX2D_SAMPLER

// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:Reflective/Bumped Specular,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:True,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:31959,y:32620|diff-8-OUT,spec-28-OUT,gloss-45-OUT,normal-51-RGB,emission-249-OUT,transm-123-OUT,amdfl-113-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33024,y:32372,ptlb:Base,ptin:_Base,ntxv:0,isnm:False|UVIN-320-OUT;n:type:ShaderForge.SFN_Multiply,id:8,x:32778,y:32344|A-2-RGB,B-17-RGB;n:type:ShaderForge.SFN_Color,id:17,x:33024,y:32569,ptlb:Color Tint,ptin:_ColorTint,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:28,x:32823,y:32646|A-35-RGB,B-29-OUT;n:type:ShaderForge.SFN_Slider,id:29,x:33197,y:32391,ptlb:Specular Level,ptin:_SpecularLevel,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Tex2d,id:35,x:33197,y:32539,ptlb:Specular (RGB) Gloss (A),ptin:_SpecularRGBGlossA,ntxv:0,isnm:False|UVIN-320-OUT;n:type:ShaderForge.SFN_Slider,id:43,x:33179,y:32729,ptlb:Gloss Level,ptin:_GlossLevel,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:45,x:32876,y:32721|A-35-A,B-43-OUT;n:type:ShaderForge.SFN_Tex2d,id:51,x:32982,y:32871,ptlb:Normal Map,ptin:_NormalMap,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Fresnel,id:78,x:33323,y:32984|EXP-102-OUT;n:type:ShaderForge.SFN_Color,id:84,x:33308,y:33144,ptlb:Rim Color,ptin:_RimColor,glob:False,c1:0.9926471,c2:0.9926471,c3:0.9926471,c4:1;n:type:ShaderForge.SFN_Slider,id:102,x:33443,y:33079,ptlb:Rim Level,ptin:_RimLevel,min:0,cur:2.805166,max:10;n:type:ShaderForge.SFN_Blend,id:113,x:33102,y:33071,blmd:1,clmp:True|SRC-78-OUT,DST-84-RGB;n:type:ShaderForge.SFN_Blend,id:123,x:32633,y:33112,blmd:10,clmp:True|SRC-124-RGB,DST-125-OUT;n:type:ShaderForge.SFN_Color,id:124,x:32832,y:33037,ptlb:Transmission Color,ptin:_TransmissionColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Fresnel,id:125,x:32834,y:33234|EXP-176-OUT;n:type:ShaderForge.SFN_NormalVector,id:153,x:32994,y:33131,pt:False;n:type:ShaderForge.SFN_Slider,id:176,x:32934,y:33352,ptlb:Transmission Fresnel,ptin:_TransmissionFresnel,min:0,cur:0.06429847,max:1;n:type:ShaderForge.SFN_Tex2d,id:187,x:32589,y:33278,ptlb:Emissive Map,ptin:_EmissiveMap,tex:bc3be832b0b46824f9891a81e51b5c8a,ntxv:0,isnm:False|UVIN-320-OUT;n:type:ShaderForge.SFN_Blend,id:188,x:32030,y:33153,blmd:3,clmp:True|SRC-206-OUT,DST-189-OUT;n:type:ShaderForge.SFN_Slider,id:189,x:31942,y:33370,ptlb:Emissive Power,ptin:_EmissivePower,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Color,id:205,x:32800,y:33625,ptlb:Emissive Color,ptin:_EmissiveColor,glob:False,c1:0.8088235,c2:0.8088235,c3:0.8088235,c4:1;n:type:ShaderForge.SFN_Multiply,id:206,x:32515,y:33606|A-218-OUT,B-205-RGB;n:type:ShaderForge.SFN_SwitchProperty,id:218,x:32237,y:33827,ptlb:Inverse,ptin:_Inverse,on:False|A-187-RGB,B-238-OUT;n:type:ShaderForge.SFN_OneMinus,id:238,x:32198,y:33621|IN-187-RGB;n:type:ShaderForge.SFN_Multiply,id:249,x:31647,y:33341|A-188-OUT,B-251-OUT;n:type:ShaderForge.SFN_Slider,id:251,x:31726,y:33513,ptlb:Emission Multiply,ptin:_EmissionMultiply,min:0,cur:1,max:4;n:type:ShaderForge.SFN_Sin,id:263,x:31649,y:33833|IN-296-OUT;n:type:ShaderForge.SFN_Pi,id:264,x:31962,y:33780;n:type:ShaderForge.SFN_Time,id:281,x:32035,y:33976;n:type:ShaderForge.SFN_Multiply,id:282,x:31469,y:33843|A-263-OUT,B-303-OUT;n:type:ShaderForge.SFN_Panner,id:283,x:31469,y:33655,spu:1,spv:1|DIST-282-OUT;n:type:ShaderForge.SFN_Multiply,id:296,x:31841,y:33899|A-264-OUT,B-281-T;n:type:ShaderForge.SFN_ValueProperty,id:303,x:31710,y:34081,ptlb:Move Intensity,ptin:_MoveIntensity,glob:False,v1:0.01;n:type:ShaderForge.SFN_SwitchProperty,id:320,x:32774,y:33448,ptlb:Movement,ptin:_Movement,on:False|A-321-UVOUT,B-283-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:321,x:33034,y:33497,uv:0;proporder:17-2-51-29-35-43-84-102-124-176-187-218-189-251-205-320-303;pass:END;sub:END;*/

Shader "ManyWorlds/Bumped/BumpedRimEmissive" {
    Properties {
        _ColorTint ("Color Tint", Color) = (0.5,0.5,0.5,1)
        _Base ("Base", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _SpecularLevel ("Specular Level", Range(0, 10)) = 0
        _SpecularRGBGlossA ("Specular (RGB) Gloss (A)", 2D) = "white" {}
        _GlossLevel ("Gloss Level", Range(0, 1)) = 0
        _RimColor ("Rim Color", Color) = (0.9926471,0.9926471,0.9926471,1)
        _RimLevel ("Rim Level", Range(0, 10)) = 2.805166
        _TransmissionColor ("Transmission Color", Color) = (0.5,0.5,0.5,1)
        _TransmissionFresnel ("Transmission Fresnel", Range(0, 1)) = 0.06429847
        _EmissiveMap ("Emissive Map", 2D) = "white" {}
        [MaterialToggle] _Inverse ("Inverse", Float ) = 0.9686275
        _EmissivePower ("Emissive Power", Range(0, 2)) = 1
        _EmissionMultiply ("Emission Multiply", Range(0, 4)) = 1
        _EmissiveColor ("Emissive Color", Color) = (0.8088235,0.8088235,0.8088235,1)
        [MaterialToggle] _Movement ("Movement", Float ) = 0
        _MoveIntensity ("Move Intensity", Float ) = 0.01
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            #ifndef LIGHTMAP_OFF
                // float4 unity_LightmapST;
                // sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform float4 _ColorTint;
            uniform float _SpecularLevel;
            uniform sampler2D _SpecularRGBGlossA; uniform float4 _SpecularRGBGlossA_ST;
            uniform float _GlossLevel;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float4 _RimColor;
            uniform float _RimLevel;
            uniform float4 _TransmissionColor;
            uniform float _TransmissionFresnel;
            uniform sampler2D _EmissiveMap; uniform float4 _EmissiveMap_ST;
            uniform float _EmissivePower;
            uniform float4 _EmissiveColor;
            uniform fixed _Inverse;
            uniform float _EmissionMultiply;
            uniform float _MoveIntensity;
            uniform fixed _Movement;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                #ifndef LIGHTMAP_OFF
                    float2 uvLM : TEXCOORD7;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                #ifndef LIGHTMAP_OFF
                    o.uvLM = v.texcoord1 * unity_LightmapST.xy + unity_LightmapST.zw;
                #endif
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_344 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_344.rg, _NormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                #ifndef LIGHTMAP_OFF
                    float4 lmtex = UNITY_SAMPLE_TEX2D(unity_Lightmap,i.uvLM);
                    #ifndef DIRLIGHTMAP_OFF
                        float3 lightmap = DecodeLightmap(lmtex);
                        float3 scalePerBasisVector = DecodeLightmap(UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd,unity_Lightmap,i.uvLM));
                        UNITY_DIRBASIS
                        half3 normalInRnmBasis = saturate (mul (unity_DirBasis, normalLocal));
                        lightmap *= dot (normalInRnmBasis, scalePerBasisVector);
                    #else
                        float3 lightmap = DecodeLightmap(lmtex);
                    #endif
                #endif
                #ifndef LIGHTMAP_OFF
                    #ifdef DIRLIGHTMAP_OFF
                        float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                    #else
                        float3 lightDirection = normalize (scalePerBasisVector.x * unity_DirBasis[0] + scalePerBasisVector.y * unity_DirBasis[1] + scalePerBasisVector.z * unity_DirBasis[2]);
                        lightDirection = mul(lightDirection,tangentTransform); // Tangent to world
                    #endif
                #else
                    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                #endif
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 forwardLight = max(0.0, NdotL );
                float3 backLight = max(0.0, -NdotL ) * saturate(( pow(1.0-max(0,dot(normalDirection, viewDirection)),_TransmissionFresnel) > 0.5 ? (1.0-(1.0-2.0*(pow(1.0-max(0,dot(normalDirection, viewDirection)),_TransmissionFresnel)-0.5))*(1.0-_TransmissionColor.rgb)) : (2.0*pow(1.0-max(0,dot(normalDirection, viewDirection)),_TransmissionFresnel)*_TransmissionColor.rgb) ));
                #ifndef LIGHTMAP_OFF
                    float3 diffuse = lightmap.rgb;
                #else
                    float3 diffuse = (forwardLight+backLight) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
                #endif
////// Emissive:
                float4 node_281 = _Time + _TimeEditor;
                float2 node_320 = lerp( i.uv0.rg, (node_344.rg+(sin((3.141592654*node_281.g))*_MoveIntensity)*float2(1,1)), _Movement );
                float4 node_187 = tex2D(_EmissiveMap,TRANSFORM_TEX(node_320, _EmissiveMap));
                float3 emissive = (saturate(((lerp( node_187.rgb, (1.0 - node_187.rgb), _Inverse )*_EmissiveColor.rgb)+_EmissivePower-1.0))*_EmissionMultiply);
///////// Gloss:
                float4 node_35 = tex2D(_SpecularRGBGlossA,TRANSFORM_TEX(node_320, _SpecularRGBGlossA));
                float gloss = (node_35.a*_GlossLevel);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float3 specularColor = (node_35.rgb*_SpecularLevel);
                float3 specular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                #ifndef LIGHTMAP_OFF
                    #ifndef DIRLIGHTMAP_OFF
                        specular *= lightmap;
                    #else
                        specular *= (floor(attenuation) * _LightColor0.xyz);
                    #endif
                #else
                    specular *= (floor(attenuation) * _LightColor0.xyz);
                #endif
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                diffuseLight += saturate((pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimLevel)*_RimColor.rgb)); // Diffuse Ambient Light
                finalColor += diffuseLight * (tex2D(_Base,TRANSFORM_TEX(node_320, _Base)).rgb*_ColorTint.rgb);
                finalColor += specular;
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            #ifndef LIGHTMAP_OFF
                // float4 unity_LightmapST;
                // sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform float4 _ColorTint;
            uniform float _SpecularLevel;
            uniform sampler2D _SpecularRGBGlossA; uniform float4 _SpecularRGBGlossA_ST;
            uniform float _GlossLevel;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float4 _TransmissionColor;
            uniform float _TransmissionFresnel;
            uniform sampler2D _EmissiveMap; uniform float4 _EmissiveMap_ST;
            uniform float _EmissivePower;
            uniform float4 _EmissiveColor;
            uniform fixed _Inverse;
            uniform float _EmissionMultiply;
            uniform float _MoveIntensity;
            uniform fixed _Movement;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_345 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_345.rg, _NormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 forwardLight = max(0.0, NdotL );
                float3 backLight = max(0.0, -NdotL ) * saturate(( pow(1.0-max(0,dot(normalDirection, viewDirection)),_TransmissionFresnel) > 0.5 ? (1.0-(1.0-2.0*(pow(1.0-max(0,dot(normalDirection, viewDirection)),_TransmissionFresnel)-0.5))*(1.0-_TransmissionColor.rgb)) : (2.0*pow(1.0-max(0,dot(normalDirection, viewDirection)),_TransmissionFresnel)*_TransmissionColor.rgb) ));
                float3 diffuse = (forwardLight+backLight) * attenColor;
///////// Gloss:
                float4 node_281 = _Time + _TimeEditor;
                float2 node_320 = lerp( i.uv0.rg, (node_345.rg+(sin((3.141592654*node_281.g))*_MoveIntensity)*float2(1,1)), _Movement );
                float4 node_35 = tex2D(_SpecularRGBGlossA,TRANSFORM_TEX(node_320, _SpecularRGBGlossA));
                float gloss = (node_35.a*_GlossLevel);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float3 specularColor = (node_35.rgb*_SpecularLevel);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (tex2D(_Base,TRANSFORM_TEX(node_320, _Base)).rgb*_ColorTint.rgb);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Reflective/Bumped Specular"
    CustomEditor "ShaderForgeMaterialInspector"
}
