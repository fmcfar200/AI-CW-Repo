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
    public float attackU;
    public float coverU;
    public float reloadU;
    public float healthU;


    //UI elements
    public Text attackTextObj;
    public Text coverTextObj;
    public Text reloadTextObj;
    public Text healthTextObj;

    //decision making bool
    bool makingDecision = false;


    // Use this for initialization
    void Start () {

        //finds enemy object
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
        //all variables used to calculate utilities
        distance = movementScript.distance;
        currentClip = shootScript.currentClip;
        currentHealth = healthScript.health;

        //method for calculating utilities is called
        CalculateUtilities();
        
        //if cooling down then make decision
        if (shootScript.coolingDown)
        {
            if (!makingDecision)
            {
                MakeDecisionRelative(healthU, reloadU, coverU,attackU);
                makingDecision = true;
            }
        }
        else
        {
            makingDecision = false;
        }
    }

    

    //calculates reload utility 
    void CalculateReload(int currentClip)
    { 
       reloadU = (1 / (1 + Mathf.Pow(currentClip, 4.0f * 0.45f)))*10;
       reloadU = Mathf.Clamp(reloadU, 0.0f, 1.0f);
       reloadTextObj.text = "Reload: " + reloadU.ToString("F1");

    }

    //calculate health utility 
    void CalculateHealth(int currenHealth)
    {
        healthU = (1 / (1 + Mathf.Pow(currentHealth, 1.5f ))) *100;
        healthU = Mathf.Clamp(healthU, 0.0f, 1.0f);
        healthTextObj.text = "Health: " + healthU.ToString("F1");

    }

    //calculate cover utility
    void CalculateCover(float distance, float healthU, float reloadU )
    {
        coverU = (1 / (1 + Mathf.Pow(distance*healthU+reloadU, 2.7f )));
        coverU = Mathf.Clamp(coverU, 0.0f, 1.0f);
        coverTextObj.text = "TakeCover: " + coverU.ToString("F1");

    }

   
    //clauclates attack Utility
    void CalculateAttack(float reload, float heal)
    {
        attackU = (1 / (1 + Mathf.Pow(reload + heal, 2.7f)));
        Debug.Log(attackU.ToString());
        reloadU = Mathf.Clamp(reloadU, 0.0f, 1.0f);
        attackTextObj.text = "Attack: " + attackU.ToString("F1");

    }

    //calculates all utilities
    void CalculateUtilities()
    {
        CalculateReload(currentClip);
        CalculateHealth(currentHealth);
        CalculateCover(distance, healthU, reloadU);
        CalculateAttack(reloadU, healthU);
    }

    //method for Absolute Utility
    void MakeDecisionAbsolute(float healthU,float reloadU, float anxietyU )
    {
        //stores all utilities in a temp variable
        float tempHealthU = healthU;
        float tempReloadU = reloadU;
        float tempAnxietyU = anxietyU;
        
        //array of utilties
        float[] utilities = {healthU,reloadU,anxietyU};
        //array is sorted
        Array.Sort(utilities);

        //if statement to decide what action to take 
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

    //method for Relative Utility
    void MakeDecisionRelative(float healthU, float reloadU, float anxietyU, float attackU)
    {
        //list for storing utilities
        List<float> utilities = new List<float>();
        //converted utilities to ints 
        float uHealToInt = healthU * 10;
        float uReloadToInt = reloadU * 10;
        float uAnxietyToInt = anxietyU * 10;
        float uAttackToInt = attackU * 10;

        /*
        adds all the utilities to the list. The larger the utility
        the more often it will appear in the list.
        */
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

        for (int i = 0; i < uAttackToInt; i++)
        {
            utilities.Add(attackU);
        }


        for (int i = 0; i < utilities.Count;i++)
        {
            Debug.Log(utilities[i]);
        }

        //random index is found
        int randomIndex = UnityEngine.Random.Range(0, utilities.Count);

        //if the random value in the list is equal to any of the utilies then it fires that action
        if (utilities[randomIndex] == healthU && healthScript.health != 100)
        {
            Debug.Log("healing");

            StartCoroutine(healthScript.Heal());
        }
        else if (utilities[randomIndex] == reloadU && shootScript.currentAmmo != 0)
        {
            Debug.Log("reloading");

            StartCoroutine(shootScript.Reload());

        }
        else if (utilities[randomIndex] == anxietyU)
        {
            Debug.Log("cover");

            movementScript.takeCover = true;
        }
        else if (utilities[randomIndex] == attackU)
        {
            Debug.Log("cover");
            shootScript.attack = true;
        }

        //clears the list
        utilities.Clear();
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
   




}
