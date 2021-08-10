using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    [SerializeField]
    private GameObject black;

    [SerializeField]
    private GameObject audioSource; 

    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject[] restartButton; 
    // Update is called once per frame
    public void GoToNextLevel()
    {
        
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1; 
    }

    public void DontDestroy()
    {
        DontDestroyOnLoad(audioSource); 
    }
    public void Play()
    {
        Time.timeScale = 1;
        black.SetActive(false);
        playButton.SetActive(false);
        restartButton[1].SetActive(false); 
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restartButton[0].SetActive(false);
        restartButton[1].SetActive(false); 

    }
}
