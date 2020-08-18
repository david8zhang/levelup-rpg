using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PostBattleManager : MonoBehaviour
{

    public Text resultLabel;
    public Button button;
    public ExpBar expBar;
    public Text expGainedText;

    SaveData saveData;

    // Start is called before the first frame update
    void Start()
    {
        string result = PlayerPrefs.GetString("battle_result");
        if (result == "win")
        {
            resultLabel.text = "You won!";
            InitGainedExp();
            button.onClick.AddListener(() => GoToFight());
        } else
        {
            expBar.gameObject.SetActive(false);
            resultLabel.text = "You lost...";
            button.GetComponentInChildren<Text>().text = "Try again";
            button.onClick.AddListener(() => GoToHome());
        }
    }

    public void InitGainedExp()
    {
        string playerData = PlayerPrefs.GetString("player_data");
        saveData = JsonUtility.FromJson<SaveData>(playerData);

        int experienceGained = 10;
        int experience = saveData.experience + experienceGained;
        int level = saveData.level;
        int maxExp = Mathf.FloorToInt(Mathf.Pow(2, level));

        if (experience >= maxExp)
        {
            experience -= maxExp;
            level++;
            maxExp = Mathf.FloorToInt(Mathf.Pow(2, level));
        }

        // Set the UI
        expBar.Init(level.ToString(), (level + 1).ToString(), experience, maxExp);
        expGainedText.text = "+" + experienceGained.ToString() + " EXP";

        // Save the experience gained
        saveData.experience = experience;
        saveData.level = level;
    }

    public void GoToFight()
    {
        string playerData = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("player_data", playerData);
        SceneManager.LoadScene("Battle");
    }

    public void GoToHome()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }
}
