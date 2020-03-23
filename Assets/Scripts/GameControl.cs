using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameControl  {

    public static 
        bool isGameActive = true, attackEnabled = true, verticalMoveEnabled = true, playerIsChasingEnemy = false, isAttacking = false;

    public static float cameraMovespeed = 1f;

    public static bool canAttack() {
        return isGameActive && attackEnabled;
    }

    public static bool canMove() {
        return isGameActive && verticalMoveEnabled;
    }


}
