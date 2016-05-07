#pragma strict

//Stats
var health : float = 100;

//Physic type stuff
var moveSpeed = 2;
var fallSpeed = 2.5;
var targetSpace = 2;
@HideInInspector
var isGrounded = false; //Stores whether the player is touching the ground


@HideInInspector
var target : Transform; // Holds the object that this object will chase

function Start () {
	target = GameObject.Find("Player").transform; //When planning to effect things within the transform, I like to store the transform itself versus the GameObject, that way its one less thing to point to.
}

function Update () {
	transform.LookAt(target); // Makes sure this object is always facing the target
	CheckGrounded(); //same as how its done in the PlayerControllerScript
	if (Vector3.Distance(transform.position, target.position) >= targetSpace){ // If this object is further away than the space specified
		transform.position += transform.forward * moveSpeed * Time.deltaTime; //Go ahead and move toward it
	}
}


function CheckGrounded () { //Kinda an odd name for the function, for what it does
	if (isGrounded == false){
		transform.Translate(0, 0 - fallSpeed * Time.deltaTime, 0); //if this objectisn't touching a floor, it makes this object fall
	}
}

function OnTriggerEnter( collision : Collider ){ //When this object collides with a trigger
	if (collision.gameObject.tag == "Floor"){ //If the trigger this object collided with is a floor
		isGrounded = true; //this object is touching the floor
	}
}

function OnTriggerExit( collision : Collider ){
	if (collision.gameObject.tag == "Floor"){
		isGrounded = false; //If the player leaves a floor object, the player is no longer touching a floor
	}
}

function TakeDamage( damage : float ) { // Function that is called to do damage to this object
	health -= damage; // Subtract the damage that was passed to the function from this objects health
	if (health <= 0){ // If this object has no more health
		Die(); // Call the Die function that takes care of everything you want it to do when it dies
	}
}

function Die () {
	Destroy(gameObject); // Destroys object
}