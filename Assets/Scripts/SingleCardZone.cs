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
}