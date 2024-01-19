#pragma strict


function Start () {

}

function Update () {

}

function ScrlUp()
{
	if (transform.localPosition.y<4500) {transform.localPosition.y= transform.localPosition.y+1500;}

}

function ScrlDn()
{
	if (transform.localPosition.y>0) {transform.localPosition.y= transform.localPosition.y-1500;}

}