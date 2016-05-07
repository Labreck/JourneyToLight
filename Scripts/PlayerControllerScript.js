#pragma strict

import UnityStandardAssets.CrossPlatformInput;


//Player values
var maxHealth : float = 100; //The max amount of health the player can have
@HideInInspector
var currentHealth : float; //What the player currently has for health
@HideInInspector
var calculatedHealth : float; // This value is used to properly scale the health bar
var maxLight : float = 10; //The required light to level up
@HideInInspector
var currentLight : float = 0; //The amount of light the player currently has
@HideInInspector
var calculatedLight : float;
var maxSp : float; //The max amount of sp the player can have
@HideInInspector
var currentSp : float; //The current amount the player has
var playerLevel : int = 1; //what level the player is

var fireball : GameObject;
var aoe : GameObject;

var buttOneVec : Vector3;
var buttTwoVec : Vector3;
var buttThreeVec : Vector3;

var canAttack : boolean = true;

var spawnPoints : Transform[]; // Stores all the transforms of the spawnpoints in the scene
@HideInInspector
var spawnLocation : Transform; // stores the chosen spawnpoint transform

//UI Transforms (the thing that holds scale values and stuff)
var healthBar : Transform; // This variable will hold the healthBar object so we can change the size and stuff
var spBar : Transform; // Same type of thing as above except will hold the spBar
var lightBar : Transform; // holds lightbar transform

function Start () {
	currentHealth = maxHealth; //when the object with this script is first loaded, it sets the current health to what the max health is
	currentSp = maxSp; //same thing as above
	calculatedHealth = currentHealth / maxHealth; //This makes it so the value is between 0 and 1. Due to the bar scaling being between those values
	calculatedLight = currentLight / maxLight; // Does the same thing above except with the light
}

function Update () {
	RegenSp(); //Calls the function RegenSP every single frame
	UpdateSpBar(); // Calls Update Spbar every single frame
	UpdateHealthBar();
	UpdateLightBar();

	buttOneVec = Vector3(CrossPlatformInputManager.GetAxisRaw("buttOneHor"), 0, CrossPlatformInputManager.GetAxisRaw("buttOneVer"));

	if (Input.GetKeyDown(KeyCode.T)){ // *FOR TESTING PURPOSES* If the player presses T, it decreases 5 health
		DecreaseHealth(5);
	}
	if (Input.GetKeyDown(KeyCode.U)){ // *FOR TESTING PURPOSES* If the player presses U, it increases the players light by 1
		IncreaseLight(1);
	}
}

function OnTriggerEnter ( collision : Collider ){
	if (collision.gameObject.tag == "Killbox"){
		Respawn();
	}
}

function ButtonOnePressed () {
	if (buttOneVec != Vector3(0, 0, 0)){
		var inst : GameObject;
//		var moveVec : Vector3 = Vector3(CrossPlatformInputManager.GetAxisRaw("buttOneHor"), 0, CrossPlatformInputManager.GetAxisRaw("buttOneVer"));
		inst = Instantiate(fireball, Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
		inst.GetComponent(FireballScript).moveVec = buttOneVec;
	}
}

function ButtonTwoPressed () {
	Instantiate(aoe, transform.position, transform.rotation);
}

function Respawn(){
	spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)];
	transform.position = Vector3(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z);
}

function DecreaseHealth ( decreaseAmount : float ){ //Function that is called above
	currentHealth -= decreaseAmount; // Subtracts the decreaseAmount specified above from the current player Health
	if (currentHealth <= 0){
		currentHealth = 0;
	}
	calculatedHealth = currentHealth / maxHealth;
}

function UpdateHealthBar (){ // This function updates the Healthbar UI to current values
	healthBar.localScale = Vector3(Mathf.Clamp(calculatedHealth,0,1), healthBar.localScale.y, healthBar.localScale.z); // This grabs the healthBars x scale from its localScale and tells it to equal the calculated health
}

function IncreaseLight ( increaseAmount : float ){
	currentLight += increaseAmount; // Increases the players light by the amount specified when the function was called
	if (currentLight >= maxLight){ //if the player has more light than the max
		currentLight = maxLight; //This sets the players light to max so it isnt over the max. Eventually will cause a levelup and set back to 0
	}
	calculatedLight = currentLight / maxLight;
}

function UpdateLightBar () {
	lightBar.localScale.x = Mathf.Clamp(calculatedLight, 0, 1); //Tells the Light bar to equal the current light
}

function RegenSp () { // This function will regenerate Sp evertime its called

}

function UpdateSpBar () { // This function will change the spBar UI to show current values

}