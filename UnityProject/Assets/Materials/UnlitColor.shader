Shader "Unlit Single Color" 
{
    Properties 
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {} 
        _ColorProps ("Color Proportions", Range(0,1)) = 0.5
    }
    
    SubShader 
    {
    	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
//    	LOD 100
    			
    	Lighting Off
    	ZWrite Off
    	Cull Back
		Blend SrcAlpha OneMinusSrcAlpha 
    	 
    	Pass
    	{
	        CGPROGRAM
	
	        #pragma vertex vert
	        #pragma fragment frag
	        #include "UnityCG.cginc"
	
	        float4 _MainColor; 
	        float _ColorProps;
	        
	        sampler2D _MainTex;
	        float4 _MainTex_ST;
	
	        struct v2f 
	        {
	            float4 pos : SV_POSITION; 
	            float2 uv : TEXCOORD0;
	        };
	        
	       	v2f vert (appdata_base v) 
	       	{
	       	     v2f o; 
	       	     o.pos = mul (UNITY_MATRIX_MVP, v.vertex); 
	       	     o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
	       	     return o;
	       	}
		   	
	       	float4 frag (v2f i) : COLOR
	       	{ 
	       		float4 texCol = tex2D(_MainTex, i.uv);
				return float4( texCol.rgb * _ColorProps + _MainColor.rgb * (1 - _ColorProps), texCol.a * _MainColor.a);
//	       		return texCol * _MainColor;
	       	} 
	       	 
	       	ENDCG 
       	}
    } 
}