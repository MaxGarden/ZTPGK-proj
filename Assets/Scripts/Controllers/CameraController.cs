using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_transformToTrack;

    private void FixedUpdate()
    {
        transform.position = m_transformToTrack.position;
        transform.rotation = Quaternion.Euler(0.0f, m_transformToTrack.rotation.eulerAngles.y, 0.0f);
    }
}
