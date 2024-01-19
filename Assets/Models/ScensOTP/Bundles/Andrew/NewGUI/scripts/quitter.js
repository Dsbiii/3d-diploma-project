#pragma strict
var param = "click";

function Start () {

}

function Update () {

}

function OnMouseDown()
{
if (param == "click") {Exit();}

}


function Exit()
{
	Debug.Log("quit");
	Application.Quit();

}