using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
	public GameObject enemyLazers;
	public float health = 150;
	public float enemyLazerSpeed = 1f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 50;
	private ScoreKeeper scoreKeeper;
	public AudioClip fireSound;
	public AudioClip deathSound;

	//collider is game object we are coliding with

	void Start ()

	//gameobject find
	//gets hold of Text from the inspecter.  dynamic way 
	{
		scoreKeeper = GameObject.Find ("Text").GetComponent<ScoreKeeper> ();
	}

	void OnCollisionEnter2D (Collision2D collider)
	{

		// we get the gameObject and extract the other componenets. this gets access the the -script- lazers. and all its funtions
		Lazers missile = collider.gameObject.GetComponent<Lazers> ();

		//check if mission exist/ and isnt false value if it didnt have Lazer Component 
		//basically check if there is a script. we made an instance called MIssle 

		if (missile) {

			health -= missile.GetDamage ();

			//calling the hit method.
			missile.Hit ();

			if (health <= 0) {
				Die ();
			}


		}

	}
	void Update ()
	{
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			EnemyFireWeapon ();

		}
	}

	public void EnemyFireWeapon ()
	{


		// first we instantiated then we put it in beam object
		//enemy lazer at transforms poistion
		GameObject enemyFire = (GameObject)Instantiate (enemyLazers, transform.position, Quaternion.identity);
		enemyFire.rigidbody2D.velocity = new Vector2 (0, -enemyLazerSpeed * 0.25f);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void Die ()
	{
				AudioSource.PlayClipAtPoint (deathSound, transform.position);
				//destroy this ship
				Destroy (gameObject);
				scoreKeeper.Score (scoreValue);
	}
}

