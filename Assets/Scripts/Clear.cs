using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clear : MonoBehaviour {
	public static Clear instance;
	public GameObject panel;
	public Button yes, no;
	void Start()
	{
		instance = this;
		HideDialoge ();
	}

	public void ShowDialoge()
	{
		panel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
		yes.enabled = true;
		no.enabled = true;
	}

	public void HideDialoge()
	{
		panel.GetComponent<RectTransform> ().localScale = new Vector3 (0f, 0f, 0f);
		yes.enabled = false;
		no.enabled = false;
	}


	// Use this for initialization
	public void ClearAll()
	{
		GameObject[] DefaultLines = GameObject.FindGameObjectsWithTag("DefaultLine");
		foreach (GameObject Line in DefaultLines) 
			Destroy(Line);
		DefaultLines = GameObject.FindGameObjectsWithTag("AccelerationLine");
		foreach (GameObject Line in DefaultLines) 
			Destroy(Line);
		DefaultLines = GameObject.FindGameObjectsWithTag("BounceLine");
		foreach (GameObject Line in DefaultLines) 
			Destroy(Line);
		DefaultLines = GameObject.FindGameObjectsWithTag("ReverseLine");
		foreach (GameObject Line in DefaultLines) 
			Destroy(Line);
		HideDialoge ();
	}
}
