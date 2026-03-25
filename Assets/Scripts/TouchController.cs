using System;
using Interfaces;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    private IClickable3D _current;
    private IClickable3D _last;
    private IClickable3D _first;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandlePress();
        }
        else if (Input.GetMouseButton(0) && _current != null)
        {
            HandleDrag();
        }

        else if (Input.GetMouseButtonUp(0) && _current != null)
        {
            _current.OnRelease();
            _current = null;
            _last = null;
            _first = null;
        }
    }

    private void HandlePress()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            _current.OnClick(hit.point);
            _first = _current;
            _current = hit.collider.GetComponent<IClickable3D>();
        }
    }

    private void HandleDrag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            if (_current != hit.collider.GetComponent<IClickable3D>())
            {
                _last = _current;
                _current = hit.collider.GetComponent<IClickable3D>();
                _last.OnExitZone();
                _current.OnEnterZone();
                return;
            }
            _current.OnDrag(hit.point);
        }
    }
}