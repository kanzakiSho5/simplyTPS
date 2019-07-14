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
    [Range(1.0f,200.0f)]
    private float maxFieldSize = 10.0f;
    [SerializeField]
    private List<SpawnField> spawnFields = new List<SpawnField>();

	[SerializeField]
	private GameObject spawnObject;
	[SerializeField]
	private Transform targetTransform;

	private int restSpawnFrame = 0;
    private int counter = 0;

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
            var spawnField = spawnFields[counter];
			this.restSpawnFrame = (int)Random.Range(this.minInterval, this.maxInterval);
            Vector3 spawnPos = new Vector3(
                Random.Range(-spawnField.size.x /2, spawnField.size.x / 2), 
                0, 
                Random.Range(-spawnField.size.z / 2, spawnField.size.z / 2));
            GameObject tmpObj = Instantiate(spawnObject, spawnField.center + spawnPos + this.transform.position, Quaternion.identity, this.transform);
			ZombieController zombie = tmpObj.GetComponent<ZombieController>();
			zombie.target = this.targetTransform;

            counter++;
            counter %= spawnFields.Count;
		}
	}
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0.5f, 0.5f);
        if (spawnFields == null) return;
        foreach(var sf in spawnFields)
        {
            Gizmos.DrawCube(sf.center + transform.position, sf.size);
        }
    }
    
}

[System.Serializable]
public class SpawnField
{
    [SerializeField]
    public Vector3 center;
    [SerializeField]
    public Vector3 size;
}
