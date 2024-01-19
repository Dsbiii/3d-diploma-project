#pragma strict
import UnityEngine.UI;

var Goo: GameObject;

private var mode='t';
private var sc='0';

function Start () {

}

function Update () {

}

function SetMode(param: String)
{
	mode = param;

}

function SetScene(param: String)
{
	sc = param;

}

function OnMouseDown() 
{
	Debug.Log("msg");
	
	Goo.SendMessage("Sel",sc+mode);
	

}