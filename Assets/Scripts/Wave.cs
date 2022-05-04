using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
	public Transform smallKamikaze;
	public Transform largeKamikaze;
	public Transform superKamikaze;
	public Transform spawnPoint;
	const float timeBetweenWaves = 15f;
	float countDown = 0.0f;
	int wave = 1, loop = 1;

	void SpawnWaves(Transform enemy)
    {
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

	void Update()
	{
		Info instance = Info.instance;
		if(!instance.running) return;

		if(countDown <= 0) 
		{
			if(loop == 128)
			{
				SpawnWaves(superKamikaze);
				loop = 0;
			}
			else if(loop % 8 == 0) SpawnWaves(largeKamikaze);	
			else
			{
				SpawnWaves(smallKamikaze);
				++wave;
				if(wave > 20) wave = 20;
			}
			++loop;
			countDown = timeBetweenWaves / wave;
		}

		countDown -= Time.deltaTime;
    }
}
