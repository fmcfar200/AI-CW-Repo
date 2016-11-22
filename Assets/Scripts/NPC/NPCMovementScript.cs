using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class NPCMovementScript : MonoBehaviour {

    UtilityAIScript utilityAI;
    public GameObject player;

    public float moveSpeed = 5.0f;
    public bool hostile = true;
    public float distance;


    public Text distanceText;


    // Use this for initialization
    void Start ()
    {
        hostile = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("PLAYER NOT FOUND!");
        }

        utilityAI = GetComponent<UtilityAIScript>();
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(player.transform.position, transform.position);
        distance = Mathf.RoundToInt(distance);

        //CODE MAY BE MOVED TO UTILITYSCRIPT
        if (utilityAI.anxietyU >= 1.0f)
        {
            hostile = true;
        }
	
	}

    void FixedUpdate()
    {
    }

    
    public void MoveTowardsAndAwayFromPlayer()
    {
        float step = moveSpeed * Time.deltaTime;

        if (distance > 15f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);


        }
        /*
        if (distance < 15f)
        {
            Vector3 direction = transform.position - player.transform.position;

            transform.Translate(direction * step * Time.deltaTime);

            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -step);
        }
        */
       
       
    }


   
}
