using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    Vector3 oldAttackerPosition;
    Vector3 attackerWindUp;
    bool isAttacking;

    float attackDuration = 2.0f;
    float attackCountdown;

    GameObject defender;
    public Battler battlerRef;

    public enum BackupAngle
    {
        Player,
        Enemy
    }


    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            PlayAttackAnimation();
        }
    }

    internal void PlayAttackAnimation()
    {
        // Move Attacker forward
        attackCountdown -= Time.deltaTime;
        Vector3 targetPosition = defender.transform.position;
        float forwardStep = 500 * Time.deltaTime * 10;
        float backwardStep = 50 * Time.deltaTime;

        Vector3 currentPosition = gameObject.transform.position;

        // Wind up animation
        if (attackCountdown > 1.0f && attackCountdown <= attackDuration)
        {
            gameObject.transform.position = Vector3.MoveTowards(currentPosition, attackerWindUp, backwardStep);
        }

        // Overshoot and pullback
        if (attackCountdown > 0.8f && attackCountdown <= 1.0f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, forwardStep);
        }
        else if (attackCountdown > 0.5f && attackCountdown <= 0.7f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, oldAttackerPosition, forwardStep);
        }
    }

    public void ResetAttacker()
    {
        oldAttackerPosition = new Vector3();
        attackerWindUp = new Vector3();
        isAttacking = false;
        attackCountdown = attackDuration;
        defender = null;
    }

    public void Attack(GameObject defender, BackupAngle backupAngle)
    {
        attackCountdown = attackDuration;
        oldAttackerPosition = gameObject.transform.position;
        Vector3 backupVector;
        switch (backupAngle)
        {
            case BackupAngle.Player:
                backupVector = new Vector3(-10, -10, 0);
                break;
            case BackupAngle.Enemy:
                backupVector = new Vector3(10, 10, 0);
                break;
            default:
                backupVector = new Vector3(0, 0, 0);
                break;
        }
        attackerWindUp = gameObject.transform.position + backupVector;
        this.defender = defender;
        isAttacking = true;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (isAttacking)
        {
            Battler otherBattler = col.GetComponent<AttackHandler>().battlerRef;
            int damageDealt = battlerRef.CalculateDamageDealt(otherBattler);
            otherBattler.TakeDamage(damageDealt);
        }
    }
}
