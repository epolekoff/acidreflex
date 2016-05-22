using UnityEngine;
using System.Collections;

public class Multiplier : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.parent.GetChild(0).position + Vector3.up;
	}
}
