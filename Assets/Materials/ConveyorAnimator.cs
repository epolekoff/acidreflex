using UnityEngine;
using System.Collections;

public class ConveyorAnimator: MonoBehaviour {

	public Material mat;
	float timer = 0f;
    public float animSpeedX = 3f;
	public float animSpeedY = 0f;

	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		mat.SetTextureOffset ("_MainTex", new Vector2 ((animSpeedX*timer), (animSpeedY*timer)));

	}
}
