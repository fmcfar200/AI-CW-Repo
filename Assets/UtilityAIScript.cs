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
        anxiety = (1 / (1 + Mathf.Pow(distance, 2.7f*0.45f))) * 10;
        anxiety = Mathf.Clamp(anxiety, 0.0f, 1.0f);
        anxietyTextObj.text = "Anxiety: " + anxiety;
        //Debug.Log("Anxiety: " + anxiety.ToString()+ " at Distance " + distance.ToString() );


    }
}
