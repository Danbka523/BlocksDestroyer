using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TopScoreUI : MonoBehaviour
{
    public List<TextMeshProUGUI> topNamesText;
    public List<TextMeshProUGUI> topScoresText;
    private void Awake()
    {
        try
        {
            for (int i = 0; i < topNamesText.Count; i++)
            {
                topNamesText[i].text = $"{i+1}. {MainManager.mainManager.topNames[i]}:";
                topScoresText[i].text = MainManager.mainManager.topScores[i].ToString();
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);       
        }
    }

    public void OnExitClick() {
        SceneManager.LoadScene(0);  
    }
}
