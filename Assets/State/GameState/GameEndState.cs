using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndState : GameStateAbstract
{
    public override List<Button> init(
        ref Text titleText,
        ref Button btnMenu,
        ref GameObject btnMenuGO, 
        ref GameObject canvas, 
        ref GameObject btnCardPrefab, 
        CardBundleData cardBundleData, 
        Action callback,
        ref List<string> listOfCorrectAnswers,
        ref List<Button> btnCards)
    {
        btnMenuGO.SetActive(true);
        titleText.text = "You win";
        BtnMenuFactory btnMenuFactory = new BtnMenuFactory();
        Button newBtnMenu = btnMenuFactory.createBtnRestart(btnMenuGO);
        newBtnMenu.onClick.AddListener(() => btnAction(newBtnMenu, callback));
        btnMenu = newBtnMenu;
        return null;
    }

    protected override void btnAction(Button btn, Action callback)
    {
        callback();
    }
}
