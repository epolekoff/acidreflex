using UnityEngine;
using System.Collections;

public class Glob : MonoBehaviour {

    public int value = 0;
    public int multiplier = 1;
    int oldMultiplier = 1;
    private GameObject multiplierText;

    void Start()
    {
        multiplierText = Resources.Load("Multiplier", typeof(GameObject)) as GameObject;
        multiplierText = Instantiate(multiplierText) as GameObject;
        multiplierText.AddComponent<Multiplier>();
        multiplierText.transform.parent = transform;

        transform.GetChild(0).GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezePositionZ;
    }

    void Update(){
        multiplier = transform.childCount - 1;

        //Update the text above the glob.
        if(oldMultiplier != multiplier)
        {
            multiplierText.GetComponent<TextMesh>().text = "X" + multiplier;
            oldMultiplier = multiplier;
        }
    }
}
