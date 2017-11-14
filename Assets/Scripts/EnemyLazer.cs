using UnityEngine;
using System.Collections;

public class EnemyLazer : MonoBehaviour {
	
public float enemyDamage = 100f;

//get damage method so that other methods cant efect the float damage./


public float GetEnemyDamage ()
{
	return enemyDamage;
}

public void Hit ()
{
	//destroy lazer/ destroy itself
	Destroy (gameObject);
}
}