using UnityEngine;
using System.Collections;

public class PlayerShootScript : MonoBehaviour {


    public Texture2D crossHair;
    public GameObject bulletSpawn;
    public GameObject bulletPrefab;
    float bulletForce =100.0f;

    GameObject firedBullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            firedBullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity) as GameObject;
            firedBullet.name = "Bullet";
        }
	
	}
    void FixedUpdate()
    {
        firedBullet.GetComponent<BulletScript>().ApplyForce(this.gameObject, bulletForce);

    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width/2 - crossHair.width/4 ,Screen.height/2 - crossHair.height/4,crossHair.width/2,crossHair.height/2), crossHair);
    }
}
