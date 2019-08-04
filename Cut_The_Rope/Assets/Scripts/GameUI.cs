using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject winMenu;

    public GameObject[] stars;

    public Slider[] volumeSliders;

    void Start()
    {
        AudioSetting audioSetting = GameDataManager.instance.gameData.optionData.audioSetting;
        volumeSliders[0].value = audioSetting.masterVolumePercent;
        volumeSliders[1].value = audioSetting.musicVolumePercent;
        volumeSliders[2].value = audioSetting.sfxVolumePercent;
    }

    private void OnEnable()
    {
        GameManager.OnWinStatic += Win;
    }

    private void OnDisable()
    {
        GameManager.OnWinStatic -= Win;
    }

    public void Win()
    {
        winMenu.SetActive(true);
        pauseMenu.SetActive(false);

        int starCode = GameManager.instance.starCode;
        stars[0].SetActive((starCode % 2 == 1) ? true : false);
        stars[1].SetActive((starCode / 2 % 2 == 1) ? true : false);
        stars[2].SetActive((starCode / 4 % 2 == 1) ? true : false);
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Next()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level " + GameManager.instance.nextLevel);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
}
