using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public sealed class CollectionController : MonoBehaviour
{
     private PlayerController m_playerController;
     private PlayerController PlayerController => m_playerController ? m_playerController : m_playerController = GetComponent<PlayerController>();
        
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Collectible"))
            return;

        other.GetComponentInParent<CollectibleController>().Destroy();
        PlayerController.OnScore();
    }
}
