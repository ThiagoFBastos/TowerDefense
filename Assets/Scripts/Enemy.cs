using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject explosion;
	public float payment = 1.0f;
	public float healthy = 1.0f;
	public int damage;
	public float speed = 2.5f;
	Stack<int> path;
	int curNode = 0;
	Rigidbody m_rigidbody;

	void Awake()
	{
		var traps = GameObject.Find("traps");
		var tr = traps.transform;
		var collision = GetComponent<Collider>();
		for(int i = 0; i < tr.childCount; ++i)
		{	
			var child = tr.GetChild(i).gameObject;
			Physics.IgnoreCollision(collision, child.GetComponent<Collider>());
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		m_rigidbody = GetComponent<Rigidbody>();
     	path = new Stack<int>();
    }

	public void TryKill(float damage, bool pay = true)
	{
		healthy -= damage;
		if(healthy <= 0)
		{
			if(pay) Info.instance.money += payment;
			RIP();
		}
	}

	void findPath(int src) 
	{
		WayPoint way = WayPoint.myWayPoint;
		int n = way.cntNodes;

		if(curNode == n) return;
		
		int[] pai = new int[n];
		Queue<int> q = new Queue<int>();

		path.Clear();

		for(int i = 0; i < n; ++i)
			pai[i] = -1;

		pai[src] = src;
		q.Enqueue(src);		

		while(q.Count > 0)
		{
			int u = q.Dequeue();

			if(way.towers[u] != null)
			{
				for(; u != src; u = pai[u]) path.Push(u);
				break;
			}

			foreach(int v in way.graph[u])
			{
				if(pai[v] != -1) continue;
				pai[v] = u;
				q.Enqueue(v);
			}
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(healthy <= 0) return;

		WayPoint way = WayPoint.myWayPoint;
	
		if(path.Count == 0) {
			findPath(curNode);
			if(path.Count == 0)
			{
				TryKill(1000, false);
				return;
			}
			curNode = path.Peek();
		}

		Transform target = way.points[curNode];
		Vector3 upd = target.position - transform.position;

		m_rigidbody.MovePosition(transform.position + upd.normalized * speed * Time.deltaTime);

		if(Vector3.Distance(target.position, transform.position) <= 0.08) {
			int u = path.Pop();
			if(path.Count > 0) curNode = path.Peek();
		}
    }

	void RIP()
	{
		GameObject exp = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
		Destroy(exp, 1.0f);
		Destroy(gameObject);
	}


	void OnCollisionEnter(Collision collision)
	{
		var enemy = collision.gameObject;
		if(enemy.tag != "Tower" || healthy <= 0) return;
		var tower = enemy.GetComponent<Tower>();
		if(tower.resistencia <= 0) return;
		tower.CausaDano(damage);
		TryKill(1000, false);
	}
}
