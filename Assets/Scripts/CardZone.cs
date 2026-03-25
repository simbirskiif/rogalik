using System;
using Entity;
using Interfaces;
using UnityEngine;

[Serializable]
public abstract class CardZone : MonoBehaviour, IClickable3D
{
    public abstract Transform GetTransformForCard(CardEntity card);
    public abstract void CardEnter(CardEntity card);
    public abstract void InjectCard(CardEntity card);
    //Implements
    public abstract void OnClick(Vector3 worldPosition);
    public abstract void OnDrag(Vector3 worldPosition);
    public abstract void OnRelease();
    public abstract void OnExitZone();
    public abstract void OnEnterZone();
}
