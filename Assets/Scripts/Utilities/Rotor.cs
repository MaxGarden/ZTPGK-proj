using UnityEngine;

public class Rotor : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_rotationVector;

    private void FixedUpdate() =>
        transform.localRotation *= Quaternion.Euler(m_rotationVector * Time.deltaTime);
}