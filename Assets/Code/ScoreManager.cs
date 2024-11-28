using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public Text scoreText;
    public static int scoreCount;
    public LeaderboardManager leaderboard;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + Mathf.Round(scoreCount);
    }

    // Panggil metode ini saat permainan berakhir
    public void GameOver()
    {
        leaderboard.SubmitScoreRoutine(scoreCount);
    }

    // Metode untuk mendapatkan nilai skor saat permainan berakhir
    public int GetFinalScore()
    {
        return scoreCount;
    }

    public void UpdateScore(int newScore)
    {
        scoreCount = newScore;
    }
}
