using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour {

	public void Click()
	{
		ToolManager.instance.Reset ();
		OutlineController.instance.SetTarget (gameObject);
		EraserDestroyer.instance.Erase ();
	}
}
