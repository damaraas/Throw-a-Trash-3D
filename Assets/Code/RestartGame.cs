using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void Taman()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        scoreManager.UpdateScore(0);
    }

    public void Kota()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
        scoreManager.UpdateScore(0);
    }
}
