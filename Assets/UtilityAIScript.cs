using UnityEngine;
using System.Collections;

public class UtilityAIScript : MonoBehaviour {

    GameObject enemyObj;
    NPCMovementScript movementScript;
    float distance;
    float anxiety;

	// Use this for initialization
	void Start () {
        enemyObj = GameObject.Find("Enemy");
        if (enemyObj != null)
        {
            movementScript = enemyObj.GetComponent<NPCMovementScript>();
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        distance = movementScript.distance;
        CalculateAnxiety(distance);

    }

    void CalculateAnxiety(float distance)
    {
        anxiety = 1 / (1 + Mathf.Pow(distance + 40, 2.718f*0.45f));
        Debug.Log("Anxiety: " + anxiety.ToString());
    }
}
