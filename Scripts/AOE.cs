using UnityEngine;
using System.Collections;

public class AOE : MonoBehaviour {
	[SerializeField]
	private float baseDamage = 50.0f;
	[SerializeField]
	private float spCost = 40;

	void Start () {
		Destroy(gameObject, 1);
	}

	void OnTriggerEnter ( Collider collision ) {
		if (collision.gameObject.tag == "Enemy"){
			collision.GetComponent<EnemyController>().TakeDamage(baseDamage);
		}
	}

	void Die(){
		Destroy(gameObject);
	}
}
