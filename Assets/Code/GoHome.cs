using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{
    // Start is called before the first frame update
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
