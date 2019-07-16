using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private GameObject ScopeCam;

    [Header("UIs")]
    [SerializeField]
    private Image crosshairUI;

    [Header("ScopeSprite")]
    [SerializeField]
    private Sprite scopeSprite;
    [SerializeField]
    private Sprite crosshairSprite;

	private float yRot;
	private float xRot;

    public int wepon { get; protected set; }

	private void Start()
    {
        print("CameraController Onstart");
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        wepon = 0;
	}

	private void Update()
	{
        //print(Input.GetAxis("Mouse ScrollWheel"));
        wepon -= (int)(Input.GetAxis("Mouse ScrollWheel") * 100);
        wepon = wepon <= 0 ? 0 : 1;

        if (Time.timeScale <= 0) return;
        if (Input.GetMouseButton(1))
        {
            crosshairUI.gameObject.SetActive(true);

            this.yRot += Input.GetAxisRaw("Mouse X") * 0.5f;
            this.xRot += Input.GetAxisRaw("Mouse Y") * 0.5f;
            xRot = Mathf.Clamp(xRot, -LimitRotateY, LimitRotateY);

            this.transform.position = this.LookPoint.position;
            
            if(wepon == 0)
            {
                crosshairUI.sprite = crosshairSprite;
                ADSRightPos.SetActive(true);
                ScopeCam.SetActive(false);
                ADSRightPos.transform.localEulerAngles = new Vector3(-this.xRot, 0, 0);
                ADSRightPos.transform.parent.localEulerAngles = new Vector3(0, this.yRot, 0);
            }
            else
            {
                crosshairUI.sprite = scopeSprite;
                ScopeCam.SetActive(true);
                ADSRightPos.SetActive(false);
                ScopeCam.transform.localEulerAngles = new Vector3(-this.xRot, 0, 0);
                ScopeCam.transform.parent.localEulerAngles = new Vector3(0, this.yRot, 0);
            }
            return;
        }
        else
        {
            crosshairUI.gameObject.SetActive(false);
        }

        ADSRightPos.SetActive(false);
        ScopeCam.SetActive(false);

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
