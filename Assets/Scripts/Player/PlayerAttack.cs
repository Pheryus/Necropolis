using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    float timer = 0;
    Transform target;
    Structure targetStructure;

    #region PlayerReferences
    PlayerMovement playerMovement;
    PlayerAnimation playerAnimation;
    #endregion

    const float meleeAttackAnimationDuration = 0.2f;

    List<float> meleeAttackTimers;


    #region Singleton

    public static PlayerAttack instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        meleeAttackTimers = new List<float>();
    }

    public void SetTarget(Transform t) {

        if (GameControl.isAttacking && t == target) {
            ExecuteMeleeAttack();
            return;
        }

        if (target != null && target != t) {
            return;
        }

        target = t;
        targetStructure = target.GetComponent<Structure>();
    }


    public bool HasTarget() {
        return target != null;
    }

    public Transform GetTarget() {
        return target;
    }

    public void DestroyTarget() {
        ResetAttack();
        playerMovement.LoseTarget();
    }

    void ResetAttack() {
        target = null;
        GameControl.isAttacking = false;
        targetStructure = null;
    }

    public void ExecuteMeleeAttack() {
        if (meleeAttackTimers.Count >= 0) {
            playerAnimation.ResetMeleeAnimation();

            if (targetStructure != null)
                targetStructure.TakeDmg(PlayerData.dmg);

            meleeAttackTimers.Clear();
        }

        meleeAttackTimers.Add(0);
        timer = 0;
    }

    void UpdateMeleeAttackInAnimation() {
        if (meleeAttackTimers.Count > 0) {
            meleeAttackTimers[0] += Time.deltaTime;
            if (meleeAttackTimers[0] > meleeAttackAnimationDuration) {
                meleeAttackTimers.Clear();
                if (targetStructure != null)
                    targetStructure.TakeDmg(PlayerData.dmg);
            }
        }
    }

    private void Update() {
        if (GameControl.isAttacking) {
            timer += Time.deltaTime;
            if (timer > 2) {
                ExecuteMeleeAttack();
            }
        }
        else
            UpdateMeleeAttackInAnimation();
    }

}
