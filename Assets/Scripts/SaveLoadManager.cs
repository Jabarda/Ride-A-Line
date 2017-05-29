using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveLoadManager : MonoBehaviour {
	public static SaveLoadManager instance;
	Vector3[] buf= new Vector3[2];
	public GameObject SavePanel;
	public GameObject DefaultPref, AccelerationPref, BouncePref, ReversePref;
	public GameObject InputObj, SaveConfirmPanel, SaveSuccessPanel;
	string Filename;
	string CurrentFloat;
	float[] CurrentPositions;
	private string current;
	int i;
	public int SavedPositions;

	void Start()
	{
		instance = this;
		if (!PlayerPrefs.HasKey ("SavedPositions"))
			PlayerPrefs.SetInt ("SavedPositions", 0);
		SavedPositions = PlayerPrefs.GetInt ("SavedPositions");
		if (PlayerPrefs.HasKey("Auto Save")) load ("Auto Save");
	}

	public void AutoSave()
	{
		PreSave ("Auto Save");
	}

    void PreSave (string s)
	{
		Filename = s;
		bool IsNew = true;
		for (int i = 0; i < SavedPositions; i++)
			if (PlayerPrefs.GetString (i.ToString()) == s)
				IsNew = false;
		if (IsNew) {
			SavedPositions++;
			PlayerPrefs.SetInt ("SavedPositions", SavedPositions);
			PlayerPrefs.SetString ((SavedPositions - 1).ToString(), Filename);
			ConfirmSave ();
		} else {
			if (Filename != "Auto Save")
				ShowCheckDialoge ();
			else
				ConfirmSave ();
		}
	}
	void ConfirmSave()
	{
		Save (Filename);
	}

	public void SaveButton()
	{
		
		Filename = InputObj.GetComponent<InputField> ().text;
		if (Filename.Length == 0)
			PreSave ("Quick Save");
		else
			PreSave (Filename);
	}

	//Some dialoge controllers
	void ShowCheckDialoge()
	{
		SaveConfirmPanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
	}

	public void HideCheckDialoge()
	{
		SaveConfirmPanel.GetComponent<RectTransform> ().localScale = new Vector3 (0f, 0f, 1f);
	}

	public void ShowSavePanel()
	{
		ToolManager.instance.Reset ();
		SavePanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
	}

	public void HideSavePanel()
	{
		SavePanel.GetComponent<RectTransform> ().localScale = new Vector3 (0f, 0f, 1f);
	}

	void ShowSaveSuccessPanel()
	{
		SaveSuccessPanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
	}

	public void HideSaveSuccessPanel()
	{
		HideSavePanel ();
		SaveSuccessPanel.GetComponent<RectTransform> ().localScale = new Vector3 (0f, 0f, 1f);
	}

	public void SaveCheckDialogeConfirm()
	{
		ConfirmSave ();
		HideCheckDialoge ();
	}
	//Save dialoge controllers end

	//load logic
	public GameObject Content;
	public string FileNameToLoad;
	public GameObject ButtonPrefab;
	public GameObject LoadPanel;
	Vector3 posioner;

	public void ShowLoadDialoge()
	{
		FileNameToLoad = "jabardacrabsuck0192837465";
		ToolManager.instance.Reset ();
		FileNameToLoad = "";
		SavedPositions = PlayerPrefs.GetInt ("SavedPositions");
		GameObject[] buttons = GameObject.FindGameObjectsWithTag ("LoadButtons");
		foreach (GameObject button in buttons)
			Destroy (button);
		Content.GetComponent<RectTransform> ().sizeDelta=new Vector2(0f,40f*SavedPositions);
		for (int i = 0; i < SavedPositions; i++) {
			GameObject newButton = Instantiate (ButtonPrefab);
			newButton.GetComponent<ButtonSaveText> ().SetText (PlayerPrefs.GetString (i.ToString ()));
			if (i == 0)
				posioner = new Vector3 (0f, 20f * SavedPositions - 20f, 0f);
			else
				posioner = posioner - new Vector3 (0f, 40f, 0f);
			newButton.GetComponent<RectTransform> ().localPosition = posioner; 
			newButton.transform.SetParent (Content.transform, false);
		}
		LoadPanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
	}	 

	public void LoadButtonClicked()
	{
		if (FileNameToLoad != "jabardacrabsuck0192837465")
			load (FileNameToLoad);
		HideLoadDialoge ();
	}


	public void HideLoadDialoge()
	{
		LoadPanel.GetComponent<RectTransform> ().localScale = new Vector3 (0f, 0f, 1f);
	}

	public GameObject LoadFromPanel;
	public GameObject InputCode;
	public void ShowLoadFromPanel()
	{
		LoadFromPanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
	}
	public void HideLoadFromPanel()
	{
		LoadFromPanel.GetComponent<RectTransform> ().localScale = new Vector3 (0f, 0f, 1f);
	}

	public void Share()
	{
		string buf=StringCompressor.Compress (SaveSharing ());
		print (buf);
		print (StringCompressor.Decompress(buf));
		StartCoroutine(ShareAndRate.ShareAndroidText (buf));
	}

	public void LoadFromCode()
	{
		string inp = InputCode.GetComponent<InputField> ().text;
		LoadSharing (StringCompressor.Decompress(inp));
		HideLoadFromPanel ();
		HideLoadDialoge ();
	}
	public void LoadSharing(string current)
	{
		Clear.instance.ClearAll ();
		i = 0;
		if (current [i] == 'd') {
			while (current [i+1] != 'a') {
				i++;
				CurrentPositions = ParsePositions ();
				GameObject line = Instantiate (DefaultPref);
				Vector3[] pos = new Vector3[2];
				pos [0] = new Vector3 (CurrentPositions [0], CurrentPositions [1]);
				pos [1] = new Vector3 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<LineRenderer> ().SetPositions (pos);
				Vector2[] edge = new Vector2[2];
				edge [0] = new Vector2 (CurrentPositions [0], CurrentPositions [1]);
				edge [1] = new Vector2 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<EdgeCollider2D> ().points=edge;
			}
		}
		i++;
		if (current [i] == 'a') {
			while ( current [i+1] != 'b') {
				i++;

				CurrentPositions = ParsePositions ();
				GameObject line = Instantiate (AccelerationPref);
				Vector3[] pos = new Vector3[2];
				pos [0] = new Vector3 (CurrentPositions [0], CurrentPositions [1]);
				pos [1] = new Vector3 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<LineRenderer> ().SetPositions (pos);
				Vector2[] edge = new Vector2[2];
				edge [0] = new Vector2 (CurrentPositions [0], CurrentPositions [1]);
				edge [1] = new Vector2 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<EdgeCollider2D> ().points=edge;
			}
		}
		i++;
		if (current [i] == 'b') {
			while (current [i+1] != 'r') {
				i++;
				CurrentPositions = ParsePositions ();
				GameObject line = Instantiate (BouncePref);
				Vector3[] pos = new Vector3[2];
				pos [0] = new Vector3 (CurrentPositions [0], CurrentPositions [1]);
				pos [1] = new Vector3 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<LineRenderer> ().SetPositions (pos);
				Vector2[] edge = new Vector2[2];
				edge [0] = new Vector2 (CurrentPositions [0], CurrentPositions [1]);
				edge [1] = new Vector2 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<EdgeCollider2D> ().points=edge;
			}
		}
		i++;
		if (current [i] == 'r') {
			while ((i+2)<=current.Length) {
				i++;
				CurrentPositions = ParsePositions ();
				GameObject line = Instantiate (ReversePref);
				Vector3[] pos = new Vector3[2];
				pos [0] = new Vector3 (CurrentPositions [0], CurrentPositions [1]);
				pos [1] = new Vector3 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<LineRenderer> ().SetPositions (pos);
				Vector2[] edge = new Vector2[2];
				edge [0] = new Vector2 (CurrentPositions [0], CurrentPositions [1]);
				edge [1] = new Vector2 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<EdgeCollider2D> ().points=edge;
			}
		}
	}
	public string SaveSharing()
	{
		current = "d";//default
		GameObject[] DefaultLines = GameObject.FindGameObjectsWithTag("DefaultLine");
		foreach (GameObject Line in DefaultLines) {
			Line.GetComponent<LineRenderer> ().GetPositions (buf);
			current += buf[0].x+"_"+buf[0].y+"_"+buf[1].x+"_"+buf[1].y+"_";
		}
		current += "a";//Acceleration
		GameObject[] AccelerationLines = GameObject.FindGameObjectsWithTag("AccelerationLine");
		foreach (GameObject Line in AccelerationLines) {
			Line.GetComponent<LineRenderer> ().GetPositions (buf);
			current += buf[0].x+"_"+buf[0].y+"_"+buf[1].x+"_"+buf[1].y+"_";
		}
		current += "b";//Bounce
		GameObject[] BounceLines = GameObject.FindGameObjectsWithTag("BounceLine");
		foreach (GameObject Line in BounceLines) {
			Line.GetComponent<LineRenderer> ().GetPositions (buf);
			current += buf[0].x+"_"+buf[0].y+"_"+buf[1].x+"_"+buf[1].y+"_";
		}
		current += "r";//Reverse
		GameObject[] ReverseLines = GameObject.FindGameObjectsWithTag("ReverseLine");
		foreach (GameObject Line in ReverseLines) {
			Line.GetComponent<LineRenderer> ().GetPositions (buf);
			current += buf[0].x+"_"+buf[0].y+"_"+buf[1].x+"_"+buf[1].y+"_";
		}
		return current;

	}

	public void Save(string s)
	{
		current = "d";//default
		GameObject[] DefaultLines = GameObject.FindGameObjectsWithTag("DefaultLine");
		foreach (GameObject Line in DefaultLines) {
			Line.GetComponent<LineRenderer> ().GetPositions (buf);
			current += buf[0].x+"_"+buf[0].y+"_"+buf[1].x+"_"+buf[1].y+"_";
		}
		current += "a";//Acceleration
		GameObject[] AccelerationLines = GameObject.FindGameObjectsWithTag("AccelerationLine");
		foreach (GameObject Line in AccelerationLines) {
			Line.GetComponent<LineRenderer> ().GetPositions (buf);
			current += buf[0].x+"_"+buf[0].y+"_"+buf[1].x+"_"+buf[1].y+"_";
		}
		current += "b";//Bounce
		GameObject[] BounceLines = GameObject.FindGameObjectsWithTag("BounceLine");
		foreach (GameObject Line in BounceLines) {
			Line.GetComponent<LineRenderer> ().GetPositions (buf);
			current += buf[0].x+"_"+buf[0].y+"_"+buf[1].x+"_"+buf[1].y+"_";
		}
		current += "r";//Reverse
		GameObject[] ReverseLines = GameObject.FindGameObjectsWithTag("ReverseLine");
		foreach (GameObject Line in ReverseLines) {
			Line.GetComponent<LineRenderer> ().GetPositions (buf);
			current += buf[0].x+"_"+buf[0].y+"_"+buf[1].x+"_"+buf[1].y+"_";
		}
		PlayerPrefs.SetString (s, current);
		ShowSaveSuccessPanel ();
	}

	public void load(string s)
	{
		Clear.instance.ClearAll ();
		current=PlayerPrefs.GetString(s);
		i = 0;
		if (current [i] == 'd') {
			while (current [i+1] != 'a') {
				i++;
				CurrentPositions = ParsePositions ();
				GameObject line = Instantiate (DefaultPref);
				Vector3[] pos = new Vector3[2];
				pos [0] = new Vector3 (CurrentPositions [0], CurrentPositions [1]);
				pos [1] = new Vector3 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<LineRenderer> ().SetPositions (pos);
				Vector2[] edge = new Vector2[2];
				edge [0] = new Vector2 (CurrentPositions [0], CurrentPositions [1]);
				edge [1] = new Vector2 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<EdgeCollider2D> ().points=edge;
			}
		}
		i++;
		if (current [i] == 'a') {
			while ( current [i+1] != 'b') {
				i++;

				CurrentPositions = ParsePositions ();
				GameObject line = Instantiate (AccelerationPref);
				Vector3[] pos = new Vector3[2];
				pos [0] = new Vector3 (CurrentPositions [0], CurrentPositions [1]);
				pos [1] = new Vector3 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<LineRenderer> ().SetPositions (pos);
				Vector2[] edge = new Vector2[2];
				edge [0] = new Vector2 (CurrentPositions [0], CurrentPositions [1]);
				edge [1] = new Vector2 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<EdgeCollider2D> ().points=edge;
			}
		}
		i++;
		if (current [i] == 'b') {
			while (current [i+1] != 'r') {
				i++;
				CurrentPositions = ParsePositions ();
				GameObject line = Instantiate (BouncePref);
				Vector3[] pos = new Vector3[2];
				pos [0] = new Vector3 (CurrentPositions [0], CurrentPositions [1]);
				pos [1] = new Vector3 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<LineRenderer> ().SetPositions (pos);
				Vector2[] edge = new Vector2[2];
				edge [0] = new Vector2 (CurrentPositions [0], CurrentPositions [1]);
				edge [1] = new Vector2 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<EdgeCollider2D> ().points=edge;
			}
		}
		i++;
		if (current [i] == 'r') {
			while ((i+2)<=current.Length) {
				i++;
				CurrentPositions = ParsePositions ();
				GameObject line = Instantiate (ReversePref);
				Vector3[] pos = new Vector3[2];
				pos [0] = new Vector3 (CurrentPositions [0], CurrentPositions [1]);
				pos [1] = new Vector3 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<LineRenderer> ().SetPositions (pos);
				Vector2[] edge = new Vector2[2];
				edge [0] = new Vector2 (CurrentPositions [0], CurrentPositions [1]);
				edge [1] = new Vector2 (CurrentPositions [2], CurrentPositions [3]);
				line.GetComponent<EdgeCollider2D> ().points=edge;
			}
		}

	}

	float[] ParsePositions()
	{
		float[] tempPos = new float[4];
		for (int j = 0; j < 4; j++) {
			CurrentFloat = "";
			while (current [i] != '_') {
				CurrentFloat += current [i];
				i++;
			}
			i++;
			tempPos [j] = float.Parse (CurrentFloat);
		}
		i--;
		return tempPos;
	}
}
