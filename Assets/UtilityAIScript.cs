using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UtilityAIScript : MonoBehaviour {

    //Objects and Scripts
    GameObject enemyObj;
    NPCMovementScript movementScript;
    ShootScript shootScript;
    Health healthScript;

    //variable data
    float distance;
    int currentClip;
    int currentHealth;

    //utilities
    public float anxietyU;
    public float reloadU;
    public float healthU;


    //UI elements
    public Text anxietyTextObj;
    public Text reloadTextObj;
    public Text healthTextObj;

	// Use this for initialization
	void Start () {
        enemyObj = GameObject.Find("Enemy");
        if (enemyObj != null)
        {
            movementScript = enemyObj.GetComponent<NPCMovementScript>();
            shootScript = enemyObj.GetComponent<ShootScript>();
            healthScript = enemyObj.GetComponent<Health>();
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        distance = movementScript.distance;
        currentClip = shootScript.currentClip;
        currentHealth = healthScript.health;

        CalculateAnxiety(distance);
        CalculateReload(currentClip);
        CalculateHealth(currentHealth);

    }

    void CalculateAnxiety(float distance)
    {
        anxietyU = (1 / (1 + Mathf.Pow(distance, 2.7f*0.45f))) * 10;
        anxietyU = Mathf.Clamp(anxietyU, 0.0f, 1.0f);
        anxietyTextObj.text = "Anxiety: " + anxietyU;
        //Debug.Log("Anxiety: " + anxiety.ToString()+ " at Distance " + distance.ToString() );

    }
    void CalculateReload(int currentClip)
    {
        reloadU = (1 / (1 + Mathf.Pow(currentClip, 4.0f * 0.45f))) * 10;
        reloadU = Mathf.Clamp(reloadU, 0.0f, 1.0f);
        reloadTextObj.text = "Reload: " + reloadU;
    }
    void CalculateHealth(int currenHealth)
    {
        healthU = (1 / (1 + Mathf.Pow(currentHealth, 1.5f * 0.45f))) * 10;
        healthU = Mathf.Clamp(healthU, 0.0f, 1.0f);
        healthTextObj.text = "Health: " + healthU;
        Debug.Log("Health Utility: " + healthU.ToString()+ " at Health Value " + currentHealth.ToString() );

    }
}
