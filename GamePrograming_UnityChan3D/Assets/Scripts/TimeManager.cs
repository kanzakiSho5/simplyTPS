using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

	private Text timeText;

	public float gameTime = 60;

	private float restTime;
	private bool isPlaying = true;

	public Text highScoreText;

	private string getRestTimeText()
	{
		int integer = Mathf.FloorToInt(this.restTime);
		return integer.ToString("00") + ":" + Mathf.FloorToInt((this.restTime - integer) * 100).ToString("00");
	}

	private void Start()
    {
        Time.timeScale = 1.0f;
        print("TimeManager Onstart");
        this.isPlaying = true;
		this.restTime = this.gameTime;
		this.timeText = this.GetComponent<Text>();
		this.timeText.text = getRestTimeText();
	}

	private void Update()
	{
		this.restTime -= Time.deltaTime;
		if (this.restTime > 0)
		{
			this.timeText.text = getRestTimeText();
		}
		else
		{
			this.isPlaying = false;
			Time.timeScale = 0.0f;

			ScoreManager scoreManager = ScoreManager.Instance;
			scoreManager.compareHighScore();
			this.highScoreText.gameObject.SetActive(true);
			this.highScoreText.text = "HighScore: " + scoreManager.getHighScore();
			scoreManager.initializeCurrentScore();

			if(Input.GetKeyDown(KeyCode.N))
			{
				Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadSceneAsync(0);
                
			}

		}
	}
}
