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

    bool takingCover = false;


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
       
        
	
	}

    void FixedUpdate()
    {
        
        
           //FindCover();

        
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

    void FindCover()
    {
        
            GameObject chosenCover;
            GameObject[] coverObjects = GameObject.FindGameObjectsWithTag("Cover");

            if (coverObjects != null)
            {
                int coverIndex = Random.Range(0, coverObjects.Length);
                Debug.Log("cover index: " + coverIndex.ToString());
                chosenCover = coverObjects[coverIndex];

                if (chosenCover != null)
                {
                    Transform coverSpot1 = chosenCover.transform.GetChild(0);
                    Transform coverSpot2 = chosenCover.transform.GetChild(1);

                    float c1DistanceToPlayer = Vector3.Distance(coverSpot1.transform.position, player.transform.position);
                    float c2DistanceToPlayer = Vector3.Distance(coverSpot2.transform.position, player.transform.position);

                    if (c1DistanceToPlayer > c2DistanceToPlayer)
                    {
                        Debug.Log(coverSpot1.name.ToString());
                        StartCoroutine(TakeCover(coverSpot1));

                    }
                    else
                    {
                        Debug.Log(coverSpot2.name.ToString());

                        StartCoroutine(TakeCover(coverSpot2));
                    }

                }
            }
            else
            {
                Debug.Log("No Cover Found");
            }


        
    }

    IEnumerator TakeCover(Transform cover)
    {
        if (!takingCover)
        {
            transform.position = Vector3.MoveTowards(transform.position, cover.position, moveSpeed * Time.deltaTime);
            if (transform.position == cover.position)
            {
                transform.position = cover.position;
            }
            takingCover = true;
            yield return new WaitForSeconds(3.0f);
        }
        takingCover = false;
    }


   
}
