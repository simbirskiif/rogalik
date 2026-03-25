using System;
using Entity;
using Interfaces;
using UnityEngine;

[Serializable]
public abstract class CardZone : MonoBehaviour, IClickable3D
{
    public static Action<CardEntity, CardZone> OnBeginDrag;
    public static Action<CardEntity, CardZone, IClickable3D, Vector3> OnEndDrag;
    
    protected CardEntity _draggedCard;
    
    public abstract Transform GetTransformForCard(CardEntity card);
    public abstract void CardEnter(CardEntity card);
    public abstract void InjectCard(CardEntity card);
    //Implements
    public abstract void OnClick(Vector3 worldPosition);
    public abstract void OnDrag(Vector3 worldPosition);

    public virtual void OnRelease(Vector3 point, IClickable3D clickable)
    {
        if (_draggedCard == null) return;
        OnEndDrag.Invoke(_draggedCard, this, clickable, point);
    }
    public abstract void OnExitZone();
    public abstract void OnEnterZone();
}
