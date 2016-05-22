using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    Tutorial tutorialScript;

    Vector3 startPosition = Vector3.zero;
    Vector3 previousPosition;
    Vector3 finalPosition = Vector3.zero;
    float flickForce = -50f;
    float maxFlickForce = 150f;
    float sphereCastRadius = 3f;

    float foodZPlane = 20;

	// Use this for initialization
	void Start () {
        tutorialScript = GameObject.Find("Game").GetComponent<Tutorial>();
	}
	
	// Update is called once per frame
	void Update () {

        //Touch controls
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            //Determine the touch positions.
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                resetPositions(Input.GetTouch(0).position);
                //trailObjectReference = Instantiate(trailObject, Input.GetTouch(0).position, Quaternion.identity) as GameObject;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                startPosition = Input.GetTouch(0).position;
                finalPosition = previousPosition;
                previousPosition = startPosition;

                //trailObject.transform.position = startPosition;
            }
        }
        else
        {
            resetPositions(Vector3.zero);
            //Destroy(trailObjectReference);
        }
#else
        //Mouse controls
        if (Input.GetMouseButtonDown(0))
        {
            resetPositions(Input.mousePosition);
            //trailObjectReference = Instantiate(trailObject, Input.mousePosition, Quaternion.identity) as GameObject;
        }
        else if (Input.GetMouseButton(0))
        {
            startPosition = Input.mousePosition;
            finalPosition = previousPosition;
            previousPosition = startPosition;
            //trailObject.transform.position = startPosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            resetPositions(Vector3.zero);
        }
#endif

        //Check if an object was hit.
        startPosition.z = foodZPlane;
        finalPosition.z = foodZPlane;
        moveObject(startPosition, finalPosition);
	}

    
    void moveObject(Vector3 startPosition, Vector3 finalPosition)
    {
        //Return out of here if we don't have a flick line.
        if (Vector3.Distance(startPosition, finalPosition) == 0){ return; }
        

        Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint(startPosition);
        Vector3 worldFinalPosition = Camera.main.ScreenToWorldPoint(finalPosition);

        Vector3 slashStartPosition = new Vector3(worldStartPosition.x, worldStartPosition.y, foodZPlane);
        Vector3 slashFinalPosition = new Vector3(worldFinalPosition.x, worldFinalPosition.y, foodZPlane);
        Vector3 flickVector = slashFinalPosition - slashStartPosition;

        Ray slashRay = new Ray(slashStartPosition,
                            flickVector);

        RaycastHit hit;
        int layerMask = ~8;
        if(Physics.SphereCast(slashRay, sphereCastRadius, out hit, flickVector.magnitude, layerMask)){
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(Vector3.ClampMagnitude(flickVector * flickForce, maxFlickForce));
                hit.rigidbody.AddForce(new Vector3(0, 0.5f, -1) * Random.Range(0, flickForce));
                tutorialScript.hasSwiped = true;
            }
        }
    }

    void resetPositions(Vector3 lastPosition)
    {
        startPosition = lastPosition;
        finalPosition = lastPosition;
        previousPosition = lastPosition;
    }
}
