/* ChangeTwoIconsOnSliderValueChange.cs
 * 
 * The script for changing icon according to slider value
 * 
 */

using UnityEngine;
using UnityEngine.UI;

public class ChangeTwoIconsOnSliderValueChange : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int threshold = 0;
    [SerializeField] Image image1;
    [SerializeField] Image image2;

    // Update is called once per frame
    void Update()
    {
        if(slider.value <= threshold)
        {
            image1.enabled = true;
            image2.enabled = false;
        }
        else
        {
            image1.enabled = false;
            image2.enabled = true;
        }
    }
}
