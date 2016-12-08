using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    // health and kits
    public int health;
    int maxHealth = 100;
    int healthKits = 3;
    //ui vars
    public Text healthText;
    public Text healthKitText;

    //healing bool
    public bool healing = false;

    //audio
    public AudioClip healthSound;



	// Use this for initialization
	void Start () {
        //health is set to max
        health = maxHealth;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //displays health
        healthText.text = "Health: " + health.ToString();
        healthKitText.text = "Health Kits: " + healthKits.ToString();

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        
	}
    //take damage method
    public void TakeDamage(int amount)
    {
        health -= amount;
    }
    //heal coroutine
    public IEnumerator Heal()
    {
        if (!healing)
        {
            if (healthKits > 0)
            {
                health = maxHealth;
                healthKits--;

                GetComponent<AudioSource>().PlayOneShot(healthSound);
                yield return new WaitForSeconds(healthSound.length);
                healing = true;
            }
        }
        healing = false;
    }
}
