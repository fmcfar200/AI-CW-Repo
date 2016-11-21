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


    public float cooldownTime;
    public bool coolingDown;

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

        CalculateUtilities();

        if (coolingDown == true)
        {
            MakeDecision(healthU,reloadU);
            coolingDown = false;
            
        }

    }

    void CalculateAnxiety(float distance)
    {
        anxietyU = (1 / (1 + Mathf.Pow(distance, 2.7f*0.45f))) * 10;
        anxietyU = Mathf.Clamp(anxietyU, 0.0f, 1.0f);
        anxietyTextObj.text = "Anxiety: " + anxietyU;

    }
    void CalculateReload(int currentClip)
    {
        reloadU = (1 / (1 + Mathf.Pow(currentClip, 4.0f * 0.45f))) * 10;
        reloadU = Mathf.Clamp(reloadU, 0.0f, 1.0f);
        reloadTextObj.text = "Reload: " + reloadU;
    }
    void CalculateHealth(int currenHealth)
    {
        healthU = (1 / (1 + Mathf.Pow(currentHealth, 1.5f * 0.65f))) * 10;
        healthU = Mathf.Clamp(healthU, 0.0f, 1.0f);
        healthTextObj.text = "Health: " + healthU;

    }

    void CalculateUtilities()
    {
        CalculateAnxiety(distance);
        CalculateReload(currentClip);
        CalculateHealth(currentHealth);
    }

    void MakeDecision(float healthU, float reloadU)
    {
        if (healthU > reloadU )
        {
            healthScript.Heal();
            Debug.Log("Healing");
            coolingDown = false;
        }
        else if (reloadU > healthU)
        {
            shootScript.Reload();
            Debug.Log("Reloading");
            coolingDown = false;


        }
        else
        {
            int randomiser = Random.Range(-1, 1);
            if (randomiser == 0)
            {
                shootScript.Reload();
                Debug.Log("reloading");
                coolingDown = false;


            }
            else
            {
                healthScript.Heal();
                Debug.Log("Healing");
                coolingDown = false;


            }
        }
    }

   


}
