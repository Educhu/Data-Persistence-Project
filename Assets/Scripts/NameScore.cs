using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class NameScore : MonoBehaviour
{
    public string playerName;
    public InputField inputFieldPlayerName;
    public Text bestScore;

    public int pontos;
    public int maxPoints;

    // Start is called before the first frame update
    void Start()
    {
        maxPoints = 0;

        //pontos = GameObject.Find("Main Manager").GetComponent<MainManager>().m_Points;

        inputFieldPlayerName = GameObject.FindGameObjectWithTag("NamePlayer").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        //bestScore.text = "Best Score: " + playerName + pontos;
    }

    public void OnChangeInputField()
    {
        playerName = inputFieldPlayerName.text;

        Debug.Log(playerName);

        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
        }
    }

    //.toString
}
