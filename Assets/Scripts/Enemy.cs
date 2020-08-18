using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Enemy : Battler
{
    public Player player;
    public AttackHandler attackHandler;

    // Use this for initialization
    public override void Start()
    {
        bugStats.SetStats("Wasp", 10, 100);
    }

    public override int CalculateDamageDealt()
    {
        return 100;
    }

    public IEnumerator Attack()
    {
        GameObject target = player.attackHandler.gameObject;
        attackHandler.Attack(target, AttackHandler.BackupAngle.Enemy);
        yield return new WaitForSeconds(3f);
        attackHandler.ResetAttacker();

        if (player.IsDead())
        {
            PlayerPrefs.SetString("battle_result", "loss");
            SceneManager.LoadScene("PostBattle");
        }
      
    }
}
