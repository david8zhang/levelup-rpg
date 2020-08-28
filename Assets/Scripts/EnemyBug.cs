using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Bug", menuName = "Bug")]
public class EnemyBug : ScriptableObject
{
    public new string name;
    public Sprite image;
    public int baseAttack;
    public int baseDefense;
    public int baseHealth;
}
