using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugStats : MonoBehaviour
{
    public Text bugName;
    public Text levelText;
    public HealthBar healthBar;
    public int health;


    public void SetStats(string name, int level, int health)
    {
        bugName.text = name;
        levelText.text = "Lv. " + level;
        this.health = health;
        healthBar.SetMaxHealth(health);
        healthBar.SetHealth(health);
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        healthBar.SetHealth(health);
    }
}
