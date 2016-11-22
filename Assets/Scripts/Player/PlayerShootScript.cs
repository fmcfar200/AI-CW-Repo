using UnityEngine;
using System.Collections;

public class PlayerShootScript : MonoBehaviour {


    public Texture2D crossHair;
    public GameObject bloodGush;
    RaycastHit hit;

    GameObject firedBullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ViewportPointToRay( new Vector3(0.5f, 0.5f, 0f));
            Physics.Raycast(ray, out hit);
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Health>().TakeDamage(15);
                hit.collider.GetComponent<NPCMovementScript>().hostile = true;

                Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                StartCoroutine(BloodEffect(hitPoint, hit.collider.transform));
            }
         
           
        }
	
	}
    

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width/2 - crossHair.width/4 ,Screen.height/2 - crossHair.height/4,crossHair.width/2,crossHair.height/2), crossHair);
    }

    IEnumerator BloodEffect(Vector3 hitPoint, Transform coll)
    {
        GameObject blood = Instantiate(bloodGush, hitPoint, Quaternion.identity) as GameObject;
        blood.transform.SetParent(coll);
        yield return new WaitForSeconds(1.0f);
        Destroy(blood);
    }
}
