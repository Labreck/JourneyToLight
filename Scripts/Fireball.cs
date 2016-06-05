using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
	public Vector3 moveVec; // Variable that holds the direction to move the object in
	[SerializeField]
	private float moveSpeed; // The speed in which to move the object

	// Stats
	[SerializeField]
	private float baseDamage = 50.0f; // Just the base damage of the skill. Will eventually factor into a final damage
	[SerializeField]
	private float spCost = 10.0f;

	void Start () {
		Destroy(gameObject, 10); // Temporary way of controlling how many fireballs are in the game at once. After 10 seconds it will destroy
	}

	void Update () {
		transform.Translate(Vector3.Normalize(moveVec) * moveSpeed * Time.deltaTime); // Constantly moves the object every update. Vector3.Normalize is used to make the value of the vector always 1, but keeps the direction
	}

	void OnTriggerEnter ( Collider collision ) {
		if (collision.gameObject.tag == "Enemy"){ // If the object collides with an object with the tag Enemy
			collision.GetComponent<EnemyController>().TakeDamage(baseDamage); // Grab the script on the enemy collided with, and call TakeDamage inside that script, passing the baseDamage of this object
			Destroy(gameObject); // After doing it's job, destroy this object
		}
	}
}

