using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))] [RequireComponent(typeof(SphereCollider))]


public class BulletScript : MonoBehaviour {

    Rigidbody rb;
    GameObject enemy;
    int bulletDamage = 20;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
        enemy = GameObject.Find("Enemy");

        StartCoroutine(LifeTime());
        
    }

    void FixedUpdate()
    {
       
    }

    /*
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
    */

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }

    public void ApplyForce(GameObject spawn, float force)
    {
        rb.AddForce(spawn.transform.forward * force);
    }
	
}
