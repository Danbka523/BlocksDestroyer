using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{


    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text TopScoreText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;



    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        ScoreText.text = $"{MainManager.mainManager.playerName}'s Score : {m_Points}";
        TopScoreText.text = $"Best score from {MainManager.mainManager.topPlayerName} = {MainManager.mainManager.highScore}";

        MainManager.mainManager.LoadAllData();

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {


                MainManager.mainManager.LoadAllData();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{MainManager.mainManager.playerName}'s Score : {m_Points}";
    }

    public void GameOver()
    {

        if (m_Points > MainManager.mainManager.highScore)
        {
            MainManager.mainManager.highScore = m_Points;
            MainManager.mainManager.topPlayerName = MainManager.mainManager.playerName;
        }
        AddToTopList(ref MainManager.mainManager.topNames, ref MainManager.mainManager.topScores);
        MainManager.mainManager.SaveAllData();

        if (m_Points > MainManager.mainManager.highScore)
        {
            MainManager.mainManager.highScore = m_Points;
            MainManager.mainManager.topPlayerName = MainManager.mainManager.playerName;
            MainManager.mainManager.SaveAllData();
        }

        m_GameOver = true;
        GameOverText.SetActive(true);
    }


    private void AddToTopList(ref List<string> topNames, ref List<int> topScores)
    {
        bool isReplaced = false;
        (string, int) current = ("", 0);
        for (int i = 0; i < topScores.Count; i++)
        {

            if (isReplaced)
            {
                var t = (topNames[i], topScores[i]);
                topNames[i] = current.Item1;
                topScores[i] = current.Item2;
                current = t;
            }
            if (m_Points > topScores[i] && !isReplaced)
            {
                current = (topNames[i], topScores[i]);
                topScores[i] = m_Points;
                topNames[i] = MainManager.mainManager.playerName;
                isReplaced = true;

            }
        }

    }

    public void ExitInMenu()
    {
        MainManager.mainManager.SaveAllData();
        SceneManager.LoadScene(0);
    }
}
