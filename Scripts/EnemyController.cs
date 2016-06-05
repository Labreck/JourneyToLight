using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	//Stats
	[SerializeField]
	private float health = 100.0f;

	//Physic type stuff
	[SerializeField]
	private float moveSpeed = 2.0f;
	[SerializeField]
	private float fallSpeed = 2.5f;
	[SerializeField]
	private float targetSpace = 2.0f;
	private bool isGrounded = false; //Stores whether the player is touching the ground

	Transform target; // Holds the object that this object will chase

	void Start () {
		target = GameObject.Find("Player").transform; //When planning to effect things within the transform, I like to store the transform itself versus the GameObject, that way its one less thing to point to.
	}

	void Update () {
		transform.LookAt(target); // Makes sure this object is always facing the target
		CheckGrounded(); //same as how its done in the PlayerControllerScript
		if (Vector3.Distance(transform.position, target.position) >= targetSpace){ // If this object is further away than the space specified
			transform.position += transform.forward * moveSpeed * Time.deltaTime; //Go ahead and move toward it
		}
	}


	void CheckGrounded () { //Kinda an odd name for the function, for what it does
		if (isGrounded == false){
			transform.Translate(0, 0 - fallSpeed * Time.deltaTime, 0); //if this objectisn't touching a floor, it makes this object fall
		}
	}

	void OnTriggerEnter( Collider collision ){ //When this object collides with a trigger
		if (collision.gameObject.tag == "Floor"){ //If the trigger this object collided with is a floor
			isGrounded = true; //this object is touching the floor
		}
	}

	void OnTriggerExit( Collider collision ){
		if (collision.gameObject.tag == "Floor"){
			isGrounded = false; //If the player leaves a floor object, the player is no longer touching a floor
		}
	}

	public void TakeDamage( float damage ) { // Function that is called to do damage to this object
		health -= damage; // Subtract the damage that was passed to the function from this objects health
		if (health <= 0){ // If this object has no more health
			Die(); // Call the Die function that takes care of everything you want it to do when it dies
		}
	}

	void Die () {
		Destroy(gameObject); // Destroys object
	}
}

