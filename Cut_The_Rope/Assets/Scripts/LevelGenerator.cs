using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public int levelCount = 1;

    public int page = 1;

    public int maxX = 1;

    public int maxY = 1;

    public Level levelPrefab;
	
	void Update () {
        Generator();
    }

    void Generator()
    {
        for (int i=0;i< levelCount; i++)
        {
            
        }
    }
}
