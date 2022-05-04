using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public GameObject selectedPrefab;
	public float prefabPrice;
	public static Shop instance;

	void Awake()
	{
		instance = this;
	}
}
