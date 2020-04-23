using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunFlip : MonoBehaviour
{
    public float gunSizeScale;
    public float x_offset;
    public float y_offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if player is facing/shooting to the right, gun must also face right
        if(transform.parent.GetComponent<playerShoot>().rightShoot == 1)
        {
           transform.localScale = new Vector3(gunSizeScale, Mathf.Abs(gunSizeScale), Mathf.Abs(gunSizeScale));
           transform.localPosition = new Vector3(0.45f + x_offset, -0.05f + y_offset, -0.1f);
        }
        //same for left
        if(transform.parent.GetComponent<playerShoot>().rightShoot == -1)
        {
           transform.localScale = new Vector3(-1 * gunSizeScale, Mathf.Abs(gunSizeScale), Mathf.Abs(gunSizeScale));
           transform.localPosition = new Vector3(-0.45f - x_offset, -0.05f + y_offset, -0.1f);
        }
    }
}
