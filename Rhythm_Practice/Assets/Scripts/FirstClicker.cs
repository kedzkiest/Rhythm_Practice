/* FirstClicker.cs
 * 
 * The script for playing a special click sound for the first 3~4 click sounds
 * so that users know when it starts.
 * 
 */

using UnityEngine;

public class FirstClicker : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    private int prevClickNum;

    private void OnEnable()
    {
        prevClickNum = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        DoFirstClicks();
    }

    private void DoFirstClicks()
    {
        int clickNum = RhythmManager.Instance.GetFirstClickCount();

        if (clickNum < RhythmManager.Instance.signatureHi+1 && prevClickNum != clickNum)
        {
            prevClickNum = clickNum;

            audioSource.volume = 1;
            audioSource.PlayOneShot(clip);
        }
    }
}
