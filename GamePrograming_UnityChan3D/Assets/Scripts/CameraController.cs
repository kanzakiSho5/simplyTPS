using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private Transform LookPoint;
	[SerializeField]
	private float LimitRotateY = 45f;
    [SerializeField]
	private Transform CameraPos;

    [Header("Camera")]
    [SerializeField]
    private GameObject ADSRightPos;
	private float yRot;
	private float xRot;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{

        if (Time.timeScale <= 0) return;
        if (Input.GetMouseButton(1))
        {
            ADSRightPos.SetActive(true);
            this.yRot += Input.GetAxisRaw("Mouse X");
            this.xRot += Input.GetAxisRaw("Mouse Y");
            xRot = Mathf.Clamp(xRot, -LimitRotateY, LimitRotateY);

            this.transform.position = this.LookPoint.position;

            ADSRightPos.transform.localEulerAngles = new Vector3(-this.xRot, 0, 0);
            ADSRightPos.transform.parent.localEulerAngles = new Vector3(0, this.yRot, 0);
            return;
        }

        ADSRightPos.SetActive(false);

		if(this.LookPoint)
		{
			this.yRot += Input.GetAxisRaw("Mouse X");
			this.xRot += Input.GetAxisRaw("Mouse Y");
			xRot = Mathf.Clamp(xRot, -LimitRotateY, LimitRotateY);

			this.transform.position = this.LookPoint.position;
			
			this.transform.localEulerAngles = new Vector3(-this.xRot, this.yRot, 0);
			this.transform.LookAt(this.LookPoint);
		}

		
	}


}
