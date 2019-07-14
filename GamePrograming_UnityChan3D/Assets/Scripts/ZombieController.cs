using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

	public Transform target;
	public int point;

    float myCollisionRadius;
    float targetCollisionRadius;
    Animator animator;
    [SerializeField]
    AudioSource source;

    [SerializeField]
	private GameObject explosion;
    [Range(200.0f, 10000.0f)]
    [SerializeField]
    private float attackDistanceThreshold = 1.0f;

    private NavMeshAgent agent;

	private void Start()
	{
		agent = this.GetComponent<NavMeshAgent>();
        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        animator = GetComponent<Animator>();

        targetCollisionRadius = target.GetComponent<CharacterController>().radius;
        StartCoroutine(UpdatePath());
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
                animator.SetBool("IsWalk", false);
                source.Stop();
                agent.SetDestination(this.transform.position);
                continue;
            }
            animator.SetBool("IsWalk", true);
            source.Play();
            // ターゲットの中心にまで移動する
            agent.SetDestination(this.target.position);
        }
    }

    public void zombieDestroy()
	{
		Instantiate(explosion, this.transform.position, Quaternion.identity);
	}
}
