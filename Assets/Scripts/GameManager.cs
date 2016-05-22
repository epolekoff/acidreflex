using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    int score = 0;
    int MAX_CALORIES = 50;
    int caloriesEaten = 0;

    bool gamePaused = false;
    public Canvas pauseCanvas;
    
    public Text scoreText;
    public Text calorieText;
    public Text tutorialText;

    public Texture playButtonTexture;
    public Texture menuButtonTexture;
    float buttonWidth;
    float buttonHeight;
    float pauseButtonWidth;
    float pauseButtonHeight;
    float virtualResolutionWidth = 1080f;
    int fontSize = 128;


    void Start()
    {
        pauseCanvas.enabled = false;
    }

	// Update is called once per frame
	void Update () {
        float percentage = (caloriesEaten * 100 / MAX_CALORIES);

        scoreText.text = "Score: " + score;
        calorieText.text = percentage + "% Full";


        //Allow the player to quit the game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Application.LoadLevel("Title");
            }
            else
            {
                pauseGame();
            }
        }
	}

    public void PauseButtonClicked()
    {
        pauseGame();
    }

    public void GoToMenu()
    {
        if (GameObject.Find("Viewbag"))
        {
            Destroy(GameObject.Find("Viewbag"));
        }

        Application.LoadLevel("Title");
    }

    public void EatFood(int multiplier, int foodPoints)
    {
        score += foodPoints * multiplier;
        caloriesEaten += multiplier;

        if (caloriesEaten >= MAX_CALORIES)
        {
            GameObject.Find("Viewbag").GetComponent<GameInfo>().score = score;
            Application.LoadLevel("Score");
        }
    }

    void pauseGame()
    {
        if (gamePaused)
        {
            gamePaused = false;
            Time.timeScale = 1;
            pauseCanvas.enabled = false;
        }
        else
        {
            gamePaused = true;
            Time.timeScale = 0;
            pauseCanvas.enabled = true;
        }
    }
}
