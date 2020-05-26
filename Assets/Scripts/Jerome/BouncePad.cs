using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public int force = 25;

    void OnTriggerEnter2D(Collider2D who)
    {
        // Assuming all colliders for bounce zone are tagged
        if (who.tag == "Player")
        {
            ///////////// Rigidbody 2D
            Rigidbody2D player = who.gameObject.GetComponent<Rigidbody2D>();
            player.velocity = new Vector2(0f, force);
            //player.AddForce(new Vector2(0f, force));
        }
    }
}
