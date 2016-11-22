using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {


    public int health;
    int maxHealth = 100;
    int healthKits = 3;

    public Text healthText;
    public Text healthKitText;

    public bool healing = false;

    public AudioClip healthSound;



	// Use this for initialization
	void Start () {

        health = maxHealth;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthText.text = "Health: " + health.ToString();
        healthKitText.text = "Health Kits: " + healthKits.ToString();

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        
	}

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public IEnumerator Heal()
    {
        GetComponent<AudioSource>().PlayOneShot(healthSound);
        if (!healing)
        {
            if (healthKits > 0)
            {
                health = maxHealth;
                healthKits--;
            }

            yield return new WaitForSeconds(healthSound.length);

            healing = true;
        }
        healing = false;
    }
}
