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
    [SerializeField]
    private GameObject PointText;

    [Header("Sound Effects")]
    [SerializeField]
    private AudioClip gunReloadSE;
    [SerializeField]
    private AudioClip gunShotSE;

    [Header("Classes")]
    [SerializeField]
    private CameraController cameraCon;
    [SerializeField]
    private AudioSource source;

    

    private int currentSlot = 0;
	private int restShotTime = 0;
    private float bulleldir = 0;
    private bool isReloading = true;

    private float currentIntervalTime = 0;

    Vector3 dir;

    private void Start()
	{
		this.animator = this.GetComponent<Animator>();
        StartCoroutine(Reload());
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

        if (isReloading) return;

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

		if (Input.GetMouseButton(0))
		{
            currentIntervalTime = 0;
            bulleldir = bulleldir <= 1f ? bulleldir : 1f;
            var currentbulleldir = Mathf.Lerp(0, bullelUpValue, bulleldir);
			this.animator.SetLayerWeight(1, 1.0f);

			this.restShotTime--;
			if(this.restShotTime <= 0 && currentSlot > 0)
			{
                //Debug.Log(bulleldir);
				this.restShotTime = this.shotInterval;
                
				GameObject tmpBullet = Instantiate(this.bullet);
				tmpBullet.transform.position = this.shotPoint.position;
				BulletController bCon = tmpBullet.GetComponent<BulletController>();
                var UperBullet = Vector3.up * currentbulleldir;
                var HorizontalRand = Vector3.right * Random.Range(-bulleldir, bulleldir) * .1f;
                var lookdir = Camera.main.transform.forward;

                dir = Vector3.zero;
                if (Input.GetMouseButton(1))
                {
                    // Scopeモードじゃない場合距離によって照準をずらす
                    if(cameraCon.wepon <= 0)
                    {
                        RaycastHit cameraHit;
                        if (Physics.Raycast(Camera.main.transform.position, lookdir, out cameraHit))
                            lookdir = (cameraHit.point - shotPoint.position).normalized;
                        
                    }
                    dir = (lookdir + UperBullet + HorizontalRand).normalized;
                }
                else
                {
                    dir = (transform.forward + UperBullet + HorizontalRand).normalized;
                }
                RaycastHit hit;
                if (Physics.Raycast(this.shotPoint.position, dir, out hit))
                {
                    
                    if (hit.collider.tag == "Enemy")
                    {
                        Debug.Log(hit.collider.name);
                        if (hit.collider.name == "Head")
                            OnHeadShotEnemy(hit.collider);
                        else
                            OnDamagedEnemy(hit.collider);
                    }
                }

                bCon.Shot(dir);
                source.PlayOneShot(gunShotSE);

                currentSlot--;
                UpdateBulletCount();
                bulleldir += 0.01f * shotInterval;

                if (cameraCon.wepon == 1)
                {
                    StartCoroutine(Reload());
                }
			}

            
        }
		else
		{
            currentIntervalTime += Time.deltaTime;
			this.animator.SetLayerWeight(1, 0.0f);
		}
        
        
        bulleldir -= currentIntervalTime * 0.1f;

        if(bulleldir <= 0)
            bulleldir = 0;
	}

    private void UpdateBulletCount()
    {
        bulletText.text = currentSlot.ToString();
        if (currentSlot >= 10)
            bulletText.color = Color.white;
        else
            bulletText.color = Color.red;

    }

    private void OnHeadShotEnemy(Collider col)
    {
        ZombieController zombie = col.transform.parent.GetComponent<ZombieController>();
        if(!zombie)
            zombie = col.transform.GetComponent<ZombieController>();
        if (zombie)
        {
            if (zombie.isDead) return;
            var point = zombie.point;
            if (!zombie.isCrawl)
                point *= 4;
            ScoreManager.Instance.addScore(point);
            Text pointText = GameObject.Find("Score").GetComponent<Text>();
            pointText.text = "Point: " + ScoreManager.Instance.getCurrentScore();
            Instantiate(PointText, pointText.transform.parent).GetComponent<PointPerticle>().GetPoint(point);
            zombie.zombieDestroy();
        }
        //Destroy(col.gameObject);
    }

    private void OnDamagedEnemy(Collider col)
    {
        ZombieController zombie = col.transform.GetComponent<ZombieController>();
        if (zombie)
        {
            if (zombie.isDead) return;
            if (zombie.isCrawl)
            {
                OnHeadShotEnemy(col);
                return;
            }
            ScoreManager.Instance.addScore(zombie.point);
            Text pointText = GameObject.Find("Score").GetComponent<Text>();
            pointText.text = "Point: " + ScoreManager.Instance.getCurrentScore();
            Instantiate(PointText, pointText.transform.parent).GetComponent<PointPerticle>().GetPoint(zombie.point);
            zombie.zombieDamaged();
        }
    }

    private IEnumerator Reload()
    {
        source.PlayOneShot(gunReloadSE);
        isReloading = true;
        yield return new WaitForSeconds(.5f);
        currentSlot = slotLength;
        isReloading = false;
        UpdateBulletCount();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.shotPoint.position, dir);
    }
}
