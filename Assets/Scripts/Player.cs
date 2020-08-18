using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : Battler
{
    public Enemy enemy;
    public AttackHandler attackHandler;

    // Use this for initialization
    public override void Start()
    {
        string playerSaveData = PlayerPrefs.GetString("player_data");
        SaveData saveData = JsonUtility.FromJson<SaveData>(playerSaveData);
        bugStats.SetStats(saveData.name, saveData.level, saveData.health);
    }

    public override int CalculateDamageDealt()
    {
        return 100;
    }

    public void Attack()
    {
        StartCoroutine(PlayAttackAnimation());
    }

    IEnumerator PlayAttackAnimation()
    {
        GameObject target = enemy.attackHandler.gameObject;
        attackHandler.Attack(target, AttackHandler.BackupAngle.Player);
        yield return new WaitForSeconds(3f);
        attackHandler.ResetAttacker();

        if (enemy.IsDead())
        {
            PlayerPrefs.SetString("battle_result", "win");
            SceneManager.LoadScene("PostBattle");
        } else
        {
            yield return StartCoroutine(enemy.Attack());
        }
    }
}
