using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Text highScore;
    public Text Loading;
    public GameObject panel;
    public Slider s;
    // Use this for initialization
    AsyncOperation a;
	void Start () {
        panel.SetActive(false);
        highScore.text = "Highscore: " +((int)PlayerPrefs.GetFloat("Highscore")).ToString(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ToGame()
    {
        panel.SetActive(true);
        StartCoroutine(LoadingWithLoading());
       
        //SceneManager.LoadScene("runner");
    }
    IEnumerator LoadingWithLoading()
    {
        yield return new WaitForSeconds(1);
        a = SceneManager.LoadSceneAsync(1);
        a.allowSceneActivation = false;
        while (!a.isDone)
        {
            s.value = a.progress;
            if (a.progress == 0.9f)
            {
                a.allowSceneActivation = true;
                
            }
            yield return null;
        }
    }

}
