using UnityEngine;
using System.Collections.Generic;

public class TAListener : MonoBehaviour
{
    public float detectionRadius = 10f; // Radius of hearing
    public LayerMask emitterLayer; // Set to detect only emitters
    public List<SoundData> detectedSounds = new List<SoundData>();

    void Update()
    {
        DetectSounds();
    }

    void DetectSounds()
    {
        detectedSounds.Clear();
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, emitterLayer);

        foreach (Collider hit in hits)
        {
            TAEmitter emitter = hit.GetComponent<TAEmitter>();
            if (emitter != null)
            {
                Vector3 direction = (emitter.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, emitter.transform.position);
                float intensityAtPoint = emitter.intensity / (1f + distance * distance); // Inverse square law

                detectedSounds.Add(new SoundData
                {
                    emitter = emitter,
                    position = emitter.transform.position,
                    direction = direction,
                    distance = distance,
                    intensity = intensityAtPoint,
                    frequency = emitter.frequency
                });
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

[System.Serializable]
public struct SoundData
{
    public TAEmitter emitter;
    public Vector3 position;
    public Vector3 direction;
    public float distance;
    public float intensity;
    public float frequency;
}
