using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void Taman()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Kota()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
