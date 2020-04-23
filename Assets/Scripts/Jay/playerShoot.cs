using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    public GameObject bullet;
    
    public float timeBetweenShots;
    public float[] listOfCooldowns;
    
    public float playerBulletSpeed;
    public float[] listOfBulletSpeeds;
    
    public float bulletSizeXMod;
    public float[] listOfXMods;
    
    public float bulletSizeYMod;
    public float[] listOfYMods;
    
    public int rightShoot;
    public int currentGun;
    public const int numOfGuns = 5;
    public Transform gun1, gun2, gun3, gun4, gun5;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        //"normal" bullet speed and cooldown
        //timeBetweenShots = 1.2f;
        //playerBulletSpeed = 3.4f;
        rightShoot = 1;
        currentGun = 1;
        
        listOfCooldowns = new float[numOfGuns]{1.2f, 1.8f, 1.5f, 2.5f, 0.8f};
        listOfBulletSpeeds = new float[numOfGuns]{3.4f, 2.9f, 2.4f, 5.6f, 4.4f};
        listOfXMods = new float[numOfGuns]{0.0f, 0.0f, -0.2f, 2.0f, -0.5f};
        listOfYMods = new float[numOfGuns]{0.0f, 1.0f, -0.2f, 0.0f, -0.5f};
        
        gun1 = transform.Find("Gun");
        gun2 = transform.Find("Gun2");
        gun3 = transform.Find("Gun3");
        gun4 = transform.Find("Gun4");
        gun5 = transform.Find("Gun5");
        
        gun1.gameObject.SetActive(true);
        gun2.gameObject.SetActive(false);
        gun3.gameObject.SetActive(false);
        gun4.gameObject.SetActive(false);
        gun5.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = new Vector3(gameObject.transform.localScale.x * -1, 0f, 0f);
        //RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, 10f);//, RaycastHit2D[2] results
        
        //if (hit.collider && hit.collider.tag == "Player" && canShoot == true)
        //{
        //    Debug.DrawLine(gameObject.transform.position, hit.collider.transform.position, Color.red, 2f);
        //    canShoot = false;
        //    StartCoroutine(Shoot(timeBetweenShots));
        //}
        
        if(Input.GetAxisRaw("Horizontal") == 1)
        {
           rightShoot = 1;
        }
        
        if(Input.GetAxisRaw("Horizontal") == -1)
        {
           rightShoot = -1;
        }
        
        if(Input.GetKeyDown(KeyCode.S) && canShoot == true)
        {
           canShoot = false;
           StartCoroutine(Shoot(timeBetweenShots));
        }
        
        if(Input.GetKeyDown(KeyCode.W))
        {
           gunSwitch();
        }
    }
    
    void gunSwitch()
    {
       switch(currentGun)
       {
          case 1:
            gun1.gameObject.SetActive(false);
            gun2.gameObject.SetActive(true);
            currentGun = 2;
            break;
          case 2:
            gun2.gameObject.SetActive(false);
            gun3.gameObject.SetActive(true);
            currentGun = 3;
            break;
          case 3:
            gun3.gameObject.SetActive(false);
            gun4.gameObject.SetActive(true);
            currentGun = 4;
            break;
          case 4:
            gun4.gameObject.SetActive(false);
            gun5.gameObject.SetActive(true);
            currentGun = 5;
            break;
          default:
            gun5.gameObject.SetActive(false);
            gun1.gameObject.SetActive(true);
            currentGun = 1;
            break;
       }
       timeBetweenShots = listOfCooldowns[currentGun - 1];
       playerBulletSpeed = listOfBulletSpeeds[currentGun - 1];
       bulletSizeXMod = listOfXMods[currentGun - 1];
       bulletSizeYMod = listOfYMods[currentGun - 1];
    }

    IEnumerator Shoot(float time)
    {
        GameObject toShoot = Instantiate(bullet, (gameObject.transform.position + new Vector3(0.70f * rightShoot, 0.05f * rightShoot * (1.0f + bulletSizeYMod),0)), Quaternion.identity);
        float speed = bullet.GetComponent<bullet>().getSpeed();
        toShoot.GetComponent<Rigidbody2D>().velocity = Vector2.right * rightShoot * speed * playerBulletSpeed * gameObject.transform.localScale.x;
        
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
        //toShoot.AddComponent<BoxCollider2D>();
        //toShoot.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(time);
        canShoot = true;
        
    }
}
