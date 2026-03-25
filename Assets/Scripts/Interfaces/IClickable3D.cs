

using UnityEngine;

namespace Interfaces
{
    public interface IClickable3D
    {
        void OnClick(Vector3 worldPosition);
        void OnDrag(Vector3 worldPosition);
        void OnRelease();
        void OnExitZone();
        void OnEnterZone();
    }
}