using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class NameScore : MonoBehaviour
{
    public string playerName;
    public InputField inputFieldPlayerName;
    public Text melhorPontuacao;

    public int pontos;
    public string playerNameHigh;


    // Start is called before the first frame update
    void Start()
    {
        inputFieldPlayerName = GameObject.FindGameObjectWithTag("NamePlayer").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        LoadHighScore();
        DontDestroyOnLoad( gameObject );
    }

    public void OnChangeInputField()
    {
        playerName = inputFieldPlayerName.text;
    }


    
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/armazena.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(json);

            playerNameHigh = data.playerNameToSave;
            pontos = data.playerPointsToSave;

            melhorPontuacao.text = "Best Score : " + playerNameHigh + " : " + pontos.ToString();
        }
    }
}
