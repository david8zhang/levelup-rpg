using UnityEngine;
using System.Collections;

public class Battler : MonoBehaviour
{

    public BugStats bugStats;
    public struct Bug
    {
        public int attack;
        public int defense;
    }

    public Bug bugRef; // A reference to bug's attack and defense stat for damage calculation


    // Use this for initialization
    public virtual void Start()
    {

    }

    public virtual int CalculateDamageDealt(Battler other)
    {
        int attack = bugRef.attack;
        int baseDamage = Mathf.FloorToInt((attack * attack) / (attack + other.bugRef.defense));

        int critValue = Random.Range(1, 10);
        if (critValue == 1)
        {
            baseDamage *= 2;
            Debug.Log("CRITICAL HIT!");
        }
        return baseDamage;
    }

    public virtual void TakeDamage(int damage)
    {
        Debug.Log(name + " took " + damage + " damage!");
        bugStats.TakeDamage(damage);
    }

    public bool IsDead()
    {
        return bugStats.health == 0;
    }
}
