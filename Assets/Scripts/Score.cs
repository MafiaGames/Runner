using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private float score = 0.0f;
    private int difficultyLevel = 1;
    private int maxDifficultylevel = 10;
    private int scoreToNext = 10;
    public Text scoreText;
    public GameObject DeathMenu;
    private bool isDead =false;
    public DeathMenu dm;
	// Use this for initialization
	void Start () {
        scoreText.text = "0";
	}
	
	// Update is called once per frame
    public int getScore()
    {
        return (int)score;
    }
	void Update () {
       
        
	}

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultylevel) return;
        scoreToNext *= 2;
        difficultyLevel++;
        GetComponent<Runner>().SetSpeed(difficultyLevel);
    }
    public void OnDeath()
    {
        if(PlayerPrefs.GetFloat("Highscore")<score)
            PlayerPrefs.SetFloat("Highscore",score);
        dm.ToggleMenu(score);
        isDead = true;
    }
    public void AddScore()
    {
        if (isDead) return;
        if (score >= scoreToNext) { LevelUp(); }
        score ++;
        scoreText.text = ((int)score).ToString();
    }
}
