using UnityEngine;
using System.Collections;

public class Battler : MonoBehaviour
{

    public BugStats bugStats;


    // Use this for initialization
    public virtual void Start()
    {

    }

    public virtual int CalculateDamageDealt()
    {
        return Random.Range(10, 30);
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
