using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    private GameObject player;

    private float distance = Mathf.Infinity;
    public float range = 3f;
    private bool canShoot = true;
    public GameObject bulletPrefab;
    public Transform barrel;
    public GameObject turretGun;

    public float cooldown = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(turretGun.transform.position, player.transform.position);
        Vector3 difference = player.transform.position - turretGun.transform.position;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotationZ -= 90f;

        if (distance <= range + 3f)
        {
            turretGun.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
        else
        {
            turretGun.transform.rotation = Quaternion.identity;
        }

        if (distance <= range && canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
            
        }
        if (turretGun.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel);
        bullet.transform.parent = null;
        bullet.transform.localScale = bulletPrefab.transform.localScale;
        bullet.transform.rotation = turretGun.transform.rotation;
        float speed = bullet.GetComponent<bullet>().getSpeed();
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * 0.01f * speed);
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
