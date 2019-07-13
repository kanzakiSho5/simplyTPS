using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	[SerializeField]
	private int minInterval = 60;
	[SerializeField]
	private int maxInterval = 180;
	[SerializeField]
	private int maxSceneApperance = 10;
	[SerializeField]
	private GameObject spawnObject;
	[SerializeField]
	private Transform targetTransform;

	private int restSpawnFrame = 0;

	int collectZombieCount()
	{
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
		return zombies.Length;
	}

	private void Update()
	{
		if (Time.timeScale <= 0) return;
		this.restSpawnFrame--;
		if(this.restSpawnFrame < 0 && this.collectZombieCount() < this.maxSceneApperance)
		{
			this.restSpawnFrame = (int)Random.Range(this.minInterval, this.maxInterval);
			GameObject tmpObj = Instantiate(spawnObject);
			ZombieController zombie = tmpObj.GetComponent<ZombieController>();
			zombie.target = this.targetTransform;
		}
	}

}
