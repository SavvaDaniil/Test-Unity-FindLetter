using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Factory
{
    class ButtonCardDataFactory : ScriptableObject
    {
        public Button create(GameObject parentPanel, GameObject btnCardPrefab, CardData cardData)
        {
            GameObject btnGO = Instantiate(btnCardPrefab, parentPanel.transform);
            
            Button button = btnGO.GetComponent<Button>();
            //button._identifier = cardData.Identifier;
            button.GetComponent<Image>().sprite = cardData.sprite;
            //ButtonCardData buttonCardData = (ButtonCardData)button;
            button.GetComponent<ButtonCardData>()._identifier = cardData.Identifier;
            //button.onClick.AddListener(() => Debug.Log("Проверка"));

            return button;
        }
    }
}
