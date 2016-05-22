using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.GetComponent<Rigidbody2D>() != null)
        {
            transform.parent.GetComponent<Water>().Splash(transform.position.x, Hit.GetComponent<Rigidbody2D>().velocity.y * Hit.GetComponent<Rigidbody2D>().mass / 40f);
        }
    }

    void OnTriggerEnter(Collider Hit)
    {
        if (Hit.GetComponent<Rigidbody>() != null)
        {
            transform.parent.GetComponent<Water>().Splash(transform.position.x, Hit.GetComponent<Rigidbody>().velocity.y * Hit.GetComponent<Rigidbody>().mass/5f);
        }
    }
}