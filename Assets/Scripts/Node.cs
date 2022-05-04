using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	Color hoverColor = Color.grey;
	Color startColor;
	Renderer rend;
	public Vector3 rot;
	public Vector2 frontVec;
	public string type;
	GameObject currentGO;

    // Start is called before the first frame update
    void Start()
    {
    	rend = GetComponent<Renderer>(); 
		startColor = rend.material.color;
    }

	void OnMouseDown()
	{
		var infoInstance = Info.instance;
		var shopInstance = Shop.instance;
		var selectedPrefab = shopInstance.selectedPrefab;
	
		if(selectedPrefab == null) 
		{
			Debug.Log("Selecione um objeto");
			return;
		}

		else if(selectedPrefab.tag != type) 
		{
			Debug.Log(selectedPrefab.tag + " Tipo incorreto");
			return;
		}

		else if(infoInstance.money < shopInstance.prefabPrice)
		{
			Debug.Log("Sem dinheiro");
			return;
		}

		if(currentGO != null)
		{
			if(type == "Trap")
			{
				var trap = currentGO.GetComponent<Trap>();
				trap.RIP();
			} 
			else if(type == "turret")
			{
				var turret = currentGO.GetComponent<Turret>();
				turret.RIP();
			}		
		}

		Vector3 position = transform.position;
		GameObject weapon = (GameObject)Instantiate(selectedPrefab, position, Quaternion.Euler(rot.x, rot.y, rot.z));

		currentGO = weapon;
		shopInstance.selectedPrefab = null;
		infoInstance.money -= shopInstance.prefabPrice;

		if(weapon.tag == "turret") 
		{
			var turret = weapon.GetComponent<Turret>();
			turret.SetFrontVec(frontVec);
		}
	}

	void OnMouseEnter()
	{
		rend.material.color = hoverColor;
	}

	void OnMouseExit()
	{
		rend.material.color = startColor;
	}
}
