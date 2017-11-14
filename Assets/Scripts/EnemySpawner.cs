using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{


	public GameObject enemyPrefab;

	public float width = 10f;
	public float height = 5f;
	bool isGoingRight = true;
	float spawnDelay = 0.5f;


	private float xMax;
	private float xMin;



	float speed = 5f;

	float padding = 1f;

	// Use this for initialization
	void Start ()
	{


		float distanceFromCamera = transform.position.z - Camera.main.transform.position.z; // distance between camera and the enemy spawner

		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceFromCamera));//to the get the leeft most position
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceFromCamera));

		xMin = leftBoundary.x;
		xMax = rightBoundary.x;

        SpawnUntilFull ();
	}

	//code below repeats, BELOW Is a recursion a method that calls itself
	void SpawnUntilFull ()
	{
		Transform freePosition = NextFreePosition ();
		//only if there is a freeposition
		if (freePosition)
		//this would spawn a single enemy
		{
			GameObject enemy = (GameObject)Instantiate (enemyPrefab, freePosition.transform.position, Quaternion.identity);
			enemy.transform.parent = freePosition;
		}

		//repeating the loop again, but this time with a delay
		//we will creat spawnDelay variable

		//call if only there is a free position
		if (NextFreePosition()) 
		{
			Invoke ("SpawnUntilFull", spawnDelay);
		}
		
	}



	public void OnDrawGizmos ()
	{
		//draws a box around our enemies

		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));

	}


	// Update is called once per frame
	void Update ()
	{

		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);

		if (isGoingRight) {

			transform.position += Vector3.right * speed * Time.deltaTime;

		} else {

			transform.position += Vector3.left * speed * Time.deltaTime;

		}

		float rightEdgeOfFormation = transform.position.x + (0.5f * width); // current position, plus 0.5/ half is centre plus width
		float leftEdgeOfForation = transform.position.x - (0.5f * width);

		if (rightEdgeOfFormation > xMax) {
			isGoingRight = false; ;
		} else if (leftEdgeOfForation < xMin) {
			isGoingRight = true;
		}

		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}

	Transform NextFreePosition ()
	{
		foreach (Transform childPoistion in transform) {
			//when chilposition does not have an enemy attached
			if (childPoistion.childCount == 0) {
				//returning free position in formation
				return childPoistion;
			}
		}
		 return null;
	}
	bool AllMembersDead ()
	{
		
		foreach (Transform childPosition in transform)
		{
			if (childPosition.childCount > 0) 
			{
				return false; 
			
			}
		}
		return true;

	}

	void RespawnEnemies ()
	{ 
		foreach (Transform child in transform) 
		{
			GameObject enemy = (GameObject)Instantiate (enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;
		}
	}
}


