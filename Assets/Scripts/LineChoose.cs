using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChoose : MonoBehaviour {
	public GameObject line;
	// Use this for initialization
	void Start () {
		
	}

	public void Click()
	{
		ToolManager.instance.Reset ();
		OutlineController.instance.SetTarget (gameObject);
		LineCreator.instance.StartDraw ();
		LineCreator.instance.linePrefab = line;

	}
}
