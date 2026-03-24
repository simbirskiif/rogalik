using UnityEngine;

public class SmoothCardMove : MonoBehaviour
{
    public Transform target;

    [SerializeField] float moveSpeed = 0.3f;
    [SerializeField] float rotateSpeed = 0.3f;
    [SerializeField] float maxRotate = 20f;
    [SerializeField] float maxDistance = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed);

        Vector3 direction = target.position - transform.position;
        if(true)
        {
            //direction.Normalize();

            float distance = direction.magnitude;
            float distanceFactor = Mathf.Clamp01(distance / maxDistance);
            float currentMaxRotate = maxRotate * distanceFactor;
            float angleX = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            float angleZ = -Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float rotateX = Mathf.Clamp(angleX, -currentMaxRotate, currentMaxRotate);
            float rotateZ = Mathf.Clamp(angleZ, -currentMaxRotate, currentMaxRotate);

            Quaternion finalRotation = Quaternion.Euler(rotateX, 0, rotateZ);

            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, rotateSpeed);
        }
    }
}
