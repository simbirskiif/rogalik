using UnityEngine;

namespace DebugFunc
{
    public class DebugCardPositionPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(this.transform.position, 0.2f);
        }
    }
}
