using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UtilityAIScript : MonoBehaviour {

    GameObject enemyObj;
    NPCMovementScript movementScript;
    float distance;
    public float anxiety;
    public Text anxietyTextObj;

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
        anxietyTextObj.text = "Anxiety: " + anxiety;
        //Debug.Log("Anxiety: " + anxiety.ToString()+ " at Distance " + distance.ToString() );


    }
}
