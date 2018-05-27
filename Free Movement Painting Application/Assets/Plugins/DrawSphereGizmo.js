#pragma strict

var radius : float = 1;

function Start () {

}

function Update () {

}

function OnDrawGizmos () {
	Gizmos.DrawWireSphere(transform.position,radius);
}