using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour {

    NPCMovementScript npcMovementScript;
    UtilityAIScript utilityScript;
    Health healthScript;

    public int currentClip;
    public int currentAmmo;

    int maxClip = 16;
    int maxAmmo = 4;
    float fireRate = 0.5f;
    float nextFire = 0.0f;
    float bulletForce = 100.0f;
    bool reloading = false;
    public AudioClip reloadSound;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    GameObject firedBullet;

    public Text ammoText;

    float cooldownTime = 5.0f;
    public bool coolingDown = false;

    float attackTimer = 5.0f;

    public AudioClip shotSound;


	// Use this for initialization
	void Start ()
    {
        npcMovementScript = this.gameObject.GetComponent<NPCMovementScript>();
        utilityScript = this.gameObject.GetComponent<UtilityAIScript>();
        healthScript = this.gameObject.GetComponent<Health>();

        currentClip = maxClip;
        currentAmmo = maxAmmo;

        

        //TEMP
        GetComponent<Rigidbody>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (npcMovementScript.hostile != false)
        {
            if (!coolingDown)
            {
                if (attackTimer > 0 && !reloading && !healthScript.healing)
                {
                    Attack();

                }
                else
                {
                    coolingDown = true;
                    attackTimer = 5.0f;
                }

            }
            else
            {
                cooldownTime -= Time.deltaTime;
                if (cooldownTime <=0)
                {
                    coolingDown = false;
                    cooldownTime = 5.0f;
                }
            }
        }

        ammoText.text = "Enemy Ammo Count: " + currentClip.ToString() + "/" + currentAmmo.ToString();
        
	
	}

    void FixedUpdate()
    {
        

        if (firedBullet != null)
        {
            firedBullet.GetComponent<BulletScript>().ApplyForce(this.gameObject, bulletForce);
        }
    }

    void AimAtPlayer()
    {
        gameObject.transform.LookAt(npcMovementScript.player.transform.position);
    }

    void Shoot()
    {
        GetComponent<AudioSource>().PlayOneShot(shotSound);
        firedBullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity) as GameObject;
        firedBullet.name = "Bullet";
        currentClip--;
    }

    void Attack()
    {
        attackTimer -= Time.deltaTime;
        npcMovementScript.SeekAndFlee();

        AimAtPlayer();
        
        if (Time.time > nextFire && currentClip > 0)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
          
    }

    public IEnumerator Reload()
    {
        if (!reloading)
        {
            if (currentAmmo > 0)
            {
                GetComponent<AudioSource>().PlayOneShot(reloadSound);

                if (currentClip < maxClip && maxAmmo > 0)
                {
                    currentClip = maxClip;
                    currentAmmo--;
                }
            }
            yield return new WaitForSeconds(reloadSound.length);

            reloading = true;
        }
        reloading = false;
    }


    public void ResetAttackTimer()
    {
        attackTimer = 0;
    }

   
}
