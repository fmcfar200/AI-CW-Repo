using UnityEngine;
using System.Collections;

public class PlayerShootScript : MonoBehaviour {

    //crosshair texture
    public Texture2D crossHair;
    //particle system
    public GameObject bloodGush;
    //raycast hit
    RaycastHit hit;
    //audio
    public AudioClip shootClip;
    //fired bullet game object
    GameObject firedBullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //code for player shooting
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<AudioSource>().PlayOneShot(shootClip);
            Ray ray = Camera.main.ViewportPointToRay( new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<Health>().TakeDamage(15);
                    hit.collider.GetComponent<NPCMovementScript>().hostile = true;

                    Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    StartCoroutine(BloodEffect(hitPoint, hit.collider.transform));
                }
                else
                {
                    Debug.Log("Hit " + hit.collider.name.ToString());
                }
               
            }
            else
            {
                Debug.Log("Miss");
            }


        }
	
	}
    
    //draws reticle
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width/2 - crossHair.width/4 ,Screen.height/2 - crossHair.height/4,crossHair.width/2,crossHair.height/2), crossHair);
    }
    //blood effect on hit object
    IEnumerator BloodEffect(Vector3 hitPoint, Transform coll)
    {
        GameObject blood = Instantiate(bloodGush, hitPoint, Quaternion.identity) as GameObject;
        blood.transform.SetParent(coll);
        yield return new WaitForSeconds(1.0f);
        Destroy(blood);
    }
}
