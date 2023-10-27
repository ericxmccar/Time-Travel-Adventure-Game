using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBasic : MonoBehaviour
{
    public TMP_Text ui;
    public Player player;

    void Start()
    {
        ui = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        ui.text = "HP: " + player.hp + " SP: " + player.timeMeter;
    }
}
