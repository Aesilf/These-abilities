using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class SoundCompass : MonoBehaviour
{
    public TAListener listener;
    public RectTransform compassUI;
    public GameObject soundIndicatorPrefab; // Prefab for UI indicator
    private List<GameObject> indicators = new List<GameObject>();

    void Update()
    {
        UpdateCompass();
    }

    void UpdateCompass()
    {
        foreach (GameObject indicator in indicators)
        {
            Destroy(indicator);
        }
        indicators.Clear();

        foreach (var sound in listener.detectedSounds.OrderByDescending(s => s.intensity))
        {
            GameObject indicator = Instantiate(soundIndicatorPrefab, compassUI);
            indicators.Add(indicator);

            Vector2 dir = new Vector2(sound.direction.x, sound.direction.z).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            indicator.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -angle);

            float alpha = Mathf.Clamp01(sound.intensity);
            indicator.GetComponent<Image>().color = new Color(1, 0, 0, alpha);
        }
    }
}
