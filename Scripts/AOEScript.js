#pragma strict

var baseDamage : float = 50;
var spCost : float = 40;

function Start () {
	Destroy(gameObject, 1);
}

function Update () {

}

function OnTriggerEnter ( collision : Collider ) {
	if (collision.gameObject.tag == "Enemy"){
		collision.GetComponent(EnemyScript).TakeDamage(baseDamage);
	}
}

function Die(){
	Destroy(gameObject);
}