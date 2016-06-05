using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	GameObject[] enemySpawnPoints;

	Transform enemySpawnLocation;

	public GameObject enemy;

	private float spawnTimerMax = 10.0f;

	private float spawnTimer;

	void Start () {
		enemySpawnPoints = GameObject.FindGameObjectsWithTag ("EnemySpawn");
		spawnTimer = spawnTimerMax;
	}

	void Update () {
		if (spawnTimer > 0){
			spawnTimer -= 1 * Time.deltaTime;
		}
		if (spawnTimer <= 0){
			spawnEnemy();
			spawnTimer = spawnTimerMax;
		}


	}

	void spawnEnemy () {
		enemySpawnLocation = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].transform;
		Instantiate((GameObject)enemy, new Vector3(enemySpawnLocation.position.x, enemySpawnLocation.position.y, enemySpawnLocation.position.z), enemySpawnLocation.rotation);
	}
}

