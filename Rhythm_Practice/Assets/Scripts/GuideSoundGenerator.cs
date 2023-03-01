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
        string expectNoteType;

        // expect "quarter_note(Clone)" (19 in length)
        expectNoteType = "quarter_note";
        if (isExpectNote(noteType, expectNoteType))  audioSource.PlayOneShot(quarter_note);

        // expect "8th_note(Clone)" (15 in length)
        expectNoteType = "8th_note";
        if (isExpectNote(noteType, expectNoteType))  audioSource.PlayOneShot(eighth_note);

        // expect "triplet_note(Clone)" (19 in length)
        expectNoteType = "triplet_note";
        if (isExpectNote(noteType, expectNoteType))  audioSource.PlayOneShot(triplet_note);

        // expect "8th_then_double16th_note(Clone)" (31 in length)
        expectNoteType = "8th_then_double16th_note";
        if (isExpectNote(noteType, expectNoteType))  audioSource.PlayOneShot(eighth_then_double16th_note);

        // expect "double16th_then_8th_note(Clone)" (31 in length)
        expectNoteType = "double16th_then_8th_note";
        if (isExpectNote(noteType, expectNoteType))  audioSource.PlayOneShot(double16th_then_eighth_note);
    }

    private bool isExpectNote(string getNoteType, string expectNoteType)
    {
        // Here it compares the length of getNoteType and expectNoteType+7.
        // This is because getNoteType is the name of the note gameobject instantiated, so it has (Clone) suffix.
        // For example, "quarter_note(Clone)".
        return getNoteType.Length == expectNoteType.Length + 7 && getNoteType.Contains(expectNoteType);
    }
}
