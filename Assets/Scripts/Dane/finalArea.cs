using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalArea : MonoBehaviour
{
    public GameObject bouncePad;
    public GameObject boss;

    public float updateTime = 0.5f;
    private float timeSince = 0f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("credits");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        timeSince += Time.deltaTime;
        if (timeSince >= updateTime)
        {
            timeSince = 0f;
            if (boss.activeInHierarchy == false)
            {
                bouncePad.SetActive(true);
            }
        }
    }
}
