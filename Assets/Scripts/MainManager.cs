using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    public int m_Points;
    
    private bool m_GameOver = false;

    public static MainManager Instance;
    //public Text atualizedPoints;
    public int highScore;
    public string namePlayer;
    public Text bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        ConstroiBricks();
       
        highScore = GameObject.Find("Armazenamento De Dados").GetComponent<NameScore>().maxPoints;
        namePlayer = GameObject.Find("Armazenamento De Dados").GetComponent<NameScore>().playerName;
    }

    private void Update()
    {
        if (!m_Started)
        {
            AtualizaScore();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                if (Ball == null)
                {
                    Ball = GameObject.Find("Ball").GetComponent<Rigidbody>();
                }

               //

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);

                ConstroiBricks();
            }
        }
        else if (m_GameOver)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                AtualizaScore();

               // m_GameOver = false;
                m_Started = false;
            }
        }

        Debug.LogWarning(m_GameOver);
    }

   

    public void AddPoint(int point)
    {
        m_Points += point;
        if (ScoreText == null)
        {
            ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        }
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        AtualizaScore();

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    private void AtualizaScore()
    {
        if (bestScore == null)
        {
            bestScore = GameObject.FindGameObjectWithTag("BestScore").GetComponent<Text>();
        }

        if (m_Points > highScore)
        {
            highScore = m_Points;
        }

        bestScore.text = "Best Score : " + namePlayer + " : " + highScore;

        m_Points = 0;
    }

    void ConstroiBricks()
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
    }
    
    void LoadScore()
    {
        

        bestScore.text = "Best Score : " + namePlayer + " : " + highScore;
    }

}
