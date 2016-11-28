using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
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
	void Update ()
    {
        distance = movementScript.distance;
        currentClip = shootScript.currentClip;
        currentHealth = healthScript.health;

        CalculateUtilities();
        
        if (shootScript.coolingDown)
        {
            if (!makingDecision)
            {
                MakeDecisionRelative2(healthU, reloadU, anxietyU);
                makingDecision = true;
            }
        }
        else
        {
            makingDecision = false;
        }
    }

    void CalculateReload(int currentClip)
    {
        if (shootScript.currentAmmo != 0)
        {
            reloadU = (1 / (1 + Mathf.Pow(currentClip, 4.0f * 0.45f))) * 10;
            reloadU = Mathf.Clamp(reloadU, 0.0f, 1.0f);
            reloadTextObj.text = "Reload: " + reloadU.ToString("F1");
        }
        else
        {
            reloadU = 0;
        }
    }

    void CalculateHealth(int currenHealth)
    {
        healthU = (1 / (1 + Mathf.Pow(currentHealth, 1.5f ))) *100;
        healthU = Mathf.Clamp(healthU, 0.0f, 1.0f);
        healthTextObj.text = "Health: " + healthU.ToString("F1");

    }

    void CalculateAnxiety(float distance,int currentHealth,int currentClip )
    {
        anxietyU = (1 / (1 + Mathf.Pow(currentHealth+distance, 2.7f )))*10000;
        anxietyU = Mathf.Clamp(anxietyU, 0.0f, 1.0f);
        anxietyTextObj.text = "Anxiety: " + anxietyU.ToString("F1");

    }

    void CalculateUtilities()
    {
        CalculateAnxiety(distance,currentHealth,currentClip);
        CalculateReload(currentClip);
        CalculateHealth(currentHealth);
    }

    void MakeDecisionAbsolute(float healthU,float reloadU, float anxietyU)
    {
        float tempHealthU = healthU;
        float tempReloadU = reloadU;
        float tempAnxietyU = anxietyU;

        float[] utilities = {healthU,reloadU,anxietyU};
        Array.Sort(utilities);


        if (utilities[utilities.Length-1] == tempHealthU)
        {
           StartCoroutine(healthScript.Heal());
        }
        else if (utilities[utilities.Length-1] == tempReloadU)
        {
            StartCoroutine(shootScript.Reload());

        }
        else if (utilities[utilities.Length-1] == tempAnxietyU)
        {
            movementScript.takeCover = true;
        }
         
    }

    void MakeDecisionRelative2(float healthU, float reloadU, float anxietyU)
    {
        List<float> utilities = new List<float>();
        float uHealToInt = healthU * 10;
        float uReloadToInt = reloadU * 10;
        float uAnxietyToInt = anxietyU * 10;

        for(int i = 0; i < uHealToInt;i++)
        {
            utilities.Add(healthU);
        }

        for (int i = 0; i < uReloadToInt; i++)
        {
            utilities.Add(reloadU);
        }

        for (int i = 0; i < uAnxietyToInt; i++)
        {
            utilities.Add(anxietyU);
        }

        for (int i = 0; i < utilities.Count;i++)
        {
            Debug.Log(utilities[i]);
        }

        int randomIndex = UnityEngine.Random.Range(0, utilities.Count);
        if (utilities[randomIndex] == healthU)
        {
            Debug.Log("healing");

            StartCoroutine(healthScript.Heal());
        }
        else if (utilities[randomIndex] == reloadU)
        {
            Debug.Log("reloading");

            StartCoroutine(shootScript.Reload());

        }
        else if (utilities[randomIndex] == anxietyU)
        {
            Debug.Log("cover");

            movementScript.takeCover = true;
        }
    }

    /*
    void MakeDecisionRelative(float healthU,float reloadU,float anxietyU)
    {
        float tempHealthU = healthU;
        float tempReloadU = reloadU;
        float tempAnxietyU = anxietyU;
        float[] utilities = { healthU, reloadU, anxietyU };


        //calculate total weight
        float totalWeight = 0;

        foreach (float utility in utilities)
        {
            totalWeight += utility;
        }

        //pick a random utility value
        float randomFloat = UnityEngine.Random.Range(0.1f, totalWeight);

        float counter = 0;
        float chosenUtility = 0;
        foreach (float utility in utilities)
        {
            counter += utility;
            if (counter >= randomFloat)
            {
                chosenUtility = utility;
            }
        }

        if (chosenUtility == tempHealthU)
        {
            Debug.Log("healing");

            StartCoroutine(healthScript.Heal());
        }
        else if (chosenUtility == tempReloadU)
        {
            Debug.Log("reloading");

            StartCoroutine(shootScript.Reload());

        }
        else if (chosenUtility == tempAnxietyU)
        {
            Debug.Log("cover");

            movementScript.takeCover = true;
        }



    }
    */
    /*
    void MakeDecision(float healthU, float reloadU, float anxiety)
    {
        //pref
        if (healthU > reloadU )
        {
            if (healthU > anxietyU)
            {
                StartCoroutine(healthScript.Heal());
                Debug.Log("Healing");
            }
            else
            {
                movementScript.takeCover = true;
            }
        }
        else if (reloadU > healthU)
        {
            if (reloadU > anxietyU)
            {
                StartCoroutine(shootScript.Reload());
                Debug.Log("relaoding");
            }
            else
            {
                movementScript.takeCover = true;
            }
        }
        else
        {
            int randomiser = UnityEngine.Random.Range(-1, 1);
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
    */




}
