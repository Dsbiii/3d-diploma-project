#pragma strict

var offset=0.5;
var os="x";
function Start () {

var rend = GetComponent.<Renderer>();
	rend.material.SetTextureOffset("_MainTex", Vector2(offset,0));
	if (os=='y') 
		{
		rend.material.SetTextureOffset("_MainTex", Vector2(0,1-offset));
		}

}

function Update () {

}