using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	Transform target; //Stores the class that holds position amongst other things
	[SerializeField]
	private float distanceFromTarget; // Distance from the player
	[SerializeField]
	private float heightFromTarget; //The height of the camera

	private Quaternion currentRotation;
	private float rotationX; // Allows the adjustment of the cemras rotation on the x axis
	private float smoothing;

	private Vector3 wantedPosition;

	private PlayerMovement targetMovement;

	void Start () {
		target = GameObject.Find ("Player").transform;
		targetMovement = target.GetComponent<PlayerMovement>();
	}

	void LateUpdate () {
		UpdateMovement();
	}

	void UpdateMovement () {
		if (!target){
			return;
		}
		Vector3 velocity = Vector3.zero;
		wantedPosition = new Vector3(target.position.x, target.position.y - heightFromTarget, target.position.z - distanceFromTarget);

		transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, ref velocity, smoothing * Time.deltaTime);

		// Always look at the target
		//transform.LookAt (target);
		currentRotation = transform.rotation;
		currentRotation.x =  rotationX;
	}
}

