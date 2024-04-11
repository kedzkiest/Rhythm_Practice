/* GuideSoundGenerator.cs
 * 
 * The script for making a guide sound corresponding to the notes placed
 * 
 */

using System.Collections;
using UnityEngine;

public class GuideSoundGenerator : SingleTonMonoBehaviour<GuideSoundGenerator>
{
    [SerializeField] private AudioSource audioSource;
    [Space(20)]
    [SerializeField] private WwisePost quarter_note;
    [SerializeField] private WwisePost eighth_note;
    [SerializeField] private WwisePost triplet_note;
    [SerializeField] private WwisePost eighth_then_double16th_note;
    [SerializeField] private WwisePost sixteenth_then_8th_then_16th_note;
    [Space(20)]
    [SerializeField] private float guideSoundOffsetSecond;

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
        if (placedNotes == null) return;
        if (placedNotes[currentAccent - 1] == null) return;

        prevAccent = currentAccent;
        string notesType = placedNotes[currentAccent - 1].name;

        PlayGuideSound(notesType);
    }

    private void PlayGuideSound(string notesType)
    {
        string expectNoteType;

        // expect "quarter_note(Clone)"
        expectNoteType = "quarter_note";
        if (isExpectNote(notesType, expectNoteType)) StartCoroutine(WaitThenPlay(quarter_note));

        // expect "8th_note(Clone)"
        expectNoteType = "8th_note";
        if (isExpectNote(notesType, expectNoteType)) StartCoroutine(WaitThenPlay(eighth_note));

        // expect "triplet_note(Clone)"
        expectNoteType = "triplet_note";
        if (isExpectNote(notesType, expectNoteType)) StartCoroutine(WaitThenPlay(triplet_note));

        // expect "8th_then_double16th_note(Clone)"
        expectNoteType = "8th_then_double16th_note";
        if (isExpectNote(notesType, expectNoteType)) StartCoroutine(WaitThenPlay(eighth_then_double16th_note));

        // expect "16th_then_8th_then_16th_note(Clone)"
        expectNoteType = "16th_then_8th_then_16th_note";
        if (isExpectNote(notesType, expectNoteType)) StartCoroutine(WaitThenPlay(sixteenth_then_8th_then_16th_note));
    }

    private IEnumerator WaitThenPlay(WwisePost _post)
    {
        yield return new WaitForSeconds(guideSoundOffsetSecond);
        _post.Event.Post(_post.gameObject);
    }

    private bool isExpectNote(string getNoteType, string expectNoteType)
    {
        // Here it compares the length of getNoteType and expectNoteType+7.
        // This is because getNoteType is the name of the note gameobject instantiated, so it has (Clone) suffix.
        // For example, "quarter_note(Clone)".
        return getNoteType.Length == expectNoteType.Length + 7 && getNoteType.Contains(expectNoteType);
    }

    public void SetAudioSourcePitch(float pitch)
    {
        audioSource.pitch = pitch;
    }
}
