using UnityEngine;

[CreateAssetMenu(fileName = "ColorBlindnessOptions", menuName = "These-abilities/ColorBlindnessOptions")]
public class ColorBlindnessOptions : ScriptableObject
{
    [SerializeField] private Material _protanopiaMaterial;
    [SerializeField] private Material _deuteranopiaMaterial;
    [SerializeField] private Material _tritanopiaMaterial;
    
    public Material ProtanopiaMaterial => _protanopiaMaterial;
}
