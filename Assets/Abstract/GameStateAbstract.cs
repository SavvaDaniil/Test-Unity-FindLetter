using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameStateAbstract
{
    public abstract List<Button> init(
        ref Text titleText, 
        ref Button btnMenu,
        ref GameObject btnMenuGO,
        ref GameObject canvas,
        ref GameObject btnCardPrefab, 
        CardBundleData cardBundleData, 
        Action callback,
        ref List<string> listOfCorrectAnswers,
        ref List<Button> btnCards
    );
    protected abstract void btnAction(Button btn, Action callback);
}
