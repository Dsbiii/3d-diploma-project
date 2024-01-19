#pragma strict
var c: Color;
var cnm = "_Color";
var o: GameObject;
var this_='this';

function Start () {
	
	var rend = GetComponent.<Renderer>();
	
	if (this_ != 'this')
	{ rend = o.GetComponent.<Renderer>();}
	
	
	
	
	rend.material.SetColor(cnm,c);
}

function Update () {

}