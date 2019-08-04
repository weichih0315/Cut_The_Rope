using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int starCode;

    public static event System.Action OnWinStatic;

    public string nextLevel = "";

    private void Awake()
    {
        instance = this;
    }

    public void Win()
    {
        if (OnWinStatic != null)
        {
            OnWinStatic();
        }

        SaveLevel();

        //解鎖  
        string loadJson = PlayerPrefs.GetString("Level " + nextLevel, "");
        LevelData loadData = JsonUtility.FromJson<LevelData>(loadJson);
        int starCodeTemp = loadData.starCode;

        LevelData levelData = new LevelData(false, starCodeTemp);
        string saveString = JsonUtility.ToJson(levelData);
        PlayerPrefs.SetString("Level " + nextLevel, saveString);
    }

    public void SaveLevel()
    {
        string loadJson = PlayerPrefs.GetString(SceneManager.GetActiveScene().name, "");
        LevelData loadData = JsonUtility.FromJson<LevelData>(loadJson);
        int starCodeTemp = loadData.starCode;

        if (starCode > starCodeTemp)
        {
            starCodeTemp = starCode;

            LevelData levelData = new LevelData(false, starCodeTemp);
            string saveString = JsonUtility.ToJson(levelData);
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);
        }
            
    }
}
