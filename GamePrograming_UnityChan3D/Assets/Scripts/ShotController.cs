using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	private Animator animator;

	[SerializeField]
	private int shotInterval = 10;
	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private Transform shotPoint;
	private int restShotTime = 0;

	private void Start()
	{
		this.animator = this.GetComponent<Animator>();
	}

	private void Update()
	{
		if (Time.timeScale <= 0) return;

		AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);

		if (animatorStateInfo.IsName("DAMAGE"))
		{
			this.animator.SetLayerWeight(1, 0.0f);
			return;
		}

		if (Input.GetMouseButton(0))
		{
			this.animator.SetLayerWeight(1, 1.0f);

			this.restShotTime--;
			if(this.restShotTime <= 0)
			{
				this.restShotTime = this.shotInterval;
				GameObject tmpBullet = Instantiate(this.bullet);
				tmpBullet.transform.position = this.shotPoint.position;
				BulletController bCon = tmpBullet.GetComponent<BulletController>();
				bCon.Shot(this.transform.forward);
			}
		}
		else
		{
			this.animator.SetLayerWeight(1, 0.0f);
		}
	}
}
