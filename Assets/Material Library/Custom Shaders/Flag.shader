// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_LightmapInd', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D
// Upgrade NOTE: replaced tex2D unity_LightmapInd with UNITY_SAMPLE_TEX2D_SAMPLER

// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:True,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:0,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32226,y:32983|diff-46-OUT,spec-132-OUT,gloss-150-OUT,normal-124-OUT,clip-85-OUT;n:type:ShaderForge.SFN_Lerp,id:2,x:33059,y:32639|A-7-RGB,B-8-RGB,T-4-A;n:type:ShaderForge.SFN_Tex2d,id:4,x:33260,y:32726,ptlb:Color Mask,ptin:_ColorMask,tex:2f977e27e855d4e438ca11ec62a91c12,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5,x:33260,y:32548,ptlb:Detail Texture,ptin:_DetailTexture,tex:c2719b5f89259984e9be6269a6b22119,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6,x:33260,y:33087,ptlb:Flag Icon,ptin:_FlagIcon,tex:6ea2de20e23b0e746b368c6f7b792486,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:7,x:33462,y:32660,ptlb:Color A,ptin:_ColorA,glob:False,c1:0.4153144,c2:0.5468307,c3:0.9413793,c4:1;n:type:ShaderForge.SFN_Color,id:8,x:33462,y:32821,ptlb:Color B,ptin:_ColorB,glob:False,c1:1,c2:0.7231834,c3:0.05882353,c4:1;n:type:ShaderForge.SFN_Tex2dAsset,id:9,x:33260,y:32905,ptlb:Alpha Mask,ptin:_AlphaMask,glob:False,tex:bd2e30b993a3ede49981dd3ebca8c404;n:type:ShaderForge.SFN_Tex2d,id:10,x:32940,y:32771,tex:bd2e30b993a3ede49981dd3ebca8c404,ntxv:0,isnm:False|MIP-83-OUT,TEX-9-TEX;n:type:ShaderForge.SFN_Multiply,id:12,x:32879,y:32639|A-2-OUT,B-110-OUT;n:type:ShaderForge.SFN_OneMinus,id:32,x:33260,y:33266|IN-6-A;n:type:ShaderForge.SFN_SwitchProperty,id:33,x:33098,y:33116,ptlb:Invert Icon,ptin:_InvertIcon,on:True|A-6-A,B-32-OUT;n:type:ShaderForge.SFN_Color,id:35,x:32889,y:32485,ptlb:Icon Color,ptin:_IconColor,glob:False,c1:0.3965517,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:46,x:32697,y:32627|A-12-OUT,B-35-RGB,T-33-OUT;n:type:ShaderForge.SFN_Tex2d,id:68,x:32843,y:33002,tex:bd2e30b993a3ede49981dd3ebca8c404,ntxv:0,isnm:False|TEX-9-TEX;n:type:ShaderForge.SFN_Slider,id:83,x:32920,y:32915,ptlb:Border Shadow,ptin:_BorderShadow,min:0,cur:1,max:8;n:type:ShaderForge.SFN_Multiply,id:85,x:32662,y:33002|A-86-OUT,B-68-A;n:type:ShaderForge.SFN_ValueProperty,id:86,x:32843,y:33144,ptlb:Alpha Clip,ptin:_AlphaClip,glob:False,v1:1;n:type:ShaderForge.SFN_Color,id:102,x:32810,y:32889,ptlb:Shadow Color,ptin:_ShadowColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Blend,id:110,x:32634,y:32801,blmd:5,clmp:True|SRC-10-RGB,DST-102-RGB;n:type:ShaderForge.SFN_Tex2d,id:121,x:33017,y:33278,ptlb:Normal Map,ptin:_NormalMap,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Vector3,id:122,x:33017,y:33439,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_ValueProperty,id:123,x:33017,y:33559,ptlb:Normal Smooth,ptin:_NormalSmooth,glob:False,v1:0;n:type:ShaderForge.SFN_Lerp,id:124,x:32855,y:33278|A-121-RGB,B-122-OUT,T-123-OUT;n:type:ShaderForge.SFN_Multiply,id:132,x:32388,y:32242|A-5-A,B-85-OUT,C-133-OUT,D-142-OUT;n:type:ShaderForge.SFN_ValueProperty,id:133,x:32388,y:32394,ptlb:Main Spec Power,ptin:_MainSpecPower,glob:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:134,x:32956,y:32242|A-135-OUT,B-137-OUT,T-4-A;n:type:ShaderForge.SFN_ValueProperty,id:135,x:33154,y:32231,ptlb:Spec Color A,ptin:_SpecColorA,glob:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:137,x:33154,y:32306,ptlb:Spec Color B,ptin:_SpecColorB,glob:False,v1:0.5;n:type:ShaderForge.SFN_ValueProperty,id:139,x:32758,y:32276,ptlb:Spec Icon,ptin:_SpecIcon,glob:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:142,x:32559,y:32242|A-134-OUT,B-139-OUT,T-33-OUT;n:type:ShaderForge.SFN_Lerp,id:150,x:32602,y:33375|A-152-OUT,B-153-OUT,T-33-OUT;n:type:ShaderForge.SFN_ValueProperty,id:152,x:32764,y:33476,ptlb:Gloss Main,ptin:_GlossMain,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:153,x:32764,y:33559,ptlb:Gloss Icon,ptin:_GlossIcon,glob:False,v1:1;proporder:5-4-133-152-7-135-8-137-6-33-35-139-153-83-102-9-86-121-123;pass:END;sub:END;*/

Shader "ManyWorlds/Special/Flag" {
    Properties {
        _DetailTexture ("Detail Texture", 2D) = "white" {}
        _ColorMask ("Color Mask", 2D) = "bump" {}
        _MainSpecPower ("Main Spec Power", Float ) = 1
        _GlossMain ("Gloss Main", Float ) = 0
        _ColorA ("Color A", Color) = (0.4153144,0.5468307,0.9413793,1)
        _SpecColorA ("Spec Color A", Float ) = 0.2
        _ColorB ("Color B", Color) = (1,0.7231834,0.05882353,1)
        _SpecColorB ("Spec Color B", Float ) = 0.5
        _FlagIcon ("Flag Icon", 2D) = "white" {}
        [MaterialToggle] _InvertIcon ("Invert Icon", Float ) = 0
        _IconColor ("Icon Color", Color) = (0.3965517,0,0,1)
        _SpecIcon ("Spec Icon", Float ) = 1
        _GlossIcon ("Gloss Icon", Float ) = 1
        _BorderShadow ("Border Shadow", Range(0, 8)) = 1
        _ShadowColor ("Shadow Color", Color) = (0.5,0.5,0.5,1)
        _AlphaMask ("Alpha Mask", 2D) = "white" {}
        _AlphaClip ("Alpha Clip", Float ) = 1
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalSmooth ("Normal Smooth", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
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
            #pragma glsl
            #ifndef LIGHTMAP_OFF
                // float4 unity_LightmapST;
                // sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _ColorMask; uniform float4 _ColorMask_ST;
            uniform sampler2D _DetailTexture; uniform float4 _DetailTexture_ST;
            uniform sampler2D _FlagIcon; uniform float4 _FlagIcon_ST;
            uniform float4 _ColorA;
            uniform float4 _ColorB;
            uniform sampler2D _AlphaMask; uniform float4 _AlphaMask_ST;
            uniform fixed _InvertIcon;
            uniform float4 _IconColor;
            uniform float _BorderShadow;
            uniform float _AlphaClip;
            uniform float4 _ShadowColor;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _NormalSmooth;
            uniform float _MainSpecPower;
            uniform float _SpecColorA;
            uniform float _SpecColorB;
            uniform float _SpecIcon;
            uniform float _GlossMain;
            uniform float _GlossIcon;
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
                float2 node_171 = i.uv0;
                float3 normalLocal = lerp(UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_171.rg, _NormalMap))).rgb,float3(0,0,1),_NormalSmooth);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float node_85 = (_AlphaClip*tex2D(_AlphaMask,TRANSFORM_TEX(node_171.rg, _AlphaMask)).a);
                clip(node_85 - 0.5);
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
                #ifndef LIGHTMAP_OFF
                    float3 diffuse = lightmap.rgb;
                #else
                    float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
                #endif
///////// Gloss:
                float4 node_6 = tex2D(_FlagIcon,TRANSFORM_TEX(node_171.rg, _FlagIcon));
                float node_33 = lerp( node_6.a, (1.0 - node_6.a), _InvertIcon );
                float gloss = lerp(_GlossMain,_GlossIcon,node_33);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_4 = tex2D(_ColorMask,TRANSFORM_TEX(node_171.rg, _ColorMask));
                float node_132 = (tex2D(_DetailTexture,TRANSFORM_TEX(node_171.rg, _DetailTexture)).a*node_85*_MainSpecPower*lerp(lerp(_SpecColorA,_SpecColorB,node_4.a),_SpecIcon,node_33));
                float3 specularColor = float3(node_132,node_132,node_132);
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
                finalColor += diffuseLight * lerp((lerp(_ColorA.rgb,_ColorB.rgb,node_4.a)*saturate(max(tex2Dlod(_AlphaMask,float4(TRANSFORM_TEX(node_171.rg, _AlphaMask),0.0,_BorderShadow)).rgb,_ShadowColor.rgb))),_IconColor.rgb,node_33);
                finalColor += specular;
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
            Cull Off
            
            
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
            #pragma glsl
            #ifndef LIGHTMAP_OFF
                // float4 unity_LightmapST;
                // sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _ColorMask; uniform float4 _ColorMask_ST;
            uniform sampler2D _DetailTexture; uniform float4 _DetailTexture_ST;
            uniform sampler2D _FlagIcon; uniform float4 _FlagIcon_ST;
            uniform float4 _ColorA;
            uniform float4 _ColorB;
            uniform sampler2D _AlphaMask; uniform float4 _AlphaMask_ST;
            uniform fixed _InvertIcon;
            uniform float4 _IconColor;
            uniform float _BorderShadow;
            uniform float _AlphaClip;
            uniform float4 _ShadowColor;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _NormalSmooth;
            uniform float _MainSpecPower;
            uniform float _SpecColorA;
            uniform float _SpecColorB;
            uniform float _SpecIcon;
            uniform float _GlossMain;
            uniform float _GlossIcon;
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
                float2 node_172 = i.uv0;
                float3 normalLocal = lerp(UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_172.rg, _NormalMap))).rgb,float3(0,0,1),_NormalSmooth);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float node_85 = (_AlphaClip*tex2D(_AlphaMask,TRANSFORM_TEX(node_172.rg, _AlphaMask)).a);
                clip(node_85 - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float4 node_6 = tex2D(_FlagIcon,TRANSFORM_TEX(node_172.rg, _FlagIcon));
                float node_33 = lerp( node_6.a, (1.0 - node_6.a), _InvertIcon );
                float gloss = lerp(_GlossMain,_GlossIcon,node_33);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_4 = tex2D(_ColorMask,TRANSFORM_TEX(node_172.rg, _ColorMask));
                float node_132 = (tex2D(_DetailTexture,TRANSFORM_TEX(node_172.rg, _DetailTexture)).a*node_85*_MainSpecPower*lerp(lerp(_SpecColorA,_SpecColorB,node_4.a),_SpecIcon,node_33));
                float3 specularColor = float3(node_132,node_132,node_132);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * lerp((lerp(_ColorA.rgb,_ColorB.rgb,node_4.a)*saturate(max(tex2Dlod(_AlphaMask,float4(TRANSFORM_TEX(node_172.rg, _AlphaMask),0.0,_BorderShadow)).rgb,_ShadowColor.rgb))),_IconColor.rgb,node_33);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            #ifndef LIGHTMAP_OFF
                // float4 unity_LightmapST;
                // sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _AlphaMask; uniform float4 _AlphaMask_ST;
            uniform float _AlphaClip;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_173 = i.uv0;
                float node_85 = (_AlphaClip*tex2D(_AlphaMask,TRANSFORM_TEX(node_173.rg, _AlphaMask)).a);
                clip(node_85 - 0.5);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            #ifndef LIGHTMAP_OFF
                // float4 unity_LightmapST;
                // sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _AlphaMask; uniform float4 _AlphaMask_ST;
            uniform float _AlphaClip;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_174 = i.uv0;
                float node_85 = (_AlphaClip*tex2D(_AlphaMask,TRANSFORM_TEX(node_174.rg, _AlphaMask)).a);
                clip(node_85 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
