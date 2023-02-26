using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    // all kinds of notes to be placed
    [SerializeField] private GameObject[] allNotes;

    // 3~4 backgrounds for placing notes on
    [SerializeField] private GameObject[] notesBackgrounds;

    // array for containing notes that is placed
    private GameObject[] placedNotes;

    private int clickNum;
    private int prevClickNum;
    private bool firstClickFinished = false;

    private void OnEnable()
    {
        placedNotes = new GameObject[notesBackgrounds.Length];

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
        // if within first few clicks, update all notes
        clickNum = RhythmManager.Instance.GetFirstClickCount();
        if (clickNum < RhythmManager.Instance.signatureHi + 1 && prevClickNum != clickNum)
        {
            prevClickNum = clickNum;
            UpdateNote(clickNum-1);
        }

        if (!firstClickFinished) return;
    }

    private void UpdateNote(int updatePos)
    {
        // pick ohe of the notes randomly and place it to the specified position
        int r = Random.Range(0, allNotes.Length);
        placedNotes[updatePos] = Instantiate(allNotes[r], notesBackgrounds[updatePos].transform.position, Quaternion.identity);
        placedNotes[updatePos].transform.SetParent(notesBackgrounds[updatePos].transform, false);
    }
}
