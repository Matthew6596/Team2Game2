using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class BoxScript : MonoBehaviour
{
    public float pushingPower = 1f;
    GameObject box;

    Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        box = GameObject.Find("openBox");
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Contact!");
            playerAnim.SetBool("isPushing", true);
            box.transform.position += new Vector3(pushingPower,0,0);
        }
        Debug.Log("bruh");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAnim.SetBool("isPushing", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bruh! " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Finish")))
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
