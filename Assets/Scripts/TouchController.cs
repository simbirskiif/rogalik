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

        else if (Input.GetMouseButtonUp(0) && _first != null)
        {
            HandleDrop();
        }
    }

    private void HandlePress()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            _current = hit.collider.GetComponent<IClickable3D>();
            _first = _current;
            _current.OnClick(hit.point);
        }
    }

    private void HandleDrag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            var thisHit = hit.collider.GetComponent<IClickable3D>();
            if (_current != hit.collider.GetComponent<IClickable3D>())
            {
                _last = _current;
                _current = hit.collider.GetComponent<IClickable3D>();
                _last.OnExitZone();
                _current.OnEnterZone();
            }

            if (_current == _first)
            {
                _first.OnDrag(hit.point);
            }
            if (_current != _last && _current != _first && _current != null)
            {
                _current.OnHover(hit.point);
            }
        }
    }

    private void HandleDrop()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Vector3 dropPoint = Vector3.zero;
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            dropPoint = hit.point;
        }

        _first.OnRelease(dropPoint, _current);
        _current = null;
        _last = null;
        _first = null;
    }
}