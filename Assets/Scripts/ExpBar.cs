using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    Gradient gradient;

    [SerializeField]
    Image fill;

    [SerializeField]
    Text currLevelText;

    [SerializeField]
    Text nextLevelText;

    public void Start()
    {
    }

    public void Init(string currLevel, string nextLevel, int currExp, int maxExp)
    {
        currLevelText.text = "Lv. " + currLevel;
        nextLevelText.text = "Lv. " + nextLevel;
        SetMaxExp(maxExp);
        SetExp(currExp);
    }

    public void SetExp(int exp)
    {
        slider.value = exp;
    }

    public void SetMaxExp(int exp)
    {
        slider.maxValue = exp;
    }
}
