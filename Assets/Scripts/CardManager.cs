using System;
using System.Collections.Generic;
using Entity;
using Exceptions;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private Transform _targetTouchPosition;
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

    private void OnEnable()
    {
        CardZone.OnEndDrag += HandleCardDrop;
        CardZone.OnBeginDrag += HandleCardBeginDrop;
        TouchController.OnUpdateTouchPosition +=  HandleChangeTouchPosition;
    }

    private void HandleCardDrop(CardEntity card, CardZone zone, IClickable3D target, Vector3 position)
    {
        try
        {
            zone.PreInjectCard(card);
            if (target is CardZone targetZone && targetZone != zone)
            {
                targetZone.PreEnterCard(card);
                targetZone.AddCard(card, position);
            }
            else
            {
                return;
            }
            zone.InjectCard(card);
        }
        
        catch (CardZoneException e)
        {
            Debug.LogException(e);
            return;
        }
        
    }

    private void HandleCardBeginDrop(CardEntity card, CardZone zone)
    {
        card.SetTarget(_targetTouchPosition);
    }
    private void HandleChangeTouchPosition(Transform touchPosition) => _targetTouchPosition = touchPosition;
}