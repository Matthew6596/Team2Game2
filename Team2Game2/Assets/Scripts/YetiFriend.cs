using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiFriend : MonoBehaviour
{
    public GameObject target;
    public float speed;
    //public Transform player;

    void Update()
    {
        //Chase target
        transform.LookAt(target.transform);
        transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime), Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));

        if (transform.position.x > target.transform.position.x)
        {
            teleportBehindPlayer();
        }
    }

    public void teleportBehindPlayer()
    {
        transform.position = new Vector3(target.transform.position.x - 5, transform.position.y, transform.position.z);
    }
}
