using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public GameObject EnemySpawn;
    public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.E))
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy == null)
            {
                Instantiate(enemyPrefab, EnemySpawn.transform.position, Quaternion.identity);
            }
        }
	
	}
}
