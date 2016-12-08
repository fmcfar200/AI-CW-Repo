using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour {

    //scripts that will be accessed
    NPCMovementScript npcMovementScript;
    Health healthScript;

    //ammo/clip variables
    public int currentClip;
    public int currentAmmo;
    //max ammo and clip
    public int maxClip = 16;
    int maxAmmo = 4;

    //fire rate 
    float fireRate = 0.5f;
    float nextFire = 0.0f;

    //force of bullet 
    float bulletForce = 100.0f;

    //reload bool
    bool reloading = false;

    //sounds
    public AudioClip reloadSound;
    public AudioClip shotSound;


    //game objects
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public GameObject emptyClip;
    GameObject firedBullet;
    GameObject gun;

    //UI
    public Text ammoText;

    //Cooldown
    float cooldownTime = 2.0f;
    public bool coolingDown = false;
    public bool attack = false;
    float attackTimer = 5.0f;



	// Use this for initialization
	void Start ()
    {
        npcMovementScript = this.gameObject.GetComponent<NPCMovementScript>();
        healthScript = this.gameObject.GetComponent<Health>();
        gun = GameObject.FindGameObjectWithTag("Gun");

        currentClip = maxClip;
        currentAmmo = maxAmmo;

        

        //TEMP
        GetComponent<Rigidbody>().isKinematic = true;

        coolingDown = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //code for attacking and cooling down when hostile
        if (npcMovementScript.hostile != false )
        {
            //if cooling down
            if (!coolingDown)
            {
                //if attacking is true and attack timer is active and not reloading and/or healing
                if (attack == true && attackTimer > 0 && !reloading && !healthScript.healing)
                {
                    //initiate attack
                    Attack();
                    
                }
                else
                {
                    //reset
                    attack = false;
                    coolingDown = true;
                    attackTimer = 5.0f;
                }

            }
            else
            {
                //cooldown
                cooldownTime -= Time.deltaTime;
                if (cooldownTime <=0)
                {
                    coolingDown = false;
                    cooldownTime = 2.0f;
                }
            }
        }
        //ammo ui elements
        ammoText.text = "Enemy Ammo Count: " + currentClip.ToString() + "/" + currentAmmo.ToString();
        

	
	}

    void FixedUpdate()
    {
        
        //applys force to fired bullet
        if (firedBullet != null)
        {
            firedBullet.GetComponent<BulletScript>().ApplyForce(this.gameObject, bulletForce);
        }
    }

    //aims of player
    void AimAtPlayer()
    {
        gameObject.transform.LookAt(npcMovementScript.player.transform.position);
    }

    //shooting code
    void Shoot()
    {
        GetComponent<AudioSource>().PlayOneShot(shotSound);
        firedBullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity) as GameObject;
        firedBullet.name = "Bullet";
        currentClip--;
    }
    //attacking code
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
    //reload coroutine -- 
    public IEnumerator Reload()
    {
        if (!reloading)
        {
            if (currentAmmo > 0)
            {
                GetComponent<AudioSource>().PlayOneShot(reloadSound);
                Instantiate(emptyClip, gun.transform.position, Quaternion.identity);
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

    //attack timer reset
    public void ResetAttackTimer()
    {
        attackTimer = 0;
    }

   
}
