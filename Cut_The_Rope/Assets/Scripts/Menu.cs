using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject optionsMenu;
    public GameObject levelSelection;

    public Slider[] volumeSliders;

	void Start() {

        AudioSetting audioSetting = GameDataManager.instance.gameData.optionData.audioSetting;
        volumeSliders[0].value = audioSetting.masterVolumePercent;
        volumeSliders[1].value = audioSetting.musicVolumePercent;
        volumeSliders[2].value = audioSetting.sfxVolumePercent;

    }
    
	public void Play() {
		SceneManager.LoadScene ("Game");
	}

    public void Quit() {
		Application.Quit ();
	}

	public void OptionsMenu() {
		mainMenu.SetActive (false);
		optionsMenu.SetActive (true);
        levelSelection.SetActive(false);
    }

	public void MainMenu() {
		mainMenu.SetActive (true);
		optionsMenu.SetActive (false);
        levelSelection.SetActive(false);
    }

    public void LevelSelection()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void Reset()
    {
        LevelData levelData = new LevelData(false, 0);
        string saveString = JsonUtility.ToJson(levelData);
        PlayerPrefs.SetString("Level 1-1", saveString);

        for (int i=2;i<10; i++)
        {
            levelData = new LevelData(true, 0);
            saveString = JsonUtility.ToJson(levelData);
            PlayerPrefs.SetString("Level 1-" + i, saveString);
        }
        
    }

    public void SetMasterVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Master);
    }

	public void SetMusicVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Music);
    }

	public void SetSfxVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Sfx);
    }

}
