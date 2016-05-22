using UnityEngine;
using System.Collections;

public class Pill : MonoBehaviour {

    float topBoundary;

    void Start()
    {
        topBoundary = GameObject.Find("SpawnPoint").transform.position.y;
    }

    void Update()
    {
        //If we throw it back up the throat, puke it up.
        if (transform.position.y > topBoundary)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //If we hit acid, destroy us
        Destroy(gameObject);
    }
}
