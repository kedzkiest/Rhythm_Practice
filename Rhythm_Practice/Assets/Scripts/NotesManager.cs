/* NotesManager.cs
 * 
 * The script for insert/remove notes to/from note's background.
 * 
 */

using UnityEditor.Animations;
using UnityEngine;

public class NotesManager : SingleTonMonoBehaviour<NotesManager>
{
    // all kinds of notes to be placed
    [SerializeField] private GameObject[] notesToBeUsed;

    // 3~4 backgrounds for placing notes on
    [SerializeField] private GameObject[] notesBackgrounds;

    // the frequency to change one of the notes that is placed
    [SerializeField] private int notesChangeFrequency = 4;

    // the animation controller that will be attached to newly added notes
    [SerializeField] private RuntimeAnimatorController newlyAddedNoteAnimationController;

    // the animation that newly added note plays
    [SerializeField] private AnimationClip newlyAddedNoteAnimatiohClip;

    // array for containing notes that is placed
    private GameObject[] placedNotes;

    // array for memorazing note types that was placed
    private int[] placedNoteType;

    private int notesChangeCount;
    private int clickNum;
    private int prevClickNum;
    private bool firstClickFinished = false;

    private int prevUpdatedPos;

    private void OnEnable()
    {
        placedNotes = new GameObject[notesBackgrounds.Length];
        placedNoteType = new int[notesBackgrounds.Length];

        notesChangeCount = 0;
        prevClickNum = 0;
        firstClickFinished = false;

        prevUpdatedPos = -1;
    }

    // clear all notes when disabled
    private void OnDisable()
    {
        for(int i = 0; i < placedNotes.Length; i++)
        {
            Destroy(placedNotes[i]);
        }

        placedNotes = null;
        placedNoteType = null;
    }

    // Update is called once per frame
    private void Update()
    {
        DoFirstNotesUpdate();

        if (!firstClickFinished) return;

        DoGeneralNotesUpdate();
    }

    private void DoFirstNotesUpdate()
    {
        // if within first few clicks, update all notes
        clickNum = RhythmManager.Instance.GetFirstClickCount();
        if (clickNum < RhythmManager.Instance.signatureHi + 1 && prevClickNum != clickNum)
        {
            prevClickNum = clickNum;

            UpdateNote(clickNum - 1);
        }

        firstClickFinished = clickNum < RhythmManager.Instance.signatureHi + 1 ? false : true;
    }

    private void DoGeneralNotesUpdate()
    {
        clickNum = RhythmManager.Instance.GetCurrentAccent();

        if (prevClickNum != clickNum)
        {
            prevClickNum = clickNum;
            notesChangeCount += 1;
        }

        if (notesChangeCount >= notesChangeFrequency)
        {
            notesChangeCount = 0;

            // choose note position to replace
            // note that the current note and replaced note won't be the same
            // and notes replaced one before are not replaced consecutively.
            int r;
            while (true)
            {
                r = Random.Range(0, notesBackgrounds.Length);
                if (r != clickNum - 1 && r != prevUpdatedPos) break;
            }
            prevUpdatedPos = r;
            UpdateNote(r);
        }
    }

    private void UpdateNote(int updatePos)
    {
        if (placedNotes[updatePos] != null)
        {
            Destroy(placedNotes[updatePos]);
        }

        // if no notes set, do nothing
        if (notesToBeUsed.Length == 0) return;

        // pick one of the notes randomly and place it to the specified position
        // at the same time, memorize the type of notes so that they will not be replaced
        // by the same type of notes.
        int r;
        while (true)
        {
            r = Random.Range(0, notesToBeUsed.Length);
            if (r != placedNoteType[updatePos]) break;
        }

        placedNoteType[updatePos] = r;
        placedNotes[updatePos] = Instantiate(notesToBeUsed[r], notesBackgrounds[updatePos].transform.position, Quaternion.identity);
        placedNotes[updatePos].transform.SetParent(notesBackgrounds[updatePos].transform, false);

        // animate newly added notes
        placedNotes[updatePos].AddComponent<Animator>();
        placedNotes[updatePos].GetComponent<Animator>().runtimeAnimatorController = newlyAddedNoteAnimationController;
    }

    public void SetNotesToBeUsed(GameObject[] notes)
    {
        notesToBeUsed = notes;
    }

    public void SetNotesChangeFrequency(int freq)
    {
        notesChangeFrequency = freq;
    }

    public void SetNotesBackgrounds(GameObject[] _notesBackgrounds)
    {
        notesBackgrounds = _notesBackgrounds;
    }

}
