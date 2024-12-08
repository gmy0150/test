using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Application.Quit();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("»Æ¿Œ");
                Application.Quit();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
