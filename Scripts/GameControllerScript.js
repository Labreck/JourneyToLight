#pragma strict
var enemySpawnPoints : Transform[];
@HideInInspector
var enemySpawnLocation : Transform;

var enemy : GameObject;

var spawnTimerMax : float = 10;
@HideInInspector
var spawnTimer : float = spawnTimerMax;

function Start () {
	
}

function Update () {
	if (spawnTimer > 0){
		spawnTimer -= 1 * Time.deltaTime;
	}
	if (spawnTimer <= 0){
		spawnEnemy();
		spawnTimer = spawnTimerMax;
	}


}

function spawnEnemy () {
	enemySpawnLocation = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
	Instantiate(enemy, Vector3(enemySpawnLocation.position.x, enemySpawnLocation.position.y, enemySpawnLocation.position.z), enemySpawnLocation.rotation);
}