using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public float spawnRate;
    public GameObject yetiPrefab;
    public int numberOfSpawns=10;
    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(spawnYeti());
    }

    IEnumerator spawnYeti()
    {
        Instantiate(yetiPrefab,gameObject.transform);
        numberOfSpawns--;
        yield return new WaitForSeconds(spawnRate);
        if(numberOfSpawns>0) StartCoroutine(spawnYeti());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !active)
        {
            active = true;
            StartCoroutine(spawnYeti());
        }
    }
}
