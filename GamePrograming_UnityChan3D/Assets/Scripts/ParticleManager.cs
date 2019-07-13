using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour 
{
	private ParticleSystem[] particleSystems;

	private void Start()
	{
		particleSystems = GetComponentsInChildren<ParticleSystem>();
	}

	private void Update()
	{
		bool isAlive = false;
		foreach(ParticleSystem ps in particleSystems)
		{
			if(ps.IsAlive())
			{
				isAlive = true;
			}
		}

		if (!isAlive)
			Destroy(gameObject);
	}

}
