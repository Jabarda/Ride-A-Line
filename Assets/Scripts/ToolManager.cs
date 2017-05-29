using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour {
	public static ToolManager instance;
	public GameObject ButtonPlay;
	public GameObject ButtonDefault;
	public GameObject ButtonAcceleration;
	public GameObject ButtonBounce;
	public GameObject ButtonReverse;
	public GameObject Eraser;
	public GameObject MapControl;
	// Use this for initialization
	void Start () {
		instance = this;
		Invoke ("Reset", 0.1f);
	}

	public void Reset()
	{
		
		SaveLoadManager.instance.HideLoadDialoge ();
		LineCreator.instance.StopDraw ();
		EraserDestroyer.instance.StopErase ();
		TouchCamera.instance.StopResize ();
		CameraFollower.instance.StopFollow ();
		SaveLoadManager.instance.AutoSave ();
		SaveLoadManager.instance.HideSaveSuccessPanel ();
	}
	/*
	public void Play ()
	{
		ButtonPlay.GetComponent<PlayButtonController> ().Clicked ();
	}

	public void SetDefault()
	{
		ButtonDefault.GetComponent<LineChoose> ().Click;	
	}
	public void SetAcceleration()
	{
		ButtonAcceleration.GetComponent<LineChoose> ().Click;	
	}
	public void SetBounce()
	{
		ButtonBounce.GetComponent<LineChoose> ().Click;	
	}
	public void SetReverse()
	{
		ButtonReverse.GetComponent<LineChoose> ().Click;	
	}
	*/
}
