/* SettingMenu.cs
 * 
 * The script for serving the setting menu functionality for users.
 * 
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] Slider bpmSlider;
    [Space(20)]
    [SerializeField] AllNotes allNotes;
    [SerializeField] Toggle quarterNoteToggle;
    [SerializeField] Toggle eighthNoteToggle;
    [SerializeField] Toggle tripletNoteToggle;
    [SerializeField] Toggle eighthThenDouble16thNoteToggle;
    [SerializeField] Toggle double16THTheneighthNoteToggle;

    public void ApplySettings()
    {
        ApplyBPMChange();
        ApplyNoteChange();
    }

    private void ApplyBPMChange()
    {
        RhythmManager.Instance.bpm = Mathf.Ceil(bpmSlider.value);
    }

    private  void ApplyNoteChange()
    {
        List<GameObject> notes = new List<GameObject>();

        if (quarterNoteToggle.isOn) notes.Add(allNotes.allNotes[0]);
        
        if (eighthNoteToggle.isOn) notes.Add(allNotes.allNotes[1]);

        if (tripletNoteToggle.isOn) notes.Add(allNotes.allNotes[2]);

        if (eighthThenDouble16thNoteToggle.isOn) notes.Add(allNotes.allNotes[3]);

        if (double16THTheneighthNoteToggle.isOn) notes.Add(allNotes.allNotes[4]);

        NotesManager.Instance.SetNotesToBeUsed(notes.ToArray());
    }
}
