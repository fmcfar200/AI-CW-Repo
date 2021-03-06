﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class NPCMovementScript : MonoBehaviour {

    public GameObject player;

    public float moveSpeed = 5.0f;
    public bool hostile = true;
    public float distance;


    public Text distanceText;

    public bool takeCover = false;
    bool takingCover = false;
    bool coverFound;

    Vector3 foundCover;
    Transform cover;


    // Use this for initialization
    void Start ()
    {
        
        hostile = false;

        //finds player
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("PLAYER NOT FOUND!");
        }


	}
	
	// Update is called once per frame
	void Update () {
        //displays distance
        distance = Vector3.Distance(player.transform.position, transform.position);
        distance = Mathf.RoundToInt(distance);
        distanceText.text = "Distance: " + distance.ToString("F2");

        if (distance < 20.0f && !hostile)
        {
            hostile = true;
        }
        
	
	}

    void FixedUpdate()
    {
        //finds cover 
        if (takeCover)
        {
            if (!takingCover)
            {
                if (!coverFound)
                {
                    foundCover = FindCover();
                    coverFound = true;
                }
                else
                {
                    Vector3 desiredVelocity = foundCover - transform.position;
                    desiredVelocity.y = 0;

                    Vector3 movementVector = desiredVelocity.normalized * moveSpeed * Time.deltaTime;
                    transform.position += movementVector;
                    // transform.position = Vector3.MoveTowards(transform.position, foundCover,
                    //        moveSpeed * Time.deltaTime);
                }

                if (transform.position == foundCover)
                {
                    takingCover = true;
                    takeCover = false;
                    coverFound = false;
                    
                }
            }

        }
      
    }

    //seeks and flees depending on distance to player
    public void SeekAndFlee()
    {
        takingCover = false;
        takeCover = false;
        coverFound = false;
        if (distance > 20.0f)
        {
            Seek();
        }
        else
        {
            Flee();
        }
       
    }

    //seek code
   void Seek()
   {
        Vector3 desiredVelocity = player.transform.position - transform.position;
        desiredVelocity.y = 0;

        Vector3 movementVector = desiredVelocity.normalized * moveSpeed * Time.deltaTime;
        transform.position += movementVector;
        
    }
    //flee code
    void Flee()
    {
        Vector3 desiredVelocity = player.transform.position - transform.position;
        desiredVelocity = -desiredVelocity;
        desiredVelocity.y = 0;
        Vector3 movementVector = desiredVelocity.normalized * moveSpeed * Time.deltaTime;
        transform.position += movementVector;
        
    }

    //method for finding appropriate cover
    public Vector3 FindCover()
    {
        
            GameObject chosenCover;
            GameObject[] coverObjects = GameObject.FindGameObjectsWithTag("Cover");

            if (coverObjects != null)
            {
                int coverIndex = Random.Range(0, coverObjects.Length);
                chosenCover = coverObjects[coverIndex];

                if (chosenCover != null)
                {
                    Transform coverSpot1 = chosenCover.transform.GetChild(0);
                    Transform coverSpot2 = chosenCover.transform.GetChild(1);

                    float c1DistanceToPlayer = Vector3.Distance(coverSpot1.transform.position, player.transform.position);
                    float c2DistanceToPlayer = Vector3.Distance(coverSpot2.transform.position, player.transform.position);

                    if (c1DistanceToPlayer > c2DistanceToPlayer)
                    {
                        cover = coverSpot1;
                    }
                    else
                    {
                        cover = coverSpot2;
                    }

                }
            }
            else
            {
                Debug.Log("No Cover Found");
                
            }

        return cover.position;
        
    }

    //stays in cover and leaves after a certain amount of time
    IEnumerator StayAndLeave()
    {
        yield return new WaitForSeconds(5.0f);
        takingCover = false;
    }


   
}
