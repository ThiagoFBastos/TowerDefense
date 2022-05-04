using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    // Start is called before the first frame update

	public GameObject turret_to_build;
	public GameObject prefab;
	public static Builder builder;

	public GameObject TurretToBuild 
	{
		get
		{
			return turret_to_build;
		}
	}

	void Awake() 
	{
		if(builder != null) return;
		builder = this;
	}

    void Start()
    {
     	turret_to_build = prefab;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
