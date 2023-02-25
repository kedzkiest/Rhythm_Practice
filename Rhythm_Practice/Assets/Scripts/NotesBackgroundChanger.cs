using UnityEngine;
using UnityEngine.UI;

public class NotesBackgroundChanger : MonoBehaviour
{
    [SerializeField] Image[] notesBackgrounds;
    [SerializeField] Color normalColor;
    [SerializeField] Color highlightedColor;

    // remove highlight from all note's backgrounds
    private void OnDisable()
    {
        for(int i = 0; i < notesBackgrounds.Length; i++)
        {
            notesBackgrounds[i].color = normalColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // highlight the current note's background
        int currentAccent = RhythmManager.Instance.GetCurrentAccent();
        notesBackgrounds[currentAccent - 1].color = highlightedColor;

        // remove highlight from the privious note's background
        // note that currentAccent takes the value from 1 to RhythmManager.signatureHi
        int previousAccent;
        if(currentAccent == 1)
        {
            previousAccent = RhythmManager.Instance.signatureHi;
        }
        else
        {
            previousAccent = currentAccent - 1;
        }
        notesBackgrounds[previousAccent - 1].color = normalColor;
    }
}
