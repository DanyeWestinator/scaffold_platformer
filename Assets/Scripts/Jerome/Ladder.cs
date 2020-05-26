using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public int speed = 6;
    public float gravityOffset = 1.18f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);

        }
        else if (other.tag == "Player" && Input.GetKey(KeyCode.S))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        }
        else if(other.tag == "Player" && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            // To offset gravity. Not dynamic so it will be off if player gravity is changed.
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gravityOffset);
        }
    }
}
