/* NotesManager.cs
 * 
 * The script for insert/remove notes to/from note's background.
 * 
 */

using UnityEngine;

public class NotesManager : SingleTonMonoBehaviour<NotesManager>
{
    // all kinds of notes to be placed
    private GameObject[] notesToBeUsed;

    // 3~4 backgrounds for placing notes on
    [SerializeField] private GameObject[] notesBackgrounds;

    // the frequency to change one of the notes that is placed
    [SerializeField] private int notesChangeFrequency = 4;

    // array for containing notes that is placed
    private GameObject[] placedNotes;

    private int notesChangeCount;
    private int clickNum;
    private int prevClickNum;
    private bool firstClickFinished = false;

    private void OnEnable()
    {
        placedNotes = new GameObject[notesBackgrounds.Length];

        notesChangeCount = 0;
        prevClickNum = 0;
        firstClickFinished = false;
    }

    // clear all notes when disabled
    private void OnDisable()
    {
        for(int i = 0; i < placedNotes.Length; i++)
        {
            Destroy(placedNotes[i]);
        }

        placedNotes = null;
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
            // care for the current note and replaced note won't be the same position
            int r;
            while (true)
            {
                r = Random.Range(0, notesBackgrounds.Length);
                if (r != clickNum - 1) break;
            }
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

        // pick ohe of the notes randomly and place it to the specified position
        int r = Random.Range(0, notesToBeUsed.Length);
        placedNotes[updatePos] = Instantiate(notesToBeUsed[r], notesBackgrounds[updatePos].transform.position, Quaternion.identity);
        placedNotes[updatePos].transform.SetParent(notesBackgrounds[updatePos].transform, false);
    }

    public void SetNotesToBeUsed(GameObject[] notes)
    {
        notesToBeUsed = notes;
    }
}
