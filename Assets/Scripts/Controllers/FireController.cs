using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Collider))]
public sealed class FireController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_flameParticleSystem;

    [SerializeField]
    private ParticleSystem m_smokeParticleSystem;

    private int m_triggersCount;

    private SerializedObject flamePart;

    private void Awake()
    {
        m_flameParticleSystem.Play();
        m_smokeParticleSystem.Stop();
        flamePart = new SerializedObject(m_flameParticleSystem);
    }

    private void OnTriggerEnter(Collider other)
    {

        flamePart.FindProperty("ShapeModule.angle").floatValue = 75.0f;
        flamePart.ApplyModifiedProperties();
        m_flameParticleSystem.Emit(200);

        flamePart.FindProperty("EmissionModule.rateOverTime.scalar").floatValue = 10.0f;
        flamePart.ApplyModifiedProperties();

        if (m_triggersCount++ <= 0)
            m_smokeParticleSystem.Play();
    }

    private void OnTriggerExit(Collider other)
    {

         flamePart.FindProperty("ShapeModule.angle").floatValue = 0.0f;
         flamePart.FindProperty("EmissionModule.rateOverTime.scalar").floatValue = 400.0f;
         flamePart.ApplyModifiedProperties();


        if (--m_triggersCount <= 0)
            m_smokeParticleSystem.Stop();
    }
}

