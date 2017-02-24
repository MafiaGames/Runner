using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Text highScore;
	// Use this for initialization
	void Start () {
        highScore.text = "Highscore: " +((int)PlayerPrefs.GetFloat("Highscore")).ToString(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ToGame()
    {
        SceneManager.LoadScene("runner");
    }
}
