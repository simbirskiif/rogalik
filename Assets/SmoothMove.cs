using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField]private float TiltAmount = 20;
    [SerializeField] private float rotationSpeed = 0.3f;
    private Vector3 velocity = Vector3.zero;
    Vector3 lastPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 velocity = (target.position - lastPos);
        lastPos = target.position;
        float targetTilt = -velocity.x * TiltAmount;
        targetTilt = Mathf.Clamp(targetTilt, -TiltAmount, TiltAmount);
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetTilt);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }
}
