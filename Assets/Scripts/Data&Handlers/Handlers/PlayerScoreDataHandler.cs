using UnityEngine;

public class PlayerScoreDataHandler
{
	public int MaxScore { get; private set; }

	private readonly string _maxScoreName = "MaxScore";

	public PlayerScoreDataHandler()
	{
		LoadScore();
	}

	private void LoadScore()
	{
		MaxScore = PlayerPrefs.GetInt(_maxScoreName, 0);
	}

	public void SaveScore(int maxScore)
	{
		MaxScore = maxScore;
		PlayerPrefs.SetInt(_maxScoreName, maxScore);
	}
}
