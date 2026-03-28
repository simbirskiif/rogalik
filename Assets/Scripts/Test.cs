using Entity;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject _cardPrefab;
    [SerializeField] CardZone _cardZone;

    public void Add()
    {
        var card = Instantiate(_cardPrefab, Vector3.zero, Quaternion.identity);  
        card.GetComponent<CardVisual>().setUpdated(new CardConfig(4,3,0.65f,2,1,1));
        _cardZone.CardEnter(card.GetComponent<CardEntity>());
    }
}