using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorBlindnessInjector : MonoBehaviour
{
    [SerializeField] private UniversalRendererData urd;
    
    [SerializeField] private ColorBlindnessOptions options;
    
    [SerializeField] private Material colorBlindnessMat;

    private FullScreenPassRendererFeature _colorBlindnessFullScreenPass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Awake()
    // {
    // }

    void OnDestroy()
    {
        urd.rendererFeatures.Remove(_colorBlindnessFullScreenPass);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreateColorBlindnessPass(options.ProtanopiaMaterial);
        }
    }

    void CreateColorBlindnessPass(Material colorBlindnessMat)
    {
        if (_colorBlindnessFullScreenPass != null)
        {
            urd.rendererFeatures.Remove(_colorBlindnessFullScreenPass);
        }
        _colorBlindnessFullScreenPass = ScriptableObject.CreateInstance<FullScreenPassRendererFeature>();
        _colorBlindnessFullScreenPass.name = "ColorBlindness Full Screen Pass";
        _colorBlindnessFullScreenPass.fetchColorBuffer = true;
        _colorBlindnessFullScreenPass.injectionPoint =
            FullScreenPassRendererFeature.InjectionPoint.BeforeRenderingPostProcessing;
        _colorBlindnessFullScreenPass.requirements = ScriptableRenderPassInput.None;
        _colorBlindnessFullScreenPass.passMaterial = colorBlindnessMat;

        urd.rendererFeatures.Add(_colorBlindnessFullScreenPass);
        urd.SetDirty();
    }
}
