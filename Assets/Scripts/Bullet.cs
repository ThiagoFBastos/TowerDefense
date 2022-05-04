using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

	GameObject target;
	float speed = 20f;
	Vector3 start_position;
	public int damage;
	
	public GameObject Target 
	{
		set
		{
			target = value;
		}
		get 
		{
			return target;
		}
	}

	public int Damage
	{
		set
		{
			damage = value;
		}
	}

    void Start()
    {
		if(target == null) return;
        start_position = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
     	if(target == null) {
			Destroy(gameObject);
			return;
		}

		var dir = start_position - transform.position;

		float curDistance = speed * Time.deltaTime;

		transform.Translate(dir.normalized * curDistance, Space.World);

		if(dir.magnitude <= curDistance)
		{
			var enemy = target.GetComponent<Enemy>();
			enemy.TryKill(damage);
			Destroy(gameObject);
			return;
		}
    }
}
