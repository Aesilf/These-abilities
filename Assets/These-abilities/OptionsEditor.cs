using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace These_abilities
{
    public class OptionsEditor : EditorWindow
    {
        private bool _isColorBlindnessEnabled;

        private ConfigOptions _configOptions;

        private Dictionary<string, Material> _colorBlindnessOptions = new Dictionary<string, Material>()
        {
            {"Trichromatic", null},
            {"Protanopia", null},
            {"Deuteranopia", null},
            {"Tritanopia", null}
        };
        [MenuItem("Window/These-abilities")]
        public static void ShowWindow()
        {
            GetWindow<OptionsEditor>("These-abilities options");
        }

        private void OnEnable()
        {
            _configOptions = Resources.Load<ConfigOptions>("DefaultOptions");
            if (_configOptions.ColorBlindnessOptions != null)
            {
                _colorBlindnessOptions = _configOptions.ColorBlindnessOptions;
            }
        }

        private void OnDisable()
        {
            var saveColorBlindnessOptions = _isColorBlindnessEnabled;
            var materials = _colorBlindnessOptions.Values.ToList();
            var allColorMaterialNull = true;
            foreach (var mat in materials)
            {
                if (mat == null) continue;
                allColorMaterialNull = false;
                break;
            }

            if (saveColorBlindnessOptions && !allColorMaterialNull)
            {
                _configOptions.ColorBlindnessOptions = _colorBlindnessOptions;
            }
            
        }

        private void OnGUI()
        {
            _isColorBlindnessEnabled = GUILayout.Toggle(_isColorBlindnessEnabled, "Enable color blindness");
            if (_isColorBlindnessEnabled)
            {
                EditorGUILayout.LabelField("Color blindness", EditorStyles.boldLabel);
                List<string> keys = new List<string>(_colorBlindnessOptions.Keys);

                foreach (var key in keys)
                {
                    EditorGUILayout.BeginHorizontal();

                    // Display the key
                    EditorGUILayout.LabelField(key, GUILayout.Width(150));

                    // Display the material field for the value
                    _colorBlindnessOptions[key] = (Material)EditorGUILayout.ObjectField(
                        _colorBlindnessOptions[key],
                        typeof(Material),
                        false
                    );

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}
