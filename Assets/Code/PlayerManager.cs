using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public LeaderboardManager leaderboard;
    public TMP_InputField inputName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        if (inputName != null)
        {
            Debug.Log("Input Name: " + inputName.text);

            LootLockerSDKManager.SetPlayerName(inputName.text, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully set player name");
                    StartCoroutine(leaderboard.FetchTopHighScoresRoutine());
                }
                else
                {
                    Debug.Log("Could not set player name");
                }
            });
        }
        else
        {
            Debug.LogWarning("Input Name is null!");
        }
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighScoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
