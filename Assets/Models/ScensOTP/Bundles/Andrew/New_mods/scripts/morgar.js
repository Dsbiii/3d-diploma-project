#pragma strict
var objs: GameObject[];
var ticks: Array;
var cols: Color[];

var ss: int;
var spd: int;
var spdM:int;
var rend: Renderer[];


function Start () {
rend[0]= objs[0].GetComponent.<Renderer>();
rend[1]= objs[1].GetComponent.<Renderer>();
}

function Update () {
spd=spd+1;
if (spd==spdM) {
ss=ss+1;
spd=0;
}

//ss=ss+1;

if (ss>=cols.Length) {ss=0;}

rend[0].material.SetColor("_TintColor",cols[ss]);
rend[1].material.SetColor("_TintColor",cols[ss]);


}