/* Clicker.cs
 * 
 * The script for playing a click sound.
 * 
 */

using UnityEngine;

public class Clicker : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip firstClickSound;
    [SerializeField] private AudioClip generalClickSound_Hi;
    [SerializeField] private AudioClip generalClickSound_Lo;
    [SerializeField] private int hiClickSoundPos = 1;

    private int clickNum;
    private int prevClickNum;
    private bool firstClickFinished = false;

    private void OnEnable()
    {
        prevClickNum = 0;
        firstClickFinished = false;
    }

    // Update is called once per frame
    private void Update()
    {
        DoFirstClicks();

        if (!firstClickFinished) return;

        DoGeneralClicks();
    }

    private void DoFirstClicks()
    {
        clickNum = RhythmManager.Instance.GetFirstClickCount();

        if (clickNum < RhythmManager.Instance.signatureHi + 1 && prevClickNum != clickNum)
        {
            prevClickNum = clickNum;

            audioSource.PlayOneShot(firstClickSound);
        }

        firstClickFinished = clickNum < RhythmManager.Instance.signatureHi + 1 ? false : true;
    }

    private void DoGeneralClicks()
    {
        clickNum = RhythmManager.Instance.GetCurrentAccent();

        if (prevClickNum != clickNum)
        {
            prevClickNum = clickNum;

            if (clickNum == hiClickSoundPos)
            {
                audioSource.PlayOneShot(generalClickSound_Hi);
            }
            else
            {
                audioSource.PlayOneShot(generalClickSound_Lo);
            }
        }
    }
}
