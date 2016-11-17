using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class NPCMovementScript : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public bool hostile = true;
    public GameObject player;
    public float distance;

    UtilityAIScript utilityAI;

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
        distanceText.text = "Distance: " + distance.ToString();

        //CODE MAY BE MOVED TO UTILITYSCRIPT
        if (utilityAI.anxietyU >= 1.0f)
        {
            hostile = true;
        }
	
	}

    void FixedUpdate()
    {
       
    }

    /*
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            hostile = true;
        }
    }
    */
    public void MoveTowards()
    {

        float step = moveSpeed * Time.deltaTime;
        //TEMP CODE |||| REPLACE WITH STEERING BEHAVIOURS
        if (distance > 10.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position,step);
           
        }
       
       
    }

    /*
    public void FallBack()
    {
        float step = moveSpeed * Time.deltaTime;
        //TEMP CODE |||| REPLACE WITH STEERING BEHAVIOURS
        if (distance < 10.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -step);
            float yValue = transform.position.y;
            yValue = Mathf.Clamp(transform.position.y, 5.7f, 5.7f);

        }
    }
    */
}
