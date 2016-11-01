using UnityEngine;
using System.Collections;

public class NPCMovementScript : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public bool hostile = true;
    public GameObject player;
    public float distance;


    // Use this for initialization
    void Start ()
    {
        hostile = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("PLAYER NOT FOUND!");
        }
	}
	
	// Update is called once per frame
	void Update () {

       
	
	}

    void FixedUpdate()
    {
        if (hostile)
        {
            //move towards player
            Move();

        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            hostile = true;
        }
    }

    void Move()
    {
        distance = Vector3.Distance(player.transform.position,transform.position);

        float step = moveSpeed * Time.deltaTime;
        //TEMP CODE |||| REPLACE WITH STEERING BEHAVIOURS
        if (distance > 10.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position,step);
           
        }
       
       
    }
}
