using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunFlip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") == 1)
        {
           transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
           transform.localPosition = new Vector3(0.45f, -0.05f, -0.1f);
        }
        
        if(Input.GetAxisRaw("Horizontal") == -1)
        {
           transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
           transform.localPosition = new Vector3(-0.45f, -0.05f, -0.1f);
        }
    }
}
