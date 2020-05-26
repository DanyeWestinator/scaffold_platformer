using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyFlip : MonoBehaviour
{
    public AIPath aiPath;
    
    public bool hold_x_position;
    public bool hold_y_position;
    
    public float x_hold;
    public float y_hold;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
           transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
           transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        //hold y to fixed position
        if(hold_y_position)
           transform.position = new Vector3(transform.position.x, y_hold, -1f);
        
        //hold x to fixed position
        if(hold_x_position)
           transform.position = new Vector3(x_hold, transform.position.y, -1f);
    }
}
