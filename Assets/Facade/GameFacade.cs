using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameFacade : ScriptableObject
{

    public GameFacade(){}

    public void clearTitle(Text titleText)
    {
        titleText.text = String.Empty;
    }
    public void setTitle(Text titleText, string text)
    {
        titleText.text = "Find " + text;
    }

    public void buttonAnimationWrong(Button btn)
    {
        //button.transform.DOMove();
        btn.transform.DOShakePosition(1, 3, 10, 50, true);
        btn.transform.DOShakePosition(1, 3);
    }

    public void destroyButtons(ref List<Button> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
            DOTween.Kill(buttons[i]);
            DOTween.Kill(buttons[i].transform);
            Destroy(buttons[i].gameObject);
        }
    }

    public void destroyButton(ref Button button)
    {
        button.gameObject.SetActive(false);
        DOTween.Kill(button);
        DOTween.Kill(button.transform);
        Destroy(button.gameObject);
    }
}
