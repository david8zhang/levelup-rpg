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

    private void Awake()
    {
        expBar.gameObject.SetActive(false);
        expGainedText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        string result = PlayerPrefs.GetString("battle_result");
        string playerData = PlayerPrefs.GetString("player_data");
        saveData = JsonUtility.FromJson<SaveData>(playerData);
        if (result == "win")
        {
            resultLabel.text = "You won!";
            HandleExpGain();
            button.onClick.AddListener(() => GoToFight());
        } else
        {
            resultLabel.text = "You lost...";
            button.GetComponentInChildren<Text>().text = "Try again";
            button.onClick.AddListener(() => GoToHome());
        }
    }

    public void HandleExpGain()
    {
        // Level up logic
        int experienceGained = 10;
        int experience = saveData.experience + experienceGained;
        int level = saveData.level;
        int maxExp = Mathf.FloorToInt(Mathf.Pow(2, level));

        bool isLevelUp = experience >= maxExp;
        if (isLevelUp)
        {
            experience -= maxExp;
            level++;
            maxExp = Mathf.FloorToInt(Mathf.Pow(2, level));
            HandleStatIncreaseOnLevelUp();
        }

        // Set the UI
        expBar.gameObject.SetActive(true);
        expGainedText.gameObject.SetActive(true);
        expBar.Init(level.ToString(), (level + 1).ToString(), experience, maxExp);
        expGainedText.text = "+" + experienceGained.ToString() + " EXP" + (isLevelUp ? " (Level Up!)" : "");

        // Save the experience gained
        saveData.experience = experience;
        saveData.level = level;
    }

    public void HandleStatIncreaseOnLevelUp()
    {
        saveData.attack += 5;
        saveData.defense += 5;
        saveData.health += 5;
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
