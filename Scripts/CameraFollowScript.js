#pragma strict

var target : Transform; //Stores the class that holds position amongst other things
var distanceFromTarget : float; // Distance from the player

var heightFromTarget : float; //The height of the camera

var rotationX : float; // Allows the adjustment of the cemras rotation on the x axis
var smoothing : float;

@HideInInspector
var wantedPosition : Vector3;

@HideInInspector
var targetMovementScript : PlayerMovement;

function Start () {

	targetMovementScript = target.GetComponent(PlayerMovement);
}

function LateUpdate () {
	UpdateMovement();
}

function UpdateMovement () {
	if (!target){
		return;
	}
	var velocity : Vector3 = Vector3.zero;
	wantedPosition = Vector3(target.position.x, target.position.y - heightFromTarget, target.position.z - distanceFromTarget);

 	transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, velocity, smoothing * Time.deltaTime);

 // Always look at the target
 	//transform.LookAt (target);
 


	transform.rotation.x =  rotationX;
}