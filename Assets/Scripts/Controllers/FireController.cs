using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class FireController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_flameParticleSystem;

    [SerializeField]
    private ParticleSystem m_smokeParticleSystem;

    private int m_triggersCount;
    
    private void Awake()
    {
        m_flameParticleSystem.Play();
        m_smokeParticleSystem.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_triggersCount++ <= 0)
            m_smokeParticleSystem.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (--m_triggersCount <= 0)
            m_smokeParticleSystem.Stop();
    }
}

