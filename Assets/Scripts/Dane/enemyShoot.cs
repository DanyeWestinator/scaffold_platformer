using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public float timeBetweenShots = 1f;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(gameObject.transform.localScale.x * -1, 0f, 0f);
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, 10f);//, RaycastHit2D[2] results
        
        if (hit.collider && hit.collider.tag == "Player" && canShoot == true)
        {
            Debug.DrawLine(gameObject.transform.position, hit.collider.transform.position, Color.red, 2f);
            canShoot = false;
            StartCoroutine(Shoot(timeBetweenShots));
        }
        
    }

    IEnumerator Shoot(float time)
    {
        GameObject toShoot = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        float speed = bullet.GetComponent<bullet>().getSpeed();
        toShoot.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed * gameObject.transform.localScale.x;
        Vector3 newScale = toShoot.transform.localScale;
        newScale.x *= gameObject.transform.localScale.x;
        toShoot.transform.localScale = newScale;
        toShoot.transform.eulerAngles = new Vector3(0f, 0f, 90 * gameObject.transform.localScale.x);
        yield return new WaitForSeconds(time);
        canShoot = true;
        
    }
}
