using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public static MainManager mainManager;
    public List<string> topNames;
    public List<int> topScores;
    public string playerName;
    public string topPlayerName;
    public int highScore;
    public string path;
    public string pathTop;

    private void Awake()
    {
        path = Application.dataPath + "mydata.json";
        pathTop = Application.dataPath + "mydataTop.json";
        if (mainManager == null)
            mainManager = this;
        DontDestroyOnLoad(mainManager);
        LoadAllData();
        InitializateList();
    }

    public void InitializateList()
    {
        if (topNames.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                topNames.Add("Unnamed");
                topScores.Add(0);
            }
        }

    }


    [Serializable]
    class SaveTopData
    {
        public List<string> topNames;
        public List<int> topScores;

    }

    [Serializable]
    class SaveData
    {
        public string topPlayerName;
        public int highScore;
    }

    public void SaveAllData()
    {
        SaveData myData = new SaveData();
        myData.topPlayerName = topPlayerName;
        myData.highScore = highScore;
        string json = JsonUtility.ToJson(myData);
        File.WriteAllText(path, json);

        SaveTopData myTopData = new SaveTopData();
        myTopData.topNames = topNames;
        myTopData.topScores = topScores;
        string json2 = JsonUtility.ToJson(myTopData);
        File.WriteAllText(pathTop, json2);
    }

    public void LoadAllData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData myData = JsonUtility.FromJson<SaveData>(json);
            topPlayerName = myData.topPlayerName;
            highScore = myData.highScore;
        }
        if (File.Exists(pathTop)) {

            string json = File.ReadAllText(pathTop);

            SaveTopData myData = JsonUtility.FromJson<SaveTopData>(json);
            topNames = myData.topNames;
            topScores = myData.topScores;
        }


    }
    public void ClearData()
    {
        File.Delete(path);
        File.Delete(pathTop);
        topNames = new List<string>();
        topScores = new List<int>();
        highScore = 0;
        topPlayerName = "";
        InitializateList();
        SaveAllData();
    }
}
