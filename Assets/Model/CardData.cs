using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "New CardData", menuName = "Card Data", order = 9)]
public class CardData : ScriptableObject
{
    [SerializeField]
    private string _identifier;

    [SerializeField]
    private Sprite _sprite;

    public string Identifier => _identifier;
    public Sprite sprite => _sprite;
}
