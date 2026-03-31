using System;
using System.Collections.Generic;
using Exceptions;
using Interfaces;
using UnityEngine;

public class HandStackCardZone : CardZone
{
    public GameObject cardPrefab;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] List<CardEntity> _cards = new();
    [SerializeField] List<GameObject> _targets = new();
    [SerializeField] private float xStart;
    [SerializeField] private float xEnd;
    [SerializeField] private Vector3 selectedOffset;

    [SerializeField] private bool _inDrag = false;
    [SerializeField] private float edgesOffset = 0f;
    [SerializeField] private CardEntity selectedCard;

    private void Start()
    {
        xStart = (transform.position.x - transform.lossyScale.x / 2) + edgesOffset;
        xEnd = (transform.position.x + transform.lossyScale.x / 2) - edgesOffset;
    }
    


    public override Transform GetTransformForCard(CardEntity card)
    {
        var index = _cards.IndexOf(card);
        return _targets[index].transform;
    }

    public override void AddCard(CardEntity card)
    {
        _cards.Add(card);
        Recalculate();
        ResetPosition();
    }

    public override void AddCard(CardEntity card, Vector3 worldPosition)
    {
        int pos = GetNearestIndex(worldPosition);
        _cards.Insert(pos > _cards.Count ? 0 : pos, card);
        Recalculate();
        ResetPosition();
    }

    public override void InjectCard(CardEntity card)
    {
        _cards.Remove(card);
        Recalculate();
        ResetPosition();
    }

    public override void PreInjectCard(CardEntity card)
    {
        if (_cards.Contains(card))
        {
            return;
        }

        throw new CardZoneException();
    }

    public override void PreEnterCard(CardEntity card)
    {
        
    }

    public override void OnClick(Vector3 worldPosition)
    {
        _isDragging = true;
        _draggedCard = _cards[GetNearestIndex(worldPosition)];
    }

    public override void OnDrag(Vector3 worldPosition)
    {
        _inDrag = true;
        Debug.Log(GetNearestIndex(worldPosition));
        RecalculateWithDraggingOffset(worldPosition);
        _draggedCard = _cards[GetNearestIndex(worldPosition)];
    }

    public override void OnHover(Vector3 worldPosition)
    {
        RecalculateWithHoverOffset(worldPosition);
    }

    public override void OnRelease(Vector3 point, IClickable3D clickable)
    {
        _isDragging = false;
        _inDrag = false;
        Recalculate();
        ResetPosition();
        Debug.Log("Release");
        OnEndDrag?.Invoke(_draggedCard, this, clickable, point);
        _draggedCard = null;
    }

    public override void OnExitZone()
    {
        Recalculate();
        if (_draggedCard == null) return;
        {
            OnBeginDrag.Invoke(_draggedCard, this);
        }
    }

    public override void OnEnterZone()
    {
        Debug.Log("EnterZone");
    }

    public void InjectCard(int index)
    {
        _cards.RemoveAt(index);
        Recalculate();
        ResetPosition();
    }

    private void Recalculate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _targets.Clear();

        for (int i = 0; i < _cards.Count; i++)
        {
            var target = Instantiate(targetPrefab, transform);
            target.transform.localScale = Vector3.one;
            float step = _cards.Count > 1 ? (xEnd - xStart) / (_cards.Count - 1) : transform.position.x;
            float xPos = _cards.Count > 1 ? xStart + i * step : transform.position.x;
            target.transform.position = new Vector3(xPos, transform.position.y,
                transform.position.z);
            _targets.Add(target);
            _cards[i].SetTarget(target.transform);
        }
    }

    private void RecalculateWithDraggingOffset(Vector3 worldPosition)
    {
        int j = GetNearestIndex(worldPosition);
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _targets.Clear();
        float offset = cardPrefab.transform.lossyScale.x * 1.5f;
        float step = _cards.Count > 1 ? ((xEnd - xStart) - offset) / (_cards.Count - 1) : transform.position.x;
        for (int i = 0; i < _cards.Count; i++)
        {
            var target = Instantiate(targetPrefab, transform);
            target.transform.localScale = Vector3.one;
            float xPos = _cards.Count > 1 ? (xStart + i * step) : transform.position.x;
            if (i > j) xPos += offset;
            if (i == j)
            {
                xPos += offset / 2;
                target.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            
            target.transform.position = new Vector3(xPos, transform.position.y, transform.position.z) +
                                        (i == j ? selectedOffset : new Vector3());
            _targets.Add(target);
            _cards[i].SetTarget(target.transform);
        }
    }
    private void RecalculateWithHoverOffset(Vector3 worldPosition)
    {
        int j = GetNearestIndex(worldPosition);
    
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _targets.Clear();
        float offset = cardPrefab.transform.lossyScale.x;
        float step = _cards.Count > 1 ? ((xEnd - xStart) - offset) / (_cards.Count - 1) : transform.position.x;
    
        for (int i = 0; i < _cards.Count; i++)
        {
            var target = Instantiate(targetPrefab, transform);
            target.transform.localScale = Vector3.one;
        
            float xPos = _cards.Count > 1 ? (xStart + i * step) : transform.position.x;
            if (i >= j) xPos += offset; // сдвигаем карты начиная с j, освобождая место
        
            target.transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            _targets.Add(target);
            _cards[i].SetTarget(target.transform);
        }
    }

    private void ResetPosition()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].SetTarget(GetTransformForCard(_cards[i]));
        }
    }
    

    private int GetNearestIndex(Vector3 worldPosition)
    {
        float x = worldPosition.x;
        int closestIndex = 0;
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < _targets.Count; i++)
        {
            float dist = Mathf.Abs(_targets[i].transform.position.x - x);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}