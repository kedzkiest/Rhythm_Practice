using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BPMSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { Apply(); });
    }

    private void Update()
    {
        // Support a subtle value adjustment.
        // More dynamic value change can be done by dragging the slider.
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slider.value += 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slider.value -= 1;
        }
    }

    private void Apply()
    {
        // Avoid this slider kept focused.
        // SliderFocused + KeyPress makes unwanted result.
        EventSystem.current.SetSelectedGameObject(null);

        double bpm = Mathf.Ceil(slider.value);
        RhythmManager.Instance.SetBPM(bpm);

        if (GuideSoundGenerator.Instance == null) return;
        GuideSoundGenerator.Instance.SetAudioSourcePitch((float)bpm / 120.0f);
    }
}
