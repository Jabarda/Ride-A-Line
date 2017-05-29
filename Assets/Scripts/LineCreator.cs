using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LineCreator : MonoBehaviour {
	
	public GameObject linePrefab;
	public static LineCreator instance;
	private bool pressed;
	public bool enabled;
	GameObject lineGO;
	Line activeLine;
	void Start()
	{
		enabled = false;
		pressed = false;
		instance = this;
	}
	public void StartDraw()
	{
		enabled = true;
	}
	public void StopDraw()
	{
		enabled = false;
	}
	void Update ()
	{
		if (enabled) {
			if (!EventSystem.current.IsPointerOverGameObject (Input.GetTouch(0).fingerId) && Input.touchCount<=1) {
				if ((Input.GetMouseButton (0) || Input.touchCount==1) && activeLine == null && !Input.GetMouseButtonUp (0)) {
					lineGO = Instantiate (linePrefab);
					activeLine = lineGO.GetComponent<Line> ();
				}
					
			if (Input.GetMouseButtonUp (0)|| Input.touchCount==0) {
					Destroy (lineGO);
					activeLine = null;
				}
					
				if (activeLine != null) {
					Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.touches[0].position);
					activeLine.UpdateLine (mousePos);
				}

			}
		}
	}

	public void StartNewLine()
	{
		activeLine = null;
		if(Input.GetMouseButton (0)|| Input.touchCount==1){
		lineGO = Instantiate (linePrefab);
		activeLine = lineGO.GetComponent<Line> ();
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		activeLine.UpdateLine (mousePos);
		}
	}
}
