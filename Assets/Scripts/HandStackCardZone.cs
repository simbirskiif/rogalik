using System;
using System.Collections.Generic;
using UnityEngine;

public class HandStackCardZone : CardZone
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] List<CardEntity> _cards = new();
    [SerializeField] List<GameObject> _targets = new();
    [SerializeField] private float xStart;
    [SerializeField] private float xEnd;

    private void Start()
    {
        xStart = transform.position.x - transform.lossyScale.x / 2;
        xEnd = transform.position.x + transform.lossyScale.x / 2;
    }

    private void calcXPosition()
    {
    }

    public override Transform GetTransformForCard(CardEntity card)
    {
        var index = _cards.IndexOf(card);
        return _targets[index].transform;
    }

    public override void CardEnter(CardEntity card)
    {
        _cards.Add(card);
        Recalculate();
        ResetPosition();
    }

    public override void InjectCard(CardEntity card)
    {
        _cards.Remove(card);
        Recalculate();
        ResetPosition();
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

    private void ResetPosition()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].SetTarget(GetTransformForCard(_cards[i]));
        }
    }
}