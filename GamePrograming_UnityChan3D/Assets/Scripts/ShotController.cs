using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotController : MonoBehaviour {

	private Animator animator;

	[SerializeField]
	private int shotInterval = 10;
	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private Transform shotPoint;
    [SerializeField]
    private int slotLength = 30;
    [SerializeField]
    private float bullelUpValue = 0.5f;
    [SerializeField]
    private Text bulletText;

    private int currentSlot = 0;
	private int restShotTime = 0;
    private float bulleldir = 0;

    private float currentIntervalTime = 0;

    Vector3 dir;

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

        if(Input.GetKeyDown(KeyCode.R))
        {
            currentSlot = slotLength;
        }

		if (Input.GetMouseButton(0))
		{
            currentIntervalTime = 0;
            bulleldir += 0.01f;
            bulleldir = bulleldir <= 1f ? bulleldir : 1f;
            var currentbulleldir = Mathf.Lerp(0, bullelUpValue, bulleldir);
			this.animator.SetLayerWeight(1, 1.0f);

			this.restShotTime--;
			if(this.restShotTime <= 0 && currentSlot > 0)
			{
                Debug.Log(bulleldir);
				this.restShotTime = this.shotInterval;
                
				GameObject tmpBullet = Instantiate(this.bullet);
				tmpBullet.transform.position = this.shotPoint.position;
				BulletController bCon = tmpBullet.GetComponent<BulletController>();
                var UperBullet = Vector3.up * currentbulleldir;
                var HorizontalRand = Vector3.right * Random.Range(-bulleldir, bulleldir) * 0.1f;


                dir = Vector3.zero;
                if (Input.GetMouseButton(1))
                {
                    dir = (Camera.main.transform.forward + UperBullet + HorizontalRand).normalized;
                }
                else
                {
                    dir = (transform.forward + UperBullet + HorizontalRand).normalized;
                }
                RaycastHit hit;
                if (Physics.Raycast(this.shotPoint.position, dir, out hit))
                    if (hit.collider.tag == "Enemy")
                        OnDamagedEnemy(hit.collider);

                bCon.Shot(dir);
                currentSlot--;
			}
		}
		else
		{
            currentIntervalTime += Time.deltaTime;
			this.animator.SetLayerWeight(1, 0.0f);
		}

        bulletText.text = currentSlot.ToString();

        if (currentSlot >= 10)
            bulletText.color = Color.white;
        else
            bulletText.color = Color.red;

        if (bulleldir >= 0)
            bulleldir -= currentIntervalTime * 0.1f;
	}

    private void OnDamagedEnemy(Collider col)
    {
        ZombieController zombie = col.GetComponent<ZombieController>();
        if (zombie)
        {
            ScoreManager.Instance.addScore(zombie.point);
            Text pointText = GameObject.Find("Score").GetComponent<Text>();
            pointText.text = "Point: " + ScoreManager.Instance.getCurrentScore();
            zombie.zombieDestroy();
        }
        Destroy(col.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.shotPoint.position, dir);
    }
}
