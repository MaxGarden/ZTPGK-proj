using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class MovementController : MonoBehaviour
{
    [SerializeField]
    private float m_movementVelocity = 1.0f;

    [SerializeField]
    private float m_rotationVelocity = 1.0f;

    [SerializeField]
    private float m_jumpForce = 10.0f;
    
    private Rigidbody m_rigidbody;
    private Rigidbody Rigidbody => m_rigidbody ? m_rigidbody : m_rigidbody = GetComponent<Rigidbody>();

    private float m_lastJumpTime;

    private void LateUpdate()
    {
        if (GameController.Instance.State != GameController.GameState.Game)
            return;
        
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        var vertical = Input.GetAxis("Vertical") * Time.deltaTime;
        var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;

        Rigidbody.AddTorque(transform.TransformDirection(Vector3.forward) * (vertical * m_movementVelocity), ForceMode.Force);

        var myTransform = transform;
        myTransform.rotation *= Quaternion.AngleAxis(m_rotationVelocity * horizontal, transform.InverseTransformDirection(Vector3.up));

        var quaternion = myTransform.localRotation;
        quaternion.eulerAngles = new Vector3(0, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
        myTransform.localRotation = quaternion;
    }
    
    private void HandleJump()
    {
        if (Input.GetButton("Jump") && CanJump())
        {
            Rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_lastJumpTime = Time.realtimeSinceStartup;
        }
    }

    private bool CanJump()
    {
        if (Time.realtimeSinceStartup - m_lastJumpTime <= 0.7f)
            return false;
        
        return Physics.Raycast(transform.position, -Vector3.up, (transform.localScale.y / 2.0f) * 1.34f);
    }
}
