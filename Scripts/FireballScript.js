#pragma strict

var moveVec : Vector3; // Variable that holds the direction to move the object in
var moveSpeed : float; // The speed in which to move the object

// Stats
var baseDamage : float = 50; // Just the base damage of the skill. Will eventually factor into a final damage
var spCost : float = 10;

function Start () {
	Destroy(gameObject, 10); // Temporary way of controlling how many fireballs are in the game at once. After 10 seconds it will destroy
}

function Update () {
	transform.Translate(Vector3.Normalize(moveVec) * moveSpeed * Time.deltaTime); // Constantly moves the object every update. Vector3.Normalize is used to make the value of the vector always 1, but keeps the direction
}

function OnTriggerEnter ( collision : Collider ) {
	if (collision.gameObject.tag == "Enemy"){ // If the object collides with an object with the tag Enemy
		collision.GetComponent(EnemyScript).TakeDamage(baseDamage); // Grab the script on the enemy collided with, and call TakeDamage inside that script, passing the baseDamage of this object
		Destroy(gameObject); // After doing it's job, destroy this object
	}
}