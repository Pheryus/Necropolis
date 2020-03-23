using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour, IPoolObject {

    public Sprite sprite;

    public int health, soul;

    private int actualHealth;

    public GameObject healthBarPrefab;

    GameObject healthBarInstance;

    private StructureHPBar structureHpBar;


    private void GenerateEnemyHealthBar() {

        if (healthBarInstance != null)
            return;

        GameObject canvasObj = (GameObject.FindGameObjectWithTag("Canvas"));

        if (canvasObj == null)
            return;

        RectTransform canvas = canvasObj.GetComponent<RectTransform>();
        healthBarInstance = Instantiate(healthBarPrefab) as GameObject;
        structureHpBar = healthBarInstance.GetComponent<StructureHPBar>();
        structureHpBar.SetHealthBarData(transform, canvas);
        structureHpBar.SetStructure(this);
        healthBarInstance.transform.SetParent(canvas, false);
    }


    public int GetActualHealth() {
        return actualHealth;
    }

    public void OnObjectSpawn() {
        GenerateEnemyHealthBar();
        healthBarInstance.SetActive(true);
        actualHealth = health;
    }

    public void TakeDmg(int dmg) {
        health -= dmg;
        if (health <= 0) {
            PlayerData.souls += soul;
            PlayerAttack.instance.DestroyTarget();
            DestroyStructure();
        }
    }

    void DestroyStructure() {
        gameObject.SetActive(false);
        healthBarInstance.SetActive(false);
    }

}
