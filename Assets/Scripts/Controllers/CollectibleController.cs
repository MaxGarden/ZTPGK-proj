using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private float m_minimumHeight;

    [SerializeField]
    private float m_maximumHeight;

    [SerializeField]
    private float m_surviveChance = 0.6f;
    
    private void Awake()
    {
        if (Random.value > m_surviveChance)
            DestroyImmediate(gameObject);
    }

    public void Destroy() =>
        Destroy(gameObject);

    private void Start()
    {
        Physics.Raycast(transform.position, -Vector3.up, out var hit);

        var randomHeight = Random.Range(m_minimumHeight, m_maximumHeight);

        var position = transform.position;
        position.y = hit.point.y + randomHeight;
        transform.position = position;
    }
}
