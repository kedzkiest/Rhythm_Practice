using UnityEngine;
using UnityEngine.UI;

public class GuideVolumeSlider : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { Apply(); });
    }
    
    private void Apply()
    {
        AkSoundEngine.SetOutputVolume(0, volumeSlider.value);
    }
}
