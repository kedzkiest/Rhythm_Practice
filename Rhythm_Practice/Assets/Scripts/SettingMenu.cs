/* SettingMenu.cs
 * 
 * The script for serving the setting menu functionality for users.
 * 
 */

using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] Slider bpmSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // apply bpm change
        RhythmManager.Instance.bpm = Mathf.Ceil(bpmSlider.value);
    }
}
