using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Collider))]
public sealed class FireController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_flameParticleSystem;

    [SerializeField]
    private ParticleSystem m_smokeParticleSystem;

    [SerializeField]
    private float m_forceMultiplier = 100.0f;

    [SerializeField]
    private float m_forceDamp = 1000.0f;
    
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
        {
            m_smokeParticleSystem.Play();

            var force = Vector3.Normalize(transform.position - other.transform.position) * m_forceMultiplier;
            SetParticleSystemForce(m_flameParticleSystem, force);
            SetParticleSystemForce(m_smokeParticleSystem, force);
        }
    }

    private void OnTriggerExit(Collider other)
    {

         flamePart.FindProperty("ShapeModule.angle").floatValue = 0.0f;
         flamePart.FindProperty("EmissionModule.rateOverTime.scalar").floatValue = 400.0f;
         flamePart.ApplyModifiedProperties();


        if (--m_triggersCount <= 0)
            m_smokeParticleSystem.Stop();
    }

    private void Update()
    {
        ZeroParticleSystemForce(m_flameParticleSystem, m_forceDamp * Time.deltaTime);
        ZeroParticleSystemForce(m_smokeParticleSystem, m_forceDamp * Time.deltaTime);
    }

    private void ZeroParticleSystemForce(ParticleSystem particleSystem, float step)
    {
        var forceModule = particleSystem.forceOverLifetime;

        forceModule.x = step > Mathf.Abs(forceModule.x.constant) ? 0.0f : (forceModule.x.constant - (forceModule.x.constant > 0 ? step : -step));
        forceModule.y = step > Mathf.Abs(forceModule.y.constant) ? 0.0f : (forceModule.y.constant - (forceModule.y.constant > 0 ? step : -step));
        forceModule.z = step > Mathf.Abs(forceModule.z.constant) ? 0.0f : (forceModule.z.constant - (forceModule.z.constant > 0 ? step : -step));
    }
    
    private void SetParticleSystemForce(ParticleSystem particleSystem, Vector3 force)
    {
        var forceModule = particleSystem.forceOverLifetime;

        forceModule.x = force.x * m_forceMultiplier;
        forceModule.y = force.y * m_forceMultiplier;
        forceModule.z = force.z * m_forceMultiplier;
    }
}

