using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public InputField playerNameField;
    private void Start()
    {
    }

    public void OnStartClick()
    {
        MainManager.mainManager.playerName = playerNameField.text;
        MainManager.mainManager.LoadAllData();
        SceneManager.LoadScene(1);
    }
    
    public void OnTopClick()
    {
        SceneManager.LoadScene(2);
    }
    public void OnExitClick()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnClearClick() {
        MainManager.mainManager.ClearData(); 
    }
}
