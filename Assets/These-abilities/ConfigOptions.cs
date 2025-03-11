using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "ConfigOptions", menuName = "Scriptable Objects/ConfigOptions")]
public class ConfigOptions : ScriptableObject
{
    public Dictionary<string, Material> ColorBlindnessOptions;
}
