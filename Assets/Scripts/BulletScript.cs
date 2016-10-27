using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))] [RequireComponent(typeof(SphereCollider))]


public class BulletScript : MonoBehaviour {

    Rigidbody rb;
    float force = 100.0f;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.right*force);
    }

	
}
