using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionScreen;
    public Button PlayButton, SettingButton;
    public Sprite NormalPlayButton, HighlightedPlayButton, NormalSettingButton, HighlightedSettingButton;
    // Start is called before the first frame update
    void Start()
    {
        optionScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeNormalPlayButon()
    {
        PlayButton.GetComponent<Image>().sprite = NormalPlayButton;
    }

    public void HoverPlayButon()
    {
        PlayButton.GetComponent<Image>().sprite = HighlightedPlayButton;
    }

    public void BeNormalSettingButon()
    {
        SettingButton.GetComponent<Image>().sprite = NormalSettingButton;
    }

    public void HoverSettingButon()
    {
        SettingButton.GetComponent<Image>().sprite = HighlightedSettingButton;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void OpenOption()
    {
        optionScreen.SetActive(true);

    }

    public void CloseOption()
    {
        optionScreen.SetActive(false);
    }
}
