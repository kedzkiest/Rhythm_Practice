/* GuideSoundGenerator.cs
 * 
 * The script for making a guide sound corresponding to the notes placed
 * 
 */

using UnityEngine;

public class GuideSoundGenerator : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [Space(20)]
    [SerializeField] private AudioClip quarter_note;
    [SerializeField] private AudioClip eighth_note;
    [SerializeField] private AudioClip triplet_note;
    [SerializeField] private AudioClip eighth_then_double16th_note;
    [SerializeField] private AudioClip double16th_then_eighth_note;

    private int currentAccent;
    private int prevAccent;

    private void OnEnable()
    {
        prevAccent = -1;
    }

    // Update is called once per frame
    private void Update()
    {
        currentAccent = RhythmManager.Instance.GetCurrentAccent();

        // make a guide sound only when notes changed
        if (currentAccent == prevAccent) return;

        // do nothing if a note is still not instantiated
        GameObject[] placedNotes = NotesManager.Instance.GetPlacedNotes();
        if (placedNotes[currentAccent - 1] == null) return;

        prevAccent = currentAccent;
        string notesType = placedNotes[currentAccent - 1].name;

        PlayGuideSound(notesType);
    }

    private void PlayGuideSound(string noteType)
    {
        if (noteType.Contains("quarter_note")) audioSource.PlayOneShot(quarter_note);
        if (noteType.Contains("8th_note")) audioSource.PlayOneShot(eighth_note);
        if (noteType.Contains("triplet_note")) audioSource.PlayOneShot(triplet_note);
        if (noteType.Contains("8th_then_double16th_note")) audioSource.PlayOneShot(eighth_then_double16th_note);
        if (noteType.Contains("double16th_then_8th_note")) audioSource.PlayOneShot(double16th_then_eighth_note);

    }
}
