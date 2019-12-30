using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public ResItem[] resolution;
    public Dropdown resolutionDropdown;
    int selectedResolution = 0;
    public Text resolutionText;

    public AudioMixer audioMixer;
    public Slider masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider;
    //public AudioSource sfx;
    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        for (int i = 0; i < resolution.Length; i++)
        {

            if ((Screen.width == resolution[i].width) && (Screen.height == resolution[i].height))
            {
                selectedResolution = i;
                resolutionDropdown.value = i;
            }
        }
        resolutionText.text = Screen.width.ToString() + "x" + Screen.height.ToString();

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnChangeFullscreenMode()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }


    public void OnSelectResolution()
    {
        selectedResolution = resolutionDropdown.value;
        Screen.SetResolution(resolution[selectedResolution].width, resolution[selectedResolution].height, fullscreenToggle.isOn);
    }

    public void OnChangeMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }

    public void OnChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    public void OnChangeSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }


    /*public void PlaySFX()
    {
        sfx.Play();
    }

    public void StopSFX()
    {
        sfx.Stop();
    }*/

}

[System.Serializable]
public class ResItem
{
    public int height, width;
}