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

    bool makingDecision = false;


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

        
        if (shootScript.coolingDown)
        {
            if (!makingDecision)
            {
                MakeDecision(healthU, reloadU);
                makingDecision = true;
            }
        }
        else
        {
            makingDecision = false;
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
            StartCoroutine(healthScript.Heal());
            Debug.Log("Healing");
        }
        else if (reloadU > healthU)
        {
            StartCoroutine(shootScript.Reload());
            Debug.Log("relaoding");
           

        }
        else
        {
            int randomiser = Random.Range(-1, 1);
            if (randomiser == 0)
            {
                StartCoroutine(shootScript.Reload());
                Debug.Log("reloading");

            }
            else
            {
                StartCoroutine(healthScript.Heal());
                Debug.Log("Healing");

            }
        }
    }

    
   


}
