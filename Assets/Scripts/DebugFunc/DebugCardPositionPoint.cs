using UnityEngine;

namespace DebugFunc
{
    public class DebugCardPositionPoint : MonoBehaviour
    
    {
        [SerializeField] private Color color = Color.red;
        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(this.transform.position, 0.2f);
            
        }
    }
}
