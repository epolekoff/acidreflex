using UnityEngine;
using System.Collections;

public class EatTrigger : MonoBehaviour {


    public Animator muppetAnimation;


    void OnTriggerEnter(Collider col)
    {
        muppetAnimation.SetTrigger("eat");
    }
}
