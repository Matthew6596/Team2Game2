using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockZPosition : MonoBehaviour
{
    float zpos;
    // Start is called before the first frame update
    void Start()
    {
        zpos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position.Set(transform.position.x,transform.position.y,zpos);
    }
}
