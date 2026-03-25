using System;
using System.Collections.Generic;
using Entity;
using JetBrains.Annotations;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    public List<CardEntity> movementPairs = new List<CardEntity>();

    public void AddCard(CardConfig card, Transform target)
    {
        GameObject newCard = Instantiate(cardPrefab);
        var visual = newCard.GetComponent<CardVisual>();
        visual.setUpdated(card);
        var entity = newCard.GetComponent<CardEntity>();
        entity.SetTarget(target);
        movementPairs.Add(entity);
    }
}