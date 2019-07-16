using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof (CharacterController))]
public class UnityChanController : MonoBehaviour {

	[SerializeField]
	private float walkSpeed = 3.0f;
	[SerializeField]
	private float runSpeed = 5.0f;
	[SerializeField]
	private float jumpSpeed = 5.0f;
	[SerializeField]
	private int invisibleTime = 60;

    private Camera cam;

    private CharacterController characterController;
	private Animator animator;

	private int restInvisibleTime = 0;

	private void Start()
    {
        print("PlayerController Onstart");
        this.characterController = this.GetComponent<CharacterController>();
		this.animator = this.GetComponent<Animator>();
        this.cam = Camera.main;
	}

	private void Update()
	{

		AnimatorStateInfo animStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if (animStateInfo.IsName("DAMAGE"))
        {
            print("damaged");
            return;
        }
		if (this.restInvisibleTime > 0) this.restInvisibleTime--;

		// 移動入力がない場合、関数を抜ける
		if ( !Input.GetButton("Vertical") && !Input.GetButton("Horizontal")) 
		{
			this.animator.SetFloat("Speed", 0);
			return;
		}

		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");

        
        
        Vector3 forward = this.cam.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = this.cam.transform.right;
        right = right.normalized;

        Vector3 direct = forward * v + right * h;
        direct.Normalize();

		Vector3 walkDirect = direct * this.walkSpeed;
		if(Input.GetButton("RunTrigger"))
		{
			walkDirect = direct * this.runSpeed;
		}

		this.characterController.Move(walkDirect * Time.deltaTime);

        if(!Input.GetMouseButton(1))
		    this.transform.rotation = Quaternion.LookRotation(direct);
            

		float speed = walkDirect.magnitude;
		this.animator.SetFloat("Speed",speed);
	}

	private void OnTriggerEnter(Collider col)
	{
		if (Time.timeScale <= 0)
			return;
		if (col.tag == "Enemy" && this.restInvisibleTime <= 0)
		{
            if (col.GetComponent<ZombieController>().isDead == true)
                return;
			this.animator.SetBool("IsDamage", true);
			this.restInvisibleTime = this.invisibleTime;
		}
	}

	public void ComebackEvent()
	{
		this.animator.SetBool("IsDamage", false);
	}

}
