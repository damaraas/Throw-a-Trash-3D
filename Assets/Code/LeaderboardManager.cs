using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    int leaderboardID = 19202;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    public Text Score;
    private bool isSubmittingScore = false;
    private ScoreManager scoreManager; // Reference to ScoreManager script

    void Start()
    {
        // Get reference to ScoreManager script
        scoreManager = FindObjectOfType<ScoreManager>();

        // Pastikan objek ini aktif dan tidak dihancurkan
        if (gameObject.activeSelf && scoreManager != null)
        {
            // Use scoreManager.GetFinalScore() to get the current score dynamically
            StartCoroutine(SubmitScoreRoutine(scoreManager.GetFinalScore()));
        }
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        // Pastikan coroutine tidak sedang berjalan sebelum memulai
        if (isSubmittingScore)
        {
            yield break; // Keluar dari coroutine jika sedang berjalan
        }

        isSubmittingScore = true; // Setel flag bahwa coroutine sedang berjalan

        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        string leaderboardIDString = leaderboardID.ToString();
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardIDString, (response) =>
        {
            Debug.Log("Player ID: " + playerID);
            Debug.Log("Score to Upload: " + scoreToUpload);
            Debug.Log("Leaderboard ID: " + leaderboardIDString);

            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
        isSubmittingScore = false; // Setel flag bahwa coroutine sudah selesai
    }

    public IEnumerator FetchTopHighScoresRoutine()
    {
        bool done = false;
        string leaderboardIDString = leaderboardID.ToString();
        LootLockerSDKManager.GetScoreList(leaderboardIDString, 7, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Nama\n";
                string tempPlayerScores = "Score\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
