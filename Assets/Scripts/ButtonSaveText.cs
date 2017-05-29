using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSaveText : MonoBehaviour {
	public GameObject TextAttached;

	public void SetText(string s)
	{
		TextAttached.GetComponent<Text> ().text = s;
	}

	public string GetText()
	{
		return TextAttached.GetComponent<Text> ().text;
	}

	public void SetSavefileName()
	{
		SaveLoadManager.instance.FileNameToLoad = GetText ();
	}
}
