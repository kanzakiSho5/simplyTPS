using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour {

	private static ScoreManager mInstance;
	private int currentScore = 0;
	private int highScore = 10;

	public static ScoreManager Instance
	{
		get
		{
			if(mInstance == null)
			{
				Debug.Log("Created ScoreManager");
				mInstance = new GameObject("ScoreManager").AddComponent<ScoreManager>();
			}
			return mInstance;
		}
		set { }
	}

    private void OnDisable()
    {
        Debug.Log("Destroy ScoreManager");
        mInstance = null;
    }

    public void initializeCurrentScore()
	{
		this.currentScore = 0;
	}

	public void addScore(int point)
	{
		this.currentScore += point;
		//Debug.Log(this.currentScore);
	}

	public int getCurrentScore()
	{
		return this.currentScore;
	}

	public int getHighScore()
	{
		return this.highScore;
	}

	public void compareHighScore()
	{
		if (this.currentScore > this.highScore)
			this.highScore = this.currentScore;
	}
}
