using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public InputField nameInput;
    public Button playButton;

    private void Start()
    {
        playButton.gameObject.SetActive(false);
        playButton.onClick.AddListener(() => OnPlayClick());
    }

    public void Update()
    {
        if (nameInput.text != "")
        {
            playButton.gameObject.SetActive(true);
        } else
        {
            playButton.gameObject.SetActive(false);
        }
    }

    public void OnPlayClick()
    {
        SaveData saveData = new SaveData
        {
            name = nameInput.text,
            attack = 50,
            defense = 50,
            health = 100,
            level = 5,
            experience = 0
        };
        string saveDataString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("player_data", saveDataString);
        SceneManager.LoadScene("Battle");        
    }
}
