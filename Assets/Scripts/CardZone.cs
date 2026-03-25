using System;
using Entity;
using UnityEngine;

[Serializable]
public abstract class CardZone : MonoBehaviour
{
    public abstract Transform GetTransformForCard(CardEntity card);
    public abstract void CardEnter(CardEntity card);
    public abstract void InjectCard(CardEntity card);
}
