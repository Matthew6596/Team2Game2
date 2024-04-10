using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiEnemy : MonoBehaviour
{
    public GameObject target;
    public float speed;

    public GameObject hungerIndicatorPrefab;
    public int hunger = 1;

    Transform hungerPanel;

    public LayerMask playerLayer;

    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = GameObject.Find("Player");

        //Create hunger UI
        hungerPanel = transform.GetChild(2).GetChild(0);
        for(int i=0; i<hunger; i++)
            Instantiate(hungerIndicatorPrefab,hungerPanel);
    }

    // Update is called once per frame
    void Update()
    {
        //Chase target
        if (active)
        {
            transform.LookAt(target.transform);
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime), Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chip"))
        {
            Destroy(collision.gameObject);
            hunger--;
            Destroy(hungerPanel.GetChild(0).gameObject);
            if (hunger <= 0) //Number of chips needed to sway yeti
                becomeFriendly();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject==target)
        {
            Destroy(GetComponent<SphereCollider>());
            active = true;
        }
    }

    void becomeFriendly()
    {
        //Add friend script to yeti
        YetiFriend yay = gameObject.AddComponent<YetiFriend>();
        yay.speed = speed;
        yay.target = target;
        yay.player = GameObject.Find("Player").transform;
        gameObject.tag = "Friend";
        gameObject.GetComponent<CapsuleCollider>().excludeLayers = playerLayer;

        //teleport behind player
        yay.teleportBehindPlayer();

        //done, remove enemy script from yeti
        Destroy(hungerPanel.gameObject);
        Destroy(this);
    }
}
