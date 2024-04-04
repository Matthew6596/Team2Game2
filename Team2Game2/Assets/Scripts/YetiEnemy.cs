using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiEnemy : MonoBehaviour
{
    public GameObject target;
    public float speed;

    public int hunger = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Chase target
        transform.LookAt(target.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed*Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chip"))
        {
            Destroy(collision.gameObject);
            hunger--;
            if (hunger <= 0) //Number of chips needed to sway yeti
                becomeFriendly();
        }
    }

    void becomeFriendly()
    {
        //Add friend script to yeti
        YetiFriend yay = gameObject.AddComponent<YetiFriend>();
        yay.speed = speed;

        //done, remove enemy script from yeti
        Destroy(this);
    }
}
