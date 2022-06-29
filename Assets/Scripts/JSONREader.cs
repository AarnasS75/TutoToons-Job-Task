using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string[] level_data;
}
[System.Serializable]
public class LevelsList
{
    public Level[] levels;
}

public class JSONREader : MonoBehaviour
{
    public TextAsset jsonFile;

    public LevelsList levelList = new LevelsList();


    private void Awake()
    {
        ReadJSON();
    }
    void ReadJSON()
    {
        levelList = JsonUtility.FromJson<LevelsList>(jsonFile.text);
    }
}
