using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {


    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        Application.LoadLevel("Game");
    }
}
