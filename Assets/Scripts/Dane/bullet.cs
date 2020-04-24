using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float speed = 3f;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getSpeed (){ return speed; }
    public void setSpeed (float newSpeed) { speed = newSpeed; }
    
    //public float getDamage (){ return damage; }
    //public void setDamage (float newDamage) { damage = newDamage; }
}
