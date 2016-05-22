using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

    public int score = 0;
    public int bestScore = 0;
    string savedScoreName = "BestScore";


    // Use this for initialization
    void Start()
    {
        //Keep this object alive.
        DontDestroyOnLoad(this);

        //Set the high score that was saved previously.
        bestScore = PlayerPrefs.GetInt(savedScoreName);
    }

    /// <summary>
    /// Overwrite the old score with the new score.
    /// </summary>
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt(savedScoreName, bestScore);
    }

    /// <summary>
    /// Delete the save data.
    /// </summary>
    public void ClearSavedData()
    {
        bestScore = 0;
        SaveHighScore();
    }

    /// <summary>
    /// Set the new high score.
    /// </summary>
    /// <param name="newScore"></param>
    public void CheckHighScore(int newScore)
    {
        if (newScore > bestScore)
        {
            bestScore = newScore;
            SaveHighScore();
        }
    }
}
