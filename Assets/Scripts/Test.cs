using Entity;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject _cardPrefab;
    [SerializeField] CardZone _cardZone;

    public void Add()
    {
        var card = Instantiate(_cardPrefab, Vector3.zero, Quaternion.identity);  
        card.GetComponent<CardVisual>().setUpdated(ScriptableObject.CreateInstance<CardConfig>());
        _cardZone.CardEnter(card.GetComponent<CardEntity>());
    }
}