using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

	public Transform target;
	public int point;

	[SerializeField]
	private GameObject explosion;

	private NavMeshAgent agent;

	private void Start()
	{
		agent = this.GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		agent.SetDestination(this.target.position);
	}

	public void zombieDestroy()
	{
		Instantiate(explosion, this.transform.position, Quaternion.identity);
	}
}
