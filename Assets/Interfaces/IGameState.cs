using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IGameState
{
    public void init(Text titleText, Button btn, GameObject canvas, GameObject btnCardPrefab, CardBundleData cardBundleData);
    public void btnAction(Button btn);
}
