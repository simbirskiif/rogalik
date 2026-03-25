

using UnityEngine;

namespace Interfaces
{
    public interface IClickable3D
    {
        void OnClick(Vector3 worldPosition);
        void OnDrag(Vector3 worldPosition);
        void OnRelease(Vector3 point, IClickable3D clickable);
        void OnExitZone();
        void OnEnterZone();
    }
}