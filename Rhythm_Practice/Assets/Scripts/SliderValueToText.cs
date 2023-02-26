/* SliderValueToText.cs
 * 
 * The script for visualizing the specified slider's value.
 */

using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.Ceil(slider.value).ToString();
    }
}
