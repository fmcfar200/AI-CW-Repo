using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UtilityAIScript : MonoBehaviour {

    GameObject enemyObj;
    NPCMovementScript movementScript;
    ShootScript shootScript;

    float distance;
    int currentClip;
    public float anxiety;
    public float reload;



    public Text anxietyTextObj;
    public Text reloadTextObj;

	// Use this for initialization
	void Start () {
        enemyObj = GameObject.Find("Enemy");
        if (enemyObj != null)
        {
            movementScript = enemyObj.GetComponent<NPCMovementScript>();
            shootScript = enemyObj.GetComponent<ShootScript>();
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        distance = movementScript.distance;
        currentClip = shootScript.currentClip;

        CalculateAnxiety(distance);
        CalculateReload(currentClip);

    }

    void CalculateAnxiety(float distance)
    {
        anxiety = (1 / (1 + Mathf.Pow(distance, 2.7f*0.45f))) * 10;
        anxiety = Mathf.Clamp(anxiety, 0.0f, 1.0f);
        anxietyTextObj.text = "Anxiety: " + anxiety;
        //Debug.Log("Anxiety: " + anxiety.ToString()+ " at Distance " + distance.ToString() );

    }
    void CalculateReload(int currentClip)
    {
        reload = (1 / (1 + Mathf.Pow(currentClip, 4.0f * 0.45f))) * 10;
        reload = Mathf.Clamp(reload, 0.0f, 1.0f);
        reloadTextObj.text = "Reload: " + reload;
    }
}
