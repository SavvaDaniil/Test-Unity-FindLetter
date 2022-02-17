using Assets.Factory;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingState : GameStateAbstract
{

    private int _stepBetweenBtns = 120;
    private string _correctIdentifier = String.Empty;
    private GameFacade _gameFacade;

    public override List<Button> init(
        ref Text titleText,
        ref Button btnMenu,
        ref GameObject btnMenuGO, 
        ref GameObject parentPanel,
        ref GameObject btnCardPrefab, 
        CardBundleData cardBundleData, 
        Action callback, 
        ref List<string> listOfCorrectAnswers,
        ref List<Button> btnCards)
    {
        //Debug.Log("GamePlayingState init");
        //тест
        //ButtonCardDataFactory buttonCardDataFactory = new ButtonCardDataFactory();
        //btnCardPrefab.SetActive(true);
        //Button buttonTest = buttonCardDataFactory.create(btnCardPrefab, cardBundleData.CardDatas[0]);
        btnCardPrefab.SetActive(true);

        //Debug.Log("Длина массива с кнопками: " + (btnCards == null ? 0 : btnCards.Count));

        _gameFacade = (GameFacade)ScriptableObject.CreateInstance("GameFacade");

        _gameFacade.clearTitle(titleText);
        CardDataGridAbstractFactory cardDataGridAbstractFactory = new CardDataGridAbstractFactory();

        btnCards = cardDataGridAbstractFactory.create(
            ref btnCardPrefab,
            ref parentPanel,
            cardBundleData,
            btnCards
        );
        if(btnCards == null)
        {
            titleText.text = "Error: can't find cards";
            return null;
        }

        
        do
        {
            _correctIdentifier = getRandomSuccessfullIdentifier(btnCards);
        } while (listOfCorrectAnswers.Contains(_correctIdentifier));


        listOfCorrectAnswers.Add(_correctIdentifier);
        _gameFacade.setTitle(titleText, _correctIdentifier);

        //меняем расположение кнопок на экране и записываем функцию в них
        Vector3 newPosition;
        //Button buttonForChanging;
        for (int i = 0; i < btnCards.Count; i++)
        {
            Button buttonForChanging = btnCards[i];
            newPosition = getPositionByFunction(parentPanel, btnCards.Count, i);
            buttonForChanging.transform.position = newPosition;
            buttonForChanging.onClick.AddListener(() => btnAction(buttonForChanging, callback));
            btnCards[i] = buttonForChanging;

        }
        return btnCards;
    }

    protected override void btnAction(Button btn, Action callback)
    {
        if(btn.GetComponent<ButtonCardData>()._identifier == _correctIdentifier)
        {
            callback();
        } else
        {
            callError(btn);
        }
    }

    private void callError(Button btn)
    {
        //Debug.Log("Ответ неправильный");
        _gameFacade.buttonAnimationWrong(btn);
    }

    private string getRandomSuccessfullIdentifier(List<Button> buttons) => buttons[UnityEngine.Random.Range(0, buttons.Count)].GetComponent<ButtonCardData>()._identifier;



    private Vector3 getPositionByFunction(GameObject parentPanel, int length, int i)
    {
        RectTransform rtOfParentPanel = (RectTransform)parentPanel.transform;
        float centerX = rtOfParentPanel.rect.width / 2;
        float centerY = rtOfParentPanel.rect.height / 2;

        List<Vector3> arrayOfCoordinates = new List<Vector3>();
        if (length < 4)
        {
            arrayOfCoordinates.Add(new Vector3(centerX - _stepBetweenBtns, centerY, 0));
            arrayOfCoordinates.Add(new Vector3(centerX, centerY, 0));
            arrayOfCoordinates.Add(new Vector3(centerX + _stepBetweenBtns, centerY, 0));
        }
        else if (length < 7)
        {
            arrayOfCoordinates.Add(new Vector3(centerX - _stepBetweenBtns, centerY + _stepBetweenBtns / 2, 0));
            arrayOfCoordinates.Add(new Vector3(centerX, centerY + _stepBetweenBtns / 2, 0));
            arrayOfCoordinates.Add(new Vector3(centerX + _stepBetweenBtns, centerY + _stepBetweenBtns / 2, 0));

            arrayOfCoordinates.Add(new Vector3(centerX - _stepBetweenBtns, centerY - _stepBetweenBtns / 2, 0));
            arrayOfCoordinates.Add(new Vector3(centerX, centerY - _stepBetweenBtns / 2, 0));
            arrayOfCoordinates.Add(new Vector3(centerX + _stepBetweenBtns, centerY - _stepBetweenBtns / 2, 0));
        }
        else
        {
            arrayOfCoordinates.Add(new Vector3(centerX - _stepBetweenBtns, centerY + _stepBetweenBtns, 0));
            arrayOfCoordinates.Add(new Vector3(centerX, centerY + _stepBetweenBtns, 0));
            arrayOfCoordinates.Add(new Vector3(centerX + _stepBetweenBtns, centerY + _stepBetweenBtns, 0));

            arrayOfCoordinates.Add(new Vector3(centerX - _stepBetweenBtns, centerY, 0));
            arrayOfCoordinates.Add(new Vector3(centerX, centerY, 0));
            arrayOfCoordinates.Add(new Vector3(centerX + _stepBetweenBtns, centerY, 0));

            arrayOfCoordinates.Add(new Vector3(centerX - _stepBetweenBtns, centerY - _stepBetweenBtns, 0));
            arrayOfCoordinates.Add(new Vector3(centerX, centerY - _stepBetweenBtns, 0));
            arrayOfCoordinates.Add(new Vector3(centerX + _stepBetweenBtns, centerY - _stepBetweenBtns, 0));
        }


        return arrayOfCoordinates[i];
    }
}
