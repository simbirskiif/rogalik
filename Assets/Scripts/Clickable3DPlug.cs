using Interfaces;
using UnityEngine;

public class Clickable3DPlug :MonoBehaviour,  IClickable3D
{
    public void OnClick(Vector3 worldPosition)
    {
        Debug.Log("On Click");
    }

    public void OnDrag(Vector3 worldPosition)
    {
        Debug.Log("On Drag");
    }

    public void OnHover(Vector3 worldPosition)
    {
        Debug.Log("On Hover");
    }

    public void OnRelease(Vector3 point, IClickable3D clickable)
    {
        Debug.Log("On Release");;
    }

    public void OnExitZone()
    {
        Debug.Log("On Exit Zone");
    }

    public void OnEnterZone()
    {
        Debug.Log("On Enter Zone");
    }
}