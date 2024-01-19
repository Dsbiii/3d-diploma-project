#pragma strict

private var namelev: String;
private var modelev: String;


private var mode='t';
private var sc='0';

var Goo: GameObject;


function Start () {

}

function Update () {

}

function SelectMode(param: String)
{
	mode = param;

}

function SelectLev(param: String)
{
	sc = param;

}

function OnMouseDown()
{
//Application.LoadLevel(namelev);

	//Debug.Log("msg");
	
	Goo.SendMessage("Sel",sc+mode);

}
