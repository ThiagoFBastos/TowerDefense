using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Transform[] points;
	public Transform towerTransform;
	public static WayPoint myWayPoint;
	public List<List<int>> graph;
	public List<GameObject> towers;
	public string graphFileName;
	public int cntNodes;

    // Start is called before the first frame update
    void Start()
    {
        using(StreamReader file = new StreamReader(Application.dataPath + "/Graphs/" + graphFileName))
		{
			int n = int.Parse(file.ReadLine());
			graph = new List<List<int>>(n);
			towers = new List<GameObject>(n);

			cntNodes = n;

			for(int i = 0; i < n; ++i)
			{
				string[] adj = file.ReadLine().Split(' ');
				graph.Add(new List<int>());
				foreach(string s in adj)
					graph[i].Add(int.Parse(s));
			}

			string[] types = file.ReadLine().Split(' ');

			for(int i = 0, j = 0; i < n; ++i) 
			{
				if(types[i] == "1") 
				{
					towers.Add(towerTransform.transform.GetChild(j).gameObject);	
					++j;
				} else
					towers.Add(null);
			}
		}
    }

    void Awake()
    {
		myWayPoint = this;
        points = new Transform[transform.childCount];

        for(int i = 0; i < points.Length; ++i)
            points[i] = transform.GetChild(i);
    }
}
