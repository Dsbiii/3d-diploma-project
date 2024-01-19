#pragma strict

var scalex : float = 0;
var scaley : float = 0;
var scalez : float = 0;


function Start () {

transform.position.x= transform.position.x + Random.value*scalex;
transform.position.z= transform.position.z + Random.value*scalez;
transform.position.y= transform.position.y + Random.value*scaley;

}

function Update () {

}