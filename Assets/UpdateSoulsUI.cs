using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateSoulsUI : MonoBehaviour {

    public TextMeshProUGUI souls;

    void Update(){
        souls.text = "Souls: " + PlayerData.souls;   
    }
}
