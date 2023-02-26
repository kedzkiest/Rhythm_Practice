/* NoteBackgroundChanger.cs
 * 
 * The script for changing the color of note's background
 * to make it easy to understand the timing.
 */

using UnityEngine;
using UnityEngine.UI;

public class NotesBackgroundChanger : SingleTonMonoBehaviour<NotesBackgroundChanger>
{
    [SerializeField] private Image[] notesBackgrounds;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightedColor;

    // remove highlight from all note's backgrounds
    private void OnDisable()
    {
        for(int i = 0; i < notesBackgrounds.Length; i++)
        {
            notesBackgrounds[i].color = normalColor;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // if within first few clicks, do not highlight
        int clickNum = RhythmManager.Instance.GetFirstClickCount();
        if (clickNum < RhythmManager.Instance.signatureHi + 1) return;

        // highlight the current note's background
        int currentAccent = RhythmManager.Instance.GetCurrentAccent();
        notesBackgrounds[currentAccent - 1].color = highlightedColor;

        // remove highlight from the privious note's background
        // note that currentAccent takes the value from 1 to RhythmManager.signatureHi
        int previousAccent;
        if(currentAccent == 1)
        {
            previousAccent = RhythmManager.Instance.beat;
        }
        else
        {
            previousAccent = currentAccent - 1;
        }
        notesBackgrounds[previousAccent - 1].color = normalColor;
    }

    public void SetHighlightedColor(Color color)
    {
        highlightedColor = color;
    }
}
