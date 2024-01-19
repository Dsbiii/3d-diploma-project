#pragma strict
var trg : GameObject [];
function Start () {

}

function Update () {

}

function retarget( tg: String)
{
	var xx : int = parseInt(tg);
	transform.position.x = trg[xx].transform.position.x;

}