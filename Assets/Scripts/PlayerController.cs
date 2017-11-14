using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public AudioClip fireSound;

	Transform ship;
	public float speed = 8.0f;
	float xMin; // posiion we can move too
	float xMax;
	public float padding = 1f;
	public float lazerSpeed;
	public float fireRate;
	public float playerHealth = 200;
	public AudioClip hitSound;

	public GameObject lazers;

	public AudioClip gameOverYea;

	// Use this for initialization
	void Start ()
	{


		ship = GetComponent<Transform> ();

		// this bascically gives the distance betwen player transform and main camera transform. 
		//changing Z because its a front end view 
		float distance = transform.position.z - Camera.main.transform.position.z;

		//
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)); //is the LEFt most postion. 0.5/0.5 would be the center
																							//2nd part of this code was to put it into a vector 3 
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));

		//now we are just puting the valuves into the floats.


		xMin = leftmost.x + padding;
		xMax = rightmost.x - padding;


	}

	public void FireWeapon ()
	{
		Vector3 offset = new Vector3 (0, 0.5f, 0);

		// first we instantiated then we put it in beam object
		GameObject beam = (GameObject)Instantiate (lazers, transform.position+offset, Quaternion.identity);
		beam.rigidbody2D.velocity = new Vector3 (0, lazerSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKey (KeyCode.LeftArrow)) {

			ship.position += Vector3.left * speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {

			ship.position += Vector3.right * speed * Time.deltaTime;
		}
		//restricts the player to the game space// below is the old position. Current position 
		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);

		//reseting transform
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);


		if (Input.GetKeyDown (KeyCode.Space)) {

			InvokeRepeating ("FireWeapon", 0.000001f, fireRate);
			Debug.Log ("Fire Weapon");
		}

		if (Input.GetKeyUp (KeyCode.Space)) {

			CancelInvoke ("FireWeapon");

		}

	}

	void Die ()
	{
		
		Destroy (gameObject);
		LevelManager manager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		manager.LoadLevel ("Win Screen");
	}

	void OnCollisionEnter2D (Collision2D collider)
	{

		// we get the gameObject and extract the other componenets. this gets access the the -script- lazers. and all its funtions
		EnemyLazer enemyMissile = collider.gameObject.GetComponent<EnemyLazer> ();

		//check if mission exist/ and isnt false value if it didnt have Lazer Component 
		//basically check if there is a script. we made an instance called MIssle 

		if (enemyMissile) {
			Debug.Log ("We have been hit");
			AudioSource.PlayClipAtPoint (hitSound, transform.position);

			playerHealth -= enemyMissile.GetEnemyDamage ();

			//calling the hit method.
			enemyMissile.Hit ();

			if (playerHealth <= 0) {
				//destroy this ship

				AudioSource.PlayClipAtPoint (gameOverYea, transform.position);
				Die ();
			}


		}

	}
}
