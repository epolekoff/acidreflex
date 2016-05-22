using UnityEngine;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour {

	//Food Objects
    GameObject pillObject;
    List<GameObject> foodObjects = new List<GameObject>();

    //Spawn timer
    const int SPAWN_TIMER_MAX = 60;
    int spawnTimer = SPAWN_TIMER_MAX;

    //Spawn properties
    Vector3 spawnPosition;
    Vector3 conveyorSpawnPosition;
    Queue<GameObject> foodQueue = new Queue<GameObject>();
    int numPills = 2;
    const int NUM_PILLS_MAX = 3;

	// Use this for initialization
	void Start () {
        //Load objects
        pillObject = Resources.Load("Pill", typeof(GameObject)) as GameObject;
        foodObjects.Add(Resources.Load("Broccoli", typeof(GameObject)) as GameObject);
        foodObjects.Add(Resources.Load("Steak", typeof(GameObject)) as GameObject);
        foodObjects.Add(Resources.Load("Meat", typeof(GameObject)) as GameObject);
        foodObjects.Add(Resources.Load("Watermelon", typeof(GameObject)) as GameObject);
        foodObjects.Add(Resources.Load("Avocado", typeof(GameObject)) as GameObject);
        foodObjects.Add(Resources.Load("Donut", typeof(GameObject)) as GameObject);
        foodObjects.Add(Resources.Load("Sushi", typeof(GameObject)) as GameObject);

        spawnPosition = GameObject.Find("SpawnPoint").transform.position;
        conveyorSpawnPosition = GameObject.Find("ConveyorSpawn").transform.position;

        //Start the queue
        foodQueue.Enqueue(foodObjects[Random.Range(0, foodObjects.Count)]);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //Spawn food
        spawnTimer--;
        if(spawnTimer == 0)
        {
            GameObject conveyorFood = foodObjects[Random.Range(0, foodObjects.Count)];
            foodQueue.Enqueue(conveyorFood);
            Instantiate(conveyorFood, conveyorSpawnPosition, Quaternion.identity);

            //Spawn the game food.
            GameObject newFood = foodQueue.Dequeue();
            GameObject.Instantiate(newFood, spawnPosition, Quaternion.Euler(0, 235, 90));

            if(newFood.tag == "Pill")
            {
                numPills --;
            }

            spawnTimer = SPAWN_TIMER_MAX;
        }
	}

    /// <summary>
    /// Eat a pill if we have space in the queue for it.
    /// </summary>
    public void EatPill()
    {
        if (numPills < NUM_PILLS_MAX)
        {
            foodQueue.Enqueue(pillObject);
            numPills++;
        }
    }
}
