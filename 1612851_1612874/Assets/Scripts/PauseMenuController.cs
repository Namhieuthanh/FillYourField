using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject optionScreen, pauseScreen;

    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        optionScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }
    }

    public void PauseResumeGame()
    {
        if (isPaused == false)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void OpenOption()
    {
        optionScreen.SetActive(true);
    }

    public void CloseOption()
    {
        optionScreen.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
