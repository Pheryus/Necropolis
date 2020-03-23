using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAttackAction : MonoBehaviour {

    public LayerMask attackableLayer;

    bool tap = false;

    PlayerAttack player;

    private void Start() {
        player = GetComponent<PlayerAttack>();
    }

    void CheckTap() {
#if UNITY_EDITOR || UNITY_STANDALONE
        CheckPCTap();
#elif UNITY_ANDROID
        CheckMobileTap();
#endif

        if (tap) {
            CheckInteraction();
            tap = false;
        }
    }

    void CheckInteraction() {
        RaycastHit2D hit = GetRayCast();
        if (hit.collider != null) {
            player.SetTarget(hit.collider.transform);
        }
    }

    void CheckPCTap() {
        if (Input.GetMouseButtonDown(0)) {
            tap = true;
        }
    }

    void CheckMobileTap() {
        if (Input.touches.Length > 0) {
            Touch t = Input.touches[0];
            if (t.phase == TouchPhase.Began) {
                tap = true;
            }
        }
    }


    Vector2 GetMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2 GetTouchPosition() {
        Vector2 pos = Vector2.zero;
        if (Input.touches.Length > 0) {
            Touch t = Input.touches[0];
            pos = Camera.main.ScreenToWorldPoint(t.position);
        }
        return pos;
    }

    Vector2 GetPosition() {
#if UNITY_STANDALONE || UNITY_EDITOR
            return GetMousePosition();
#elif UNITY_ANDROID
		    return GetTouchPosition();
#endif
    }

    RaycastHit2D GetRayCast() {
        return Physics2D.Raycast(GetPosition(), Vector3.back, 0.2f, attackableLayer);
    }


    void Update() {
        if (GameControl.canAttack())
            CheckTap();
    }
}
