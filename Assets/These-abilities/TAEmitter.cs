using UnityEngine;

public class TAEmitter : MonoBehaviour
{
    public float intensity = 1.0f; // How loud the sound is
    public float frequency = 440f; // Frequency in Hz (example: A4 note)

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
