using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

	public Transform target;
	public int point;
    
    Animator animator;

    [Range(200.0f, 10000.0f)]
    [SerializeField]
    private float attackDistanceThreshold = 1.0f;

    [Header("AudioSources")]
    [SerializeField]
    AudioSource voice;
    [SerializeField]
    AudioSource walkSound;

    [Header("AudioClip")]
    [SerializeField]
    AudioClip headshotSE;

    private NavMeshAgent agent;
    public bool isDead { get; protected set; }
    public bool isCrawl { get; protected set; }
    private Coroutine UpdateCoro;

	private void Start()
	{
		agent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isDead = false;
        UpdateCoro = StartCoroutine(UpdatePath());
	}

    IEnumerator UpdatePath()
    {
        while (target != null)
        {
            // 距離を比較するときは、平方根(Mathf.Sqrt)のコストが高いので、距離の二乗通しを計算することで、パフォーマンスをあげる。
            // 現在のターゲットと自身の距離の二乗。
            float sqrMag = (target.position - transform.position).sqrMagnitude;

            // １秒ウェイト
            yield return new WaitForSeconds(.5f);
            if (sqrMag > attackDistanceThreshold)
            {
                StopWalk();
                continue;
            }
            animator.SetBool("IsWalk", true);
            if(!isCrawl)
                walkSound.Play();
            // ターゲットの中心にまで移動する
            agent.SetDestination(this.target.position);
        }
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void StopWalk()
    {
        animator.SetBool("IsWalk", false);
        walkSound.Stop();
        agent.SetDestination(this.transform.position);
    }

    public void zombieDestroy()
    {
        if (!isCrawl)
            GetComponent<AudioSource>().PlayOneShot(headshotSE);
        isDead = true;
        animator.applyRootMotion = true;
        voice.Stop();
        StopCoroutine(UpdateCoro);
        StopWalk();
        animator.SetBool("IsDead", true);
        StartCoroutine(Destroy());
	}

    public void zombieDamaged()
    {
        agent.speed *= 0.3f; 
        isCrawl = true;
        StopWalk();
        StopCoroutine(UpdateCoro);
        animator.SetBool("IsCrawl", true);
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(1f);
        UpdateCoro = StartCoroutine(UpdatePath());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
