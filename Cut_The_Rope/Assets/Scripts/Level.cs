using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour {

    public string titleText;

    public GameObject openLevel;

    public Text levelText;

    public GameObject star;

    public GameObject[] stars;

    public bool isLock;

    private int starCode;

    private void OnEnable()
    {
        levelText.text = titleText;
        LoadLevelData();
    }

    private void Update()
    {
        if (isLock)
        {
            openLevel.SetActive(false);
            star.SetActive(false);
        }
        else
        {
            openLevel.SetActive(true);
            star.SetActive(true);
            
            stars[0].SetActive((starCode % 2 == 1) ? true : false);
            stars[1].SetActive((starCode / 2 % 2 == 1) ? true : false);
            stars[2].SetActive((starCode / 4 % 2 == 1) ? true : false);
        }
    }

    public void OnClick()
    {
        if (!isLock)
        {
            string sceneName = "Level " + levelText.text;
            LoadLevel(sceneName);
        }
    }

    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void LoadLevelData()
    {
        string loadJson = PlayerPrefs.GetString("Level " + levelText.text, "");

        if (loadJson == "")
        {
            LevelData levelData = new LevelData(true, 0);
            if (levelText.text == "1-1")
                levelData = new LevelData(false, 0);
            string saveString = JsonUtility.ToJson(levelData);

            PlayerPrefs.SetString("Level " + levelText.text, saveString);
        }
        else
        {
            LevelData loadData;
            loadData = JsonUtility.FromJson<LevelData>(loadJson);
            isLock = loadData.isLock;
            starCode = loadData.starCode;
        }
    }
}
