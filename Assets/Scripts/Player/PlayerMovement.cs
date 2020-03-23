using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    [Range(0, 10)]
    public float followEnemyMovespeed;

    bool returningToStartPosition = false;

    enum animationStates { down = 0, left, right, up, attackDown, attackLeft, attackRight, attackUp };

    Vector3 startPosition;

    bool endedVerticalMovement = false, endedHorizontalMovement = false;

    PlayerAttack playerAttack;

    private void Start() {
        playerAttack = GetComponent<PlayerAttack>();
        startPosition = transform.position;
    }


    void ResetMovement() {
        endedHorizontalMovement = false;
        endedVerticalMovement = false;
    }

    private void Update() {
        CheckTarget();
        MovePlayer();
    }

    void CheckTarget() {
        if (GameControl.isAttacking)
            return;

        if (!PlayerData.isRanged && playerAttack.HasTarget() && !GameControl.playerIsChasingEnemy) {
            if (TargetIsClose()) {
                GameControl.verticalMoveEnabled = false;
                GameControl.playerIsChasingEnemy = true;
                ResetMovement();
            }
        }
    }

    void MovePlayer() {
        if (returningToStartPosition) {
            MoveTowardsTarget(startPosition);
        }
        else if (GameControl.playerIsChasingEnemy) {
            MoveTowardsTarget(playerAttack.GetTarget().position);
        }
        
    }

    void MoveHorizontally (float x) {
        float step = followEnemyMovespeed * Time.deltaTime;
        Vector2 horizontalTarget = new Vector2(x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, horizontalTarget, step);

        if (returningToStartPosition) {
            if (Vector2.Distance(transform.position, horizontalTarget) < 0.01f) {
                endedHorizontalMovement = true;
            }
        }
        else if (Vector2.Distance(transform.position, horizontalTarget) < 1.5f) {
            endedHorizontalMovement = true;
        }


    }

    void MoveVertically (float y) {
        float step = followEnemyMovespeed * Time.deltaTime;
        Vector2 verticalTarget = new Vector2(transform.position.x, y);
        transform.position = Vector2.MoveTowards(transform.position, verticalTarget, step);

        if (Vector2.Distance(transform.position, verticalTarget) < 0.1f) {
            endedVerticalMovement = true;
        }
    }


    public void LoseTarget() {
        GameControl.playerIsChasingEnemy = false;
        EndChase();
        ResetMovement();
    }


    /// <summary>
    /// Primeiro move o personagem verticalmente, depois horizontalmente.
    /// Quando acabar ambas movimentações, alcança o alvo.
    /// </summary>
    /// <param name="position"></param>
    void MoveTowardsTarget(Vector3 position) {

        if (!endedVerticalMovement)
            MoveVertically(position.y);
        else if (!endedHorizontalMovement)
            MoveHorizontally(position.x);
        

        if (endedVerticalMovement && endedHorizontalMovement) { 
            ReachTarget(); 
        }
    }


    void StartVerticalMovement() {
        returningToStartPosition = false;
        GameControl.verticalMoveEnabled = true;
    }

    /// </summary>
    void ReachTarget() {
        if (returningToStartPosition) {
            StartVerticalMovement();
        }
        else {
            if (playerAttack.HasTarget()) {
                GameControl.playerIsChasingEnemy = false;
                GameControl.isAttacking = true;
            }
        }
    }

    void EndChase() {
        returningToStartPosition = true;
    }

    bool TargetIsClose() {
        Transform target = playerAttack.GetTarget();
        return Mathf.Abs(transform.position.y - target.position.y) < 0.1f || (target.position.y > transform.position.y &&
            Mathf.Abs(target.position.y - transform.position.y) > 2);
    }
}
