using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float speed = 3f;
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(collision.GetComponent<PlayerHealth>().BlinkSprite());
            Destroy(this.gameObject);
        }
        
        //don't let bullet go through walls
        if(collision.tag == "WallOrGround")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //again don't let bullet go through walls
        if(collision.tag == "WallOrGround")
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        //again don't let bullet go through walls
        if(collision.tag == "WallOrGround")
        {
            Destroy(this.gameObject);
        }
    }


    public float getSpeed (){ return speed; }
    public void setSpeed (float newSpeed) { speed = newSpeed; }
    
    //public float getDamage (){ return damage; }
    //public void setDamage (float newDamage) { damage = newDamage; }
    
}
