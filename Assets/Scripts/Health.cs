using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {


    public int health;
    public Text healthText;

	// Use this for initialization
	void Start () {

        health = 100;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthText.text = "Health: " + health.ToString();

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }


	
	}

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
