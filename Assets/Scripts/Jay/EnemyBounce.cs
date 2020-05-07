using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-////////////////////////////////////////////////////
///
/// HurtBox is the "weakness" area of an entity, if the player hits an enemy on such spot the enemy takes damage. This script is attached to an object
/// with a "HurtBox tag.
///

//changed from hurtbox to bounce only
public class EnemyBounce : MonoBehaviour 
{
    //public GameObject mainObject;

    //-////////////////////////////////////////////////////
    ///
    /// Gets call when a trigger collision happens on the game scene
    ///
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "BounceBox")
        {
           float x_velocity = transform.parent.GetComponent<Rigidbody2D>().velocity.x;
           transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(x_velocity, 12);
           ScoreScript.scoreValue += 1;
            //mainObject.SetActive(false); //Deactivate the mainObject scene object. We could destroy, but in order to still have access to such object 
                                         //so we can do things like reviving it, we deactivate it instead.
            //Destroy(collision.gameObject);
        }
    }
    
    //public void OnCollisionEnter2D(Collision2D other)
    //{
    //   //should deactivate enemy when hit by player's bullet
    //   if(GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("playerGunShot").GetComponent<Collider2D>()))
    //   {
    //      mainObject.SetActive(false);
    //   }
    //}
}
