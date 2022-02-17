using Assets.Factory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDataGridAbstractFactory
{
    private List<int> alreadyUsedIndex = new List<int>();

    public List<Button> create(ref GameObject btnCardPrefab, ref GameObject parentPanel, CardBundleData cardBundleData, List<Button> btnCardsPrev = null)
    {
        float screenHeight = ((RectTransform)parentPanel.transform).rect.height;
        float sizeCard = (float)((screenHeight - (screenHeight * 0.3)) * 0.3);

        RectTransform rtCard = (RectTransform)btnCardPrefab.transform;
        rtCard.sizeDelta = new Vector2(sizeCard, sizeCard);

        if (cardBundleData == null) return null;
        if(cardBundleData.CardDatas.Length == 0)return null;

        int count = cardBundleData.CardDatas.Length;
        List<Button> buttons = new List<Button>();
        ButtonCardDataFactory buttonCardDataFactory = (ButtonCardDataFactory)ScriptableObject.CreateInstance("ButtonCardDataFactory");

        //Debug.Log("Длина массива с кнопками: " + (btnCardsPrev == null ? 0 : btnCardsPrev.Count));
        int length = (btnCardsPrev == null ? 0 : btnCardsPrev.Count);
        if (length == 0)
        {
            length = 3;
        } else if (length < 4)
        {
            length = 6;
        } else if (length < 7)
        {
            length = 9;
        }


        List<CardData> randomCards = getRandomCards(cardBundleData.CardDatas, length);
        foreach (CardData cardData in randomCards)
        {
            //Debug.Log("Случайный идентификатор: " + cardData.Identifier);
            buttons.Add(buttonCardDataFactory.create(parentPanel, btnCardPrefab, cardData));
        }

        return buttons;
    }



    private List<CardData> getRandomCards(CardData[] cardDatas, int length)
    {
        List<CardData> cards = new List<CardData>();
        int indexOfNewCard = 0;
        for(int i = 0; i < length; i++)
        {
            do
            {
                indexOfNewCard = getRandomIndex(cardDatas);
            } while (alreadyUsedIndex.Contains(indexOfNewCard));

            cards.Add(cardDatas[indexOfNewCard]);
            alreadyUsedIndex.Add(indexOfNewCard);
        }
        return cards;
    }

    private int getRandomIndex(CardData[] cardDatas) => Random.Range(0, cardDatas.Length);
}
