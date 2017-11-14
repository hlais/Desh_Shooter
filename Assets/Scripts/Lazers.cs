using UnityEngine;
using System.Collections;

public class Lazers : MonoBehaviour
{ // this will juust tell anything this is a projectile, and only to take effect when it has projectile 
	public float damage = 100f;


	//get damage method so that other methods cant efect the float damage./


	public float GetDamage ()
	{
		return damage;
	}

	public void Hit ()
	{
		//destroy lazer/ destroy itself
		Destroy (gameObject);
	}
}