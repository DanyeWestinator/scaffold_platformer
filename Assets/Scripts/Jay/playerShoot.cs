using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    public GameObject bullet;
    
    public float timeBetweenShots;
    public float playerBulletSpeed;
    public float bulletSizeXMod;
    public float bulletSizeYMod;
    public float bulletDamage;
    
    public int rightShoot;
    public int currentGun;
    
    public List<GameObject> gunList;
    public List<List<float>> gunStats;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        rightShoot = 1;
        currentGun = 1;
        
        gunList = new List<GameObject>();
        
        foreach(Transform child in transform)
        {
           gunList.Add(child.gameObject);
        }
        
        foreach(GameObject gun in gunList)
        {
           gun.SetActive(false);
        }
        //default weapon is Gun1
        gunList[0].SetActive(true);
        
        gunStats = new List<List<float>>();
        gunStats.Add(new List<float>{0.6f, 0.8f, 0.4f, 1.2f, 0.1f}); //list of cooldown times for Gun1, Gun2, Gun3, ...
        gunStats.Add(new List<float>{3.9f, 3.4f, 2.9f, 5.8f, 4.7f}); //list of bullet speeds " "
        gunStats.Add(new List<float>{0.0f, 0.0f, -0.2f, 2.0f, -0.5f}); //list of X (scale) mods " "
        gunStats.Add(new List<float>{0.0f, 1.0f, -0.2f, 0.0f, -0.5f}); //list of Y (scale) mods " "
        gunStats.Add(new List<float>{5.0f, 6.8f, 3.6f, 12.6f, 1.0f}); //list of bullet damage values " "
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetAxisRaw("Horizontal") == 1)
        {
           rightShoot = 1;
        }
        
        if(Input.GetAxisRaw("Horizontal") == -1)
        {
           rightShoot = -1;
        }
        
        if(Input.GetButton("Shoot") && canShoot == true)
        {
           canShoot = false;
           StartCoroutine(Shoot(timeBetweenShots));
        }
        
        if(Input.GetButtonDown("Switch"))
        {
           gunSwitch();
        }
    }
    
    void gunSwitch()
    {
       gunList[currentGun - 1].SetActive(false);
       
       currentGun++;
       if(currentGun > gunList.Count)
          currentGun = 1;
       
       gunList[currentGun - 1].SetActive(true);
       
       timeBetweenShots = gunStats[0][currentGun - 1];
       playerBulletSpeed = gunStats[1][currentGun - 1];
       bulletSizeXMod = gunStats[2][currentGun - 1];
       bulletSizeYMod = gunStats[3][currentGun - 1];
       bulletDamage = gunStats[4][currentGun - 1];
    }

    IEnumerator Shoot(float time)
    {
        GameObject toShoot = Instantiate(bullet, (gameObject.transform.position + new Vector3(0.70f * rightShoot, 0.05f * rightShoot * (1.0f + bulletSizeYMod),0)), Quaternion.identity);
        float speed = bullet.GetComponent<bullet>().getSpeed();
        toShoot.GetComponent<Rigidbody2D>().velocity = Vector2.right * rightShoot * speed * playerBulletSpeed * gameObject.transform.localScale.x;
        
        //set bullet damage
        toShoot.GetComponent<bullet>().damage = bulletDamage;
        
        Vector3 newScale = toShoot.transform.localScale;
        newScale.x *= gameObject.transform.localScale.x;
        toShoot.transform.localScale = newScale;
        
        //larger(+)/smaller(-) bullets
        //since bullets are rotated + or - 90 degrees, increasing x local scale gives taller bullets
        //and increasing y local scale gives wider bullets (must be swapped)
        toShoot.transform.localScale += new Vector3(bulletSizeYMod, bulletSizeXMod, 0.0f);
        
        toShoot.transform.eulerAngles = new Vector3(0f, 0f, -1 * rightShoot * 90 * gameObject.transform.localScale.x);
        toShoot.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        
        toShoot.tag = "playerGunShot";
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
