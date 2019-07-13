using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private Transform LookPoint;
	[SerializeField]
	private float LimitRotateY = 45f;

	private Transform CameraPos;
	private float yRot;
	private float xRot;

	private void Start()
	{
		CameraPos = Camera.main.transform;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		if (Time.timeScale <= 0) return;
		if(this.LookPoint)
		{
			this.yRot += Input.GetAxisRaw("Mouse X");
			this.xRot += Input.GetAxisRaw("Mouse Y");
			xRot = Mathf.Clamp(xRot, -LimitRotateY, LimitRotateY);

			this.transform.position = this.LookPoint.position;
			
			this.transform.localEulerAngles = new Vector3(this.xRot, this.yRot, 0);
			this.transform.LookAt(this.LookPoint);
		}

		
	}


}
