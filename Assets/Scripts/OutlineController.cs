using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour {
	public static OutlineController instance;

	void Start()
	{
		instance = this;
	}

	public void SetTarget(GameObject obj)
	{
		transform.position = obj.transform.position;
	}
}
