using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScreen : MonoBehaviour {

    public Texture playButtonTexture;
    public Texture menuButtonTexture;
    public Text scoreInfo;
    public Text bestScoreText;

    void Start()
    {
        //Set the score text.
        GameInfo gameInfo = GameObject.Find("Viewbag").GetComponent<GameInfo>();
        int score = gameInfo.score;
        gameInfo.CheckHighScore(score);//See if this is the new high score.
        scoreInfo.text = "You caused $" + score + " in hospital fees";

        //Display best score.
        if(gameInfo.score == gameInfo.bestScore)
        {
            bestScoreText.text = "new best, congrats";
        }
        else
        {
            bestScoreText.text = "best: $" + gameInfo.bestScore;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
        }
    }

    public void GoToGame()
    {
        Application.LoadLevel("Game");
    }


    public void GoToMenu()
    {
        Destroy(GameObject.Find("Viewbag"));
        Application.LoadLevel("Title");
    }
}
