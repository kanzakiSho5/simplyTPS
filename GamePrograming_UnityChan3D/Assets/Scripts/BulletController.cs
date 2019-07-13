using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour {

	[SerializeField]
	private float life = 120;

	[SerializeField]
	private float speed = 3.0f;

	private Rigidbody rigid;

	private void OnEnable()
	{
		this.rigid = this.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Time.timeScale <= 0) return;

		this.life--;
		if(this.life < 0)
			Destroy(this.gameObject);
	}

	private void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			ZombieController zombie = col.GetComponent<ZombieController>();
			if(zombie)
			{
				ScoreManager.Instance.addScore(zombie.point);
				Text pointText = GameObject.Find("Score").GetComponent<Text>();
				pointText.text = "Point: " + ScoreManager.Instance.getCurrentScore();
				zombie.zombieDestroy();
			}
			Destroy(col.gameObject);
			Destroy(this.gameObject);
		}
	}

	public void Shot(Vector3 direct)
	{
		this.rigid.velocity = direct * this.speed;
	}
}
