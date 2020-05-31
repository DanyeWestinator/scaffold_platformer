using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueMaster : MonoBehaviour
{
    public GameObject dialogueCanvas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogueCanvas.SetActive(true);
            this.gameObject.GetComponent<dialogueMaster>().enabled = false;
            Destroy(this.gameObject.GetComponent<dialogueMaster>());
            Time.timeScale = 0f;
        }
    }
}
