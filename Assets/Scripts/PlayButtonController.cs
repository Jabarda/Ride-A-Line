using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonController : MonoBehaviour {

	private Image img;
	public Sprite start,stop;
	private bool IsPlaying; //
	public GameObject PlayerPref;
	public GameObject Player;
	void Start()
	{
		IsPlaying = false;
		img = GetComponent<Image> ();

	}

	void PlacePlayer()
	{
		Destroy (Player);
		Player = Instantiate (PlayerPref);
		Player.transform.localPosition = new Vector2 (-5f, 0f);
		Player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		Player.GetComponent<Transform> ().rotation = Quaternion.identity;	

	}

	void StartGame()
	{
		ToolManager.instance.Reset ();
		CameraFollower.instance.StartFollow ();
		PlacePlayer ();
		GameObject[] bones = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject bone in bones)
			bone.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		IsPlaying = !IsPlaying;
		img.sprite=stop;
	}

	void StopGame()
	{
		CameraFollower.instance.StopFollow ();
		PlacePlayer ();
		Player.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
		IsPlaying = !IsPlaying;
		img.sprite= start;
	}


	public void Clicked()
	{
		ToolManager.instance.Reset ();
		OutlineController.instance.SetTarget (gameObject);
		if (!IsPlaying) {
			StartGame ();
		} else {
			StopGame ();	
		}
	}

}
