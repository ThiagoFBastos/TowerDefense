using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	GameObject target;	
	public Transform firePoint;
	public Transform firePointL;
	public Transform firePointR;
	public GameObject lineRenderer;
	public bool useLaser = false;
	public int damage = 0;
	public float radius = 8f;
	public GameObject bullet;
	public float fireRate = 2.5f;
	float fireCountDown = 0;
	LineRenderer lineRendererL;
	LineRenderer lineRendererR;
	Vector2 frontVec;
	double frontDegree = Math.PI / 12.0;
	
	public void RIP()
	{
		if(useLaser)
		{
			Destroy(lineRendererL);
			Destroy(lineRendererR);
		}
		Destroy(gameObject);
	}

	public void SetFrontVec(Vector2 frontVec) 
	{
		this.frontVec = frontVec;
	}

    // Start is called before the first frame update
    void Start()
    {
		if(useLaser)
		{
			var L = (GameObject)Instantiate(lineRenderer, firePointL.position, firePointL.rotation);
			var R = (GameObject)Instantiate(lineRenderer, firePointR.position, firePointR.rotation);
			lineRendererL = L.GetComponent<LineRenderer>();
			lineRendererR = R.GetComponent<LineRenderer>();
			lineRendererL.enabled = lineRendererR.enabled = false;
		}

     	InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

	bool CanShot(GameObject enemy)
	{
		Vector3 dir = enemy.transform.position - transform.position;
		Vector2 XZ = new Vector2(dir.x, dir.z);
		float dist = XZ.sqrMagnitude;
		double degree = Math.Acos(Vector2.Dot(XZ.normalized, frontVec));
		return degree <= frontDegree && dist <= radius;
	}

	float Distance(GameObject enemy)
	{
		Vector3 dir = enemy.transform.position - transform.position;
		return (new Vector2(dir.x, dir.z)).sqrMagnitude;
	}

	void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject enemySelected = null;

        float best_distance = float.MaxValue;

        foreach(var enemy in enemies)
        {
			float dist = Distance(enemy);
			if(dist < best_distance && CanShot(enemy))
			{
				best_distance = dist;
				enemySelected = enemy;
			}
        }

       target = enemySelected;
    }

    void Update()
    {
		
        if(target == null) {
			if(useLaser) {
				lineRendererL.enabled = false;
				lineRendererR.enabled = false;	
			}
			return;
		}

		if(fireCountDown <= 0)
		{
			if(useLaser) Laser();
			else Shot();
			fireCountDown = 1f / fireRate;
		}

		fireCountDown -= Time.deltaTime;
    }

	void Laser()
	{
		var enemy = target.GetComponent<Enemy>();
		enemy.TryKill(damage);
		if(!lineRendererL.enabled) lineRendererL.enabled = true;
		if(!lineRendererR.enabled) lineRendererR.enabled = true;
		lineRendererL.SetPosition(0, firePointL.position);
		lineRendererL.SetPosition(1, target.transform.position);
		lineRendererR.SetPosition(0, firePointR.position);
		lineRendererR.SetPosition(1, target.transform.position);
	}

	void Shot()
	{
		if(bullet == null) return;

		var ball = ((GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation)).GetComponent<Bullet>();
		
		if(ball == null) return;
		
		ball.Target = target;
		ball.Damage = damage;
	}
}
