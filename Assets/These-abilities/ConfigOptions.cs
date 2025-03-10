using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigOptions", menuName = "Scriptable Objects/ConfigOptions")]
public class ConfigOptions : ScriptableObject
{
    public Dictionary<string, Material> ColorBlindnessOptions;
}
