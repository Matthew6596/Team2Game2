using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float pushingPower = 1f;
    GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        box = GameObject.Find("openBox");
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
            box.transform.position += new Vector3(pushingPower,0,0);
        }
    }
}
