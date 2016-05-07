#pragma strict
import UnityStandardAssets.CrossPlatformInput;

var forwardSpeed : float = 3; //Adjustable in the Inspector
var sideSpeed : float = 3; //Same here
var fallSpeed : float = 5; //And here
var jumpForce : float = 6;
var jumping : boolean = false;

@HideInInspector
var forwardMove : float; //variable that stores the vertical axis input
@HideInInspector
var sideMove : float; //stores the horizontal axis input
@HideInInspector
var isGrounded = false; //Stores whether the player is touching the ground
@HideInInspector
var collidingEnemy : boolean = false;
function Start () {

}

function Update () {
	Move(); //every frame, this function is called
	CheckGrounded(); //same here
}

function OnTriggerEnter( collision : Collider ){ //When the player collides with a trigger
	if (collision.gameObject.tag == "Floor"){ //If the trigger the player collided with is a floor
		isGrounded = true; //the player is touching the floor
		jumping = false;
	}
	if (collision.gameObject.tag == "Enemy"){
		collidingEnemy = true;
	}
}

function OnTriggerExit( collision : Collider ){
	if (collision.gameObject.tag == "Floor"){
		isGrounded = false; //If the player leaves a floor object, the player is no longer touching a floor
	}
}

function Jump() {
	if (isGrounded){
		transform.Translate(0, jumpForce * Time.smoothDeltaTime, 0);
		jumping = true;
	}
}

function Move (){
	forwardMove = CrossPlatformInputManager.GetAxisRaw("Vertical") * Time.deltaTime * forwardSpeed; //Grabs info from onscreen joystick
	sideMove = CrossPlatformInputManager.GetAxisRaw("Horizontal") * Time.deltaTime * sideSpeed;
	var moveVec : Vector3 = Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal") * sideSpeed * Time.deltaTime, 0, CrossPlatformInputManager.GetAxisRaw("Vertical") * forwardSpeed * Time.deltaTime);
	if (isGrounded || jumping){ //If the player is jumping, allow him to move, if he isnt jumping only allow the player to move if he is touching the ground. 
		transform.Translate(moveVec); //move the player accordingly with values that are stored above
	}

}

function CheckGrounded () { //Kinda an odd name for the function, for what it does
	if (isGrounded == false){
		transform.Translate(0, 0 - fallSpeed * Time.deltaTime, 0); //if the player isn't touching a floor, it makes the player fall
	}
}


