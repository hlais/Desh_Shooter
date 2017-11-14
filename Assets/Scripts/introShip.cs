using UnityEngine;
using System.Collections;

public class introShip : MonoBehaviour {

	public float speed;
	Transform ship;
	void Start () {

		ship = GetComponent<Transform> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		ship.position += Vector3.left * speed * Time.deltaTime;
	
	}
}
