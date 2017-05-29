using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EraserDestroyer : MonoBehaviour {
	public static EraserDestroyer instance;
	float Mode;
	public bool erasing;
	Transform trans;
	Camera camera;
	void Start()
	{
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		trans = GetComponent<Transform>();
		instance = this;
		Mode = -20f;
		erasing = false;
	}
	public void Erase()
	{
		
		erasing = true;
		GetComponent<CircleCollider2D> ().enabled = true;
	}
	public void StopErase()
	{
		erasing = false;
		GetComponent<CircleCollider2D> ().enabled = false;
	}

	// Use this for initialization
	void Update()
	{
		trans.localScale = new Vector3(3f*(float)camera.orthographicSize / 5f,3f*(float)camera.orthographicSize / 5f,1f);
		if (!EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId) && Input.touchCount <= 1 && erasing) {
			Mode = 0f;
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			gameObject.transform.position = new Vector3 (mousePos.x, mousePos.y, Mode);
		} else {
			Mode = -20f;
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, Mode);
		}
	}

	void OnCollisionEnter2D (Collision2D coll){
		if ((coll.gameObject.tag == "DefaultLine" || coll.gameObject.tag == "AccelerationLine" || 
			coll.gameObject.tag == "BounceLine" ||coll.gameObject.tag == "ReverseLine")
			&& erasing) {
			Destroy (coll.gameObject);
		}
	}
}
