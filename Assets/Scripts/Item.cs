using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
	public GameObject prefab;
	public float price = 0.0f;

	public void onClick()
	{
		if(!Info.instance.running) return;
		var instance = Shop.instance;
		instance.selectedPrefab = prefab;
		instance.prefabPrice = price;		
	}
}
