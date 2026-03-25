using Interfaces;
using UnityEngine;

public class SingleCardZone : CardZone
{
    private CardEntity _thisCard;
    public override Transform GetTransformForCard(CardEntity card)
    {
        return transform;
    }

    public override void CardEnter(CardEntity card)
    {
        _thisCard = card;
        card.SetTarget(GetTransformForCard(card));
    }

    public override void InjectCard(CardEntity card)
    {
        _thisCard = null;
    }

    public override void OnClick(Vector3 worldPosition)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDrag(Vector3 worldPosition)
    {
        throw new System.NotImplementedException();
    }

    public override void OnHover(Vector3 worldPosition)
    {
        throw new System.NotImplementedException();
    }

    public override void OnRelease(Vector3 point, IClickable3D clickable)
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitZone()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnterZone()
    {
        throw new System.NotImplementedException();
    }
}