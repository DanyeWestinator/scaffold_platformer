using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    public GameObject bullet;
    public float timeBetweenShots;
    public float playerBulletSpeed;
    public int rightShoot;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = 1.2f;
        playerBulletSpeed = 3.6f;
        rightShoot = 1;
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
    }

    IEnumerator Shoot(float time)
    {
        GameObject toShoot = Instantiate(bullet, (gameObject.transform.position + new Vector3(0.70f * rightShoot,0,0)), Quaternion.identity);
        float speed = bullet.GetComponent<bullet>().getSpeed();
        toShoot.GetComponent<Rigidbody2D>().velocity = Vector2.right * rightShoot * speed * playerBulletSpeed * gameObject.transform.localScale.x;
        
        Vector3 newScale = toShoot.transform.localScale;
        newScale.x *= gameObject.transform.localScale.x;
        toShoot.transform.localScale = newScale;
        toShoot.transform.eulerAngles = new Vector3(0f, 0f, -1 * rightShoot * 90 * gameObject.transform.localScale.x);
        toShoot.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        
        toShoot.tag = "playerGunShot";
        toShoot.AddComponent<BoxCollider2D>();
        toShoot.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(time);
        canShoot = true;
        
    }
}
