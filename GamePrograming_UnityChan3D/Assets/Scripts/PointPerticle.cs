using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointPerticle : MonoBehaviour {

    private Text text;
    private void OnEnable()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        transform.position += Vector3.up;
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.01f);
        if (text.color.a <= 0)
            Destroy(gameObject);
    }

    public void GetPoint(int point)
    {
        text.text = "+ " + point;
    }

}
