using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject _cardPrefab;
    [SerializeField] CardZone _cardZone;

    public void Add()
    {
        var card = Instantiate(_cardPrefab, Vector3.zero, Quaternion.identity);
        _cardZone.CardEnter(card.GetComponent<CardEntity>());
    }
}