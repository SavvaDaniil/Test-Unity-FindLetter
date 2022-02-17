using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMenuFactory
{
    public Button createBtnStart(GameObject btnMenuPrefabInititated)
    {
        Button buttonStart = btnMenuPrefabInititated.GetComponent<Button>();
        buttonStart.GetComponentInChildren<Text>().text = "Start game";
        return buttonStart;
    }
    public Button createBtnRestart(GameObject btnMenuPrefabInititated)
    {
        Button buttonStart = btnMenuPrefabInititated.GetComponent<Button>();
        buttonStart.GetComponentInChildren<Text>().text = "Play again";
        return buttonStart;
    }
}
