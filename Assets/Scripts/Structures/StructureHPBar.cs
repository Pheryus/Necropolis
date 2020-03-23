using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureHPBar : MonoBehaviour {
    private Vector2 positionCorrection = new Vector2(-10, 130f);

    public Image bar;


    private Structure structure;

    private RectTransform targetCanvas;
    private RectTransform healthBar;
    private Transform objectToFollow;

    public void SetHealthBarData(Transform targetTransform, RectTransform healthBarPanel) {
        targetCanvas = healthBarPanel;
        healthBar = GetComponent<RectTransform>();
        objectToFollow = targetTransform;
        RepositionHealthBar();
        healthBar.gameObject.SetActive(true);
    }

    public void SetStructure(Structure s) {
        structure = s;
    }

    private void Update() {
        bar.fillAmount = (float)structure.health / (float)structure.GetActualHealth();
        RepositionHealthBar();
    }

    public void OnHealthChanged(float healthFill) {
        bar.fillAmount = healthFill;
    }

    /// <summary>
    /// Reposiciona a barra de vida do inimigo convertendo as coordenadas do canvas com a do inimigo.
    /// </summary>
    private void RepositionHealthBar() {
        float side = 1;
        if (objectToFollow.eulerAngles.y == 180)
            side = -1;

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(objectToFollow.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f) + positionCorrection.x * side),
        ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)) + positionCorrection.y);
        //now you can set the position of the ui element
        healthBar.anchoredPosition = WorldObject_ScreenPosition;
    }
}
