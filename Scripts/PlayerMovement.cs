using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{	 

	[SerializeField]
	private float forwardSpeed = 3.0f; //Adjustable in the Inspector
	[SerializeField]
	private float sideSpeed = 3.0f; //Same here

	[SerializeField]
	private float jumpHeight = 0.2f; //how height it will go
	private float wantedJumpHeight;
	private float currentJumpHeight; //directly fed into the players position
	private float jumpV; //variable used for smoothing
	private bool jumping;//if the play is jumping

	private float fallDistance;
	[SerializeField]
	private float fallSpeed = 0.1f; //And here

	private float forwardMove; //variable that stores the vertical axis input
	private float sideMove; //stores the horizontal axis input

	private bool isGrounded; //Stores whether the player is touching the ground

	private bool collidingEnemy;

	void Start () {
		
	}

	void Update () {Debug.Log ("isGrounded " + isGrounded);Debug.Log ("fallLength " + fallDistance);
		Debug.Log ("currentJumpHeight " + currentJumpHeight);
		Move(); //every frame, this function is called
		CheckGrounded(); //same here
		if (jumping) {
			currentJumpHeight = Mathf.SmoothDamp (currentJumpHeight, wantedJumpHeight, ref jumpV, 0.5f);
			if (currentJumpHeight > wantedJumpHeight) {
				jumping = false;
			}
		} else {
			currentJumpHeight = Mathf.SmoothDamp (currentJumpHeight, 0, ref jumpV, 0.5f);
		}
	}

	void OnTriggerEnter( Collider collision ){ //When the player collides with a trigger
		if (collision.gameObject.tag == "Floor"){ //If the trigger the player collided with is a floor
			isGrounded = true; //the player is touching the floor
			fallDistance = 0;
			//jumping = false;
		}
		if (collision.gameObject.tag == "Enemy"){
			collidingEnemy = true;
		}
	}

	void OnTriggerExit( Collider collision ){
		if (collision.gameObject.tag == "Floor"){
			isGrounded = false; //If the player leaves a floor object, the player is no longer touching a floor
		}
	}

	public void Jump() {
		if (isGrounded){
			jumping = true;
			wantedJumpHeight = jumpHeight;
		}
	}

	void Move (){
		forwardMove = CrossPlatformInputManager.GetAxisRaw("Vertical") * Time.deltaTime * forwardSpeed; //Grabs info from onscreen joystick
		sideMove = CrossPlatformInputManager.GetAxisRaw("Horizontal") * Time.deltaTime * sideSpeed;

		Vector3 moveVec = new Vector3 (CrossPlatformInputManager.GetAxisRaw ("Horizontal") * sideSpeed * Time.deltaTime, fallDistance, CrossPlatformInputManager.GetAxisRaw ("Vertical") * forwardSpeed * Time.deltaTime) + new Vector3 (0.0f, currentJumpHeight, 0.0f);;
		if (isGrounded || jumping){ //If the player is jumping, allow him to move, if he isnt jumping only allow the player to move if he is touching the ground. 
			transform.position = transform.position + moveVec;
		}
	}

	void CheckGrounded () { //Kinda an odd name for the function, for what it does
		if (!isGrounded && !jumping){
			
			//currentJumpHeight = Mathf.SmoothDamp(currentJumpHeight, 0.0f, ref jumpV, 0.5f);

		}
		if (!isGrounded){
			fallDistance -= fallSpeed * Time.smoothDeltaTime;
			//transform.Translate(0, - fallSpeed * Time.smoothDeltaTime, 0); //if the player isn't touching a floor, it makes the player fall
		}
	}
}