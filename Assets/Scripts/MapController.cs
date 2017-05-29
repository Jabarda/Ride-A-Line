using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	public void Click()
	{
		ToolManager.instance.Reset ();
		OutlineController.instance.SetTarget (gameObject);
		TouchCamera.instance.StartResize ();
	}
}
