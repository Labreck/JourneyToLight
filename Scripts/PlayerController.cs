using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
	
	//Player values
	[SerializeField]
	private float maxHealth = 100.0f; //The max amount of health the player can have
	private float currentHealth; //What the player currently has for health
	private float calculatedHealth; // This value is used to properly scale the health bar

	[SerializeField]
	private float maxLight = 10.0f; //The required light to level up
	private float currentLight = 0.0f; //The amount of light the player currently has
	private float calculatedLight;

	[SerializeField]
	private float maxSp; //The max amount of sp the player can have
	private float currentSp; //The current amount the player has
	private int playerLevel = 1; //what level the player is

	public GameObject fireball;
	public GameObject aoe;

	Vector3 buttOneVec;
	Vector3 buttTwoVec;
	Vector3 buttThreeVec;

	private bool canAttack = true;

	Transform[] spawnPoints; // Stores all the transforms of the spawnpoints in the scene

	Transform spawnLocation; // stores the chosen spawnpoint transform

	//UI Transforms (the thing that holds scale values and stuff)
	public Transform healthBar; // This variable will hold the healthBar object so we can change the size and stuff
	public Transform spBar; // Same type of thing as above except will hold the spBar
	public Transform lightBar; // holds lightbar transform

	void Start () {
		currentHealth = maxHealth; //when the object with this script is first loaded, it sets the current health to what the max health is
		currentSp = maxSp; //same thing as above
		calculatedHealth = currentHealth / maxHealth; //This makes it so the value is between 0 and 1. Due to the bar scaling being between those values
		calculatedLight = currentLight / maxLight; // Does the same thing above except with the light
	}

	void Update () {
		RegenSp(); //Calls the function RegenSP every single frame
		UpdateSpBar(); // Calls Update Spbar every single frame
		UpdateHealthBar();
		UpdateLightBar();

		buttOneVec = new Vector3(CrossPlatformInputManager.GetAxisRaw("buttOneHor"), 0.0f, CrossPlatformInputManager.GetAxisRaw("buttOneVer"));

		if (Input.GetKeyDown(KeyCode.T)){ // *FOR TESTING PURPOSES* If the player presses T, it decreases 5 health
			DecreaseHealth(5);
		}
		if (Input.GetKeyDown(KeyCode.U)){ // *FOR TESTING PURPOSES* If the player presses U, it increases the players light by 1
			IncreaseLight(1);
		}
	}

	void OnTriggerEnter ( Collider collision ){
		if (collision.gameObject.tag == "Killbox"){
			Respawn();
		}
	}

	public void ButtonOnePressed () {
		if (buttOneVec != new Vector3(0.0f, 0.0f, 0.0f)){
			GameObject inst;
			//		var moveVec : Vector3 = Vector3(CrossPlatformInputManager.GetAxisRaw("buttOneHor"), 0, CrossPlatformInputManager.GetAxisRaw("buttOneVer"));
			inst = (GameObject)Instantiate(fireball, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			inst.GetComponent<Fireball>().moveVec = buttOneVec;
		}
	}

	public void ButtonTwoPressed () {
		Instantiate(aoe, transform.position, transform.rotation);
	}

	void Respawn(){
		spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)];
		transform.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
	}

	void DecreaseHealth ( float decreaseAmount ){ //Function that is called above
		currentHealth -= decreaseAmount; // Subtracts the decreaseAmount specified above from the current player Health
		if (currentHealth <= 0){
			currentHealth = 0;
		}
		calculatedHealth = currentHealth / maxHealth;
	}

	void UpdateHealthBar (){ // This function updates the Healthbar UI to current values
		healthBar.localScale = new Vector3(Mathf.Clamp(calculatedHealth,0,1), healthBar.localScale.y, healthBar.localScale.z); // This grabs the healthBars x scale from its localScale and tells it to equal the calculated health
	}

	void IncreaseLight ( float increaseAmount ){
		currentLight += increaseAmount; // Increases the players light by the amount specified when the function was called
		if (currentLight >= maxLight){ //if the player has more light than the max
			currentLight = maxLight; //This sets the players light to max so it isnt over the max. Eventually will cause a levelup and set back to 0
		}
		calculatedLight = currentLight / maxLight;
	}

	void UpdateLightBar () {
		Vector3 lightBarLScale = lightBar.localScale;
		lightBarLScale.x = Mathf.Clamp(calculatedLight, 0.0f, 1.0f); //Tells the Light bar to equal the current light
	}

	void RegenSp () { // This function will regenerate Sp evertime its called

	}

	void UpdateSpBar () { // This function will change the spBar UI to show current values

	}
}

