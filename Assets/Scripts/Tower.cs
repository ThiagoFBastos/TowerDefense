using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public int resistencia = 100;
	public bool is_final_tower = false;
	public float treasure = 2.0f;
	public static int count_final_towers = 0;

    // Start is called before the first frame update
    void Awake()
    {
     	if(is_final_tower) ++count_final_towers;
    }

	void OnDestroy()
	{
		Info.instance.money -= treasure;
	}

	public void CausaDano(int dano)
	{
		Info instance = Info.instance;
		if(!instance.running) return;
		resistencia -= dano;
		if(resistencia <= 0)
		{
			if(is_final_tower) --count_final_towers;
			Destroy(gameObject);
			if(count_final_towers == 0) instance.SetGameOver();
		}
	}
}
