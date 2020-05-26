using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBounds : MonoBehaviour
{
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position.x > rightBound)
          transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
       
       if(transform.position.x < leftBound)
          transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);

       if(transform.position.y > topBound)
          transform.position = new Vector3(transform.position.x, topBound, transform.position.z);

       if(transform.position.y < bottomBound)
          transform.position = new Vector3(transform.position.x, bottomBound, transform.position.z);
       
       //stop pathfinding when child enemy is not active
       if(!transform.GetChild(0).gameObject.active)
          transform.gameObject.SetActive(false);
    }
}
