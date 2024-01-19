#pragma strict

var offset=0.5;
var os="x";
var wak="";


function Start () {

var rend = GetComponent.<Renderer>();
	var offs: float = 0-offset / 20;
	
	//rend.material.SetTextureOffset("_MainTex", Vector2(0-offs,0));
	if (os=='y') 
		{
		rend.material.SetTextureOffset("_MainTex", Vector2(0,offs));
		}
		
	if (os=='x') 
		{
		rend.material.SetTextureOffset("_MainTex", Vector2(offs,0));
		if (wak=='WAK')
			{rend.material.SetTextureOffset("_MainTex", Vector2(offset,0));}
		}	
			

}

function Update () {

}