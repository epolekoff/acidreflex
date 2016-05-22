using UnityEngine;
using System.Collections.Generic;

public class FoodItem : MonoBehaviour {

    public int value = 1;
    Vector3 spawnDirection;
    float spawnForce = 50f;

    bool onBelt = false;
    float beltForce = 10f;

    Tutorial tutorialScript;

	// Use this for initialization
	void Start () {
        //HACK: Made it not add this force when it spawns on the conveyor belt :3
        if (transform.position.z > 0)
        {
            //Spawn the food so it goes in a random direction
            spawnDirection = new Vector3(Random.Range(-1f, 2f), 0, 0);
            GetComponent<Rigidbody>().AddForce(spawnDirection * spawnForce);
        }
        tutorialScript = GameObject.Find("Game").GetComponent<Tutorial>();
	}

    void Update()
    {
        //Slide down the conveyor belt.
        if (onBelt)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * beltForce);
        }


        //Catch the falling foods
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Person")
        {
            onBelt = false;
            return; 
        }

        //Hit Wall
        if (col.transform.tag == "Stomach")
        {
            GameObject.Find("Game").GetComponent<FoodSpawner>().EatPill();
            return; 
        }
        //If we hit a pill, destroy us both.
        else if (col.transform.tag == "Pill")
        {
            tutorialScript.hasHitPill = true;

            col.gameObject.GetComponent<AudioSource>().Play();
            if (transform.parent == null)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(col.gameObject);
            return;
        }
        else if (col.transform.tag == "ConveyorBelt")
        {
            onBelt = true;
            return;
        }

        //If we are not in a glob, make a new glob.
        if (transform.parent == null && col.transform.parent == null)
        {
            tutorialScript.hasCombined = true;
            GameObject newParent = new GameObject("Glob");
            Glob globScript = newParent.AddComponent<Glob>();
            transform.parent = newParent.transform;
            col.transform.parent = newParent.transform;
            col.rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;

            globScript.value += this.value;
            globScript.value += col.gameObject.GetComponent<FoodItem>().value;

            //Reset our values to 0 so they don't double-count.
            col.gameObject.GetComponent<FoodItem>().value = 0;
            value = 0;
        }
        //If they are in a glob, put us in that glob.
        else if(transform.parent == null)
        {
            transform.parent = col.transform.parent;
            GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;

            transform.parent.gameObject.GetComponent<Glob>().value += value;
            value = 0;//Reset our values to 0 so they don't double-count.
        }
        //If we are both in a glob
        else if(transform.parent != null && col.transform.parent != null && !transform.parent.Equals(col.transform.parent))
        {
            int globValue1 = transform.parent.GetComponent<Glob>().value;
            int globValue2 = col.transform.parent.GetComponent<Glob>().value;

            //Reset our values to 0 so they don't double-count.
            col.transform.parent.GetComponent<Glob>().value = 0;
            transform.parent.GetComponent<Glob>().value = 0;

            GameObject newParent = new GameObject("Glob");
            Glob globScript = newParent.AddComponent<Glob>();
            globScript.value = globValue1 + globValue2;
            //rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;

            //Move all of these objects to the other glob.
            Transform parentObject = transform.parent;
            Transform otherParent = col.transform.parent;

            //Get all siblings and move them to the new glob
            List<Transform> newChildren = new List<Transform>();
            foreach(Transform child in parentObject)
            {
                if (child.tag == "Food")
                {
                    newChildren.Add(child);
                }
            }

            //Get all the children of the other glob and move them to the new glob.
            foreach (Transform child in otherParent)
            {
                if (child.tag == "Food")
                {
                    newChildren.Add(child);
                }
            }
            //Set all of the childrens' parents.
            foreach (Transform t in newChildren)
            {
                t.parent = newParent.transform;
            }
            Destroy(parentObject.gameObject);
            Destroy(otherParent.gameObject);
        }

        //Joint the objects together.
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = col.gameObject.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Acid")
        {
            //If we hit acid, destroy us and add points.
            if (transform.parent == null)
            {
                GameObject.Find("Game").GetComponent<GameManager>().EatFood(1, 0);//Don't get points for individual food items
                Destroy(this.gameObject);
            }
            else
            {
                int m = transform.parent.GetComponent<Glob>().multiplier;
                int fp = transform.parent.GetComponent<Glob>().value;
                GameObject.Find("Game").GetComponent<GameManager>().EatFood(m, fp);
                Destroy(transform.parent.gameObject);
            }
        }

        else if(col.gameObject.tag == "MuppetTrigger")
        {
            Destroy(gameObject);
        }

    }
}
