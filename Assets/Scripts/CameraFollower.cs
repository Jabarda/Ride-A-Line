using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
	public static CameraFollower instance;
	public GameObject player;
	public float offset;
	private Vector3 playerPosition, veloc;
	public float offsetSmoothing;
	int x,y;
	public bool enabled;
	public Vector3 prevpos;
	private Camera cam;
	// Use this for initialization
	void Start () {
		instance = this;
		prevpos = transform.position;
		cam = GetComponent<Camera> ();
	}

	public void StartFollow()
	{
		enabled = true;
		prevpos = transform.position;
	}

	public void StopFollow()
	{
		enabled = false;
		transform.position = prevpos;
	}


	// Update is called once per frame
	void Update () {
		if (enabled) {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerPosition = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
			//transform.position = Vector3.Lerp (transform.position, playerPosition, offsetSmoothing*Time.deltaTime);
			transform.position=playerPosition;
		} else {
			prevpos=transform.position;
		}
	}
}
