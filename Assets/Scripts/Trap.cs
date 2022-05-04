using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	public int damage = 100;

	public void RIP()
	{
		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
		var enemy = collision.gameObject;

		if(enemy.tag != "Enemy") 
		{
			Debug.Log("Não é inimigo");
			return;
		}

		var script = enemy.GetComponent<Enemy>();

		script.TryKill(damage);
		Destroy(gameObject);
	}
}
