using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    private float score = 0.0f;
    public Text scoreText;
    //public GameObject scoreContainer;
    private int difficultyLevel = 1;
    private int difficultyLevelMax = 10;
    private int scoreToNextLevel = 10;
    private bool isDead = false;
    public DeathMenu deathMenu;

    // Update is called once per frame
    private void Update()
    {
        if (isDead)
            return;
        if (score >= scoreToNextLevel)
            LevelUp();
       
        score += Time.deltaTime * difficultyLevel; 
        scoreText.text = ((int)score).ToString();
        

    }

    void LevelUp()
    {
        if (difficultyLevel == difficultyLevelMax)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerMovement>().SetSpeed(difficultyLevel);
        Debug.Log("Level up : " + isDead);
    }

    public void OnDeath()
    {

        //Debug.Log("Received call : OnDeath() : " + isDead);
        isDead = true;
        Debug.Log("Received call : OnDeath()2 : " + isDead);

        if (PlayerPrefs.GetFloat("Highscore") < score)
            PlayerPrefs.SetFloat("Highscore", score);
        
        deathMenu.ToggleEndMenu(score);
    }

    public void Collectable()
    {
        Debug.Log("Collectable called");
        score += 10;
        scoreText.text = ((int)score).ToString();
    }
}
