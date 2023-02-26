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
    [SerializeField] private Slider bpmSlider;
    [Space(20)]
    [SerializeField] private AllNotes allNotes;
    [SerializeField] private Toggle quarterNoteToggle;
    [SerializeField] private Toggle eighthNoteToggle;
    [SerializeField] private Toggle tripletNoteToggle;
    [SerializeField] private Toggle eighthThenDouble16thNoteToggle;
    [SerializeField] private Toggle double16THTheneighthNoteToggle;
    [Space(20)]
    [SerializeField] private ToggleGroup notesHighlightColorToggleGroup;
    [Space(20)]
    [SerializeField] private AllClickSounds allClickSounds;
    [SerializeField] private Dropdown startClickSoundDropDown;
    [SerializeField] private Dropdown generalHiClickSoundDropDown;
    [SerializeField] private Dropdown generalLoClickSoundDropDown;

    private void Start()
    {
        ApplySettings();
    }

    public void ApplySettings()
    {
        ApplyBPMChange();
        ApplyNotesChange();
        ApplyNotesBackgroundColorChange();
        ApplyClickSoundChange();
    }

    private void ApplyBPMChange()
    {
        RhythmManager.Instance.bpm = Mathf.Ceil(bpmSlider.value);
    }

    private  void ApplyNotesChange()
    {
        List<GameObject> notes = new List<GameObject>();

        if (quarterNoteToggle.isOn) notes.Add(allNotes.allNotes[0]);
        
        if (eighthNoteToggle.isOn) notes.Add(allNotes.allNotes[1]);

        if (tripletNoteToggle.isOn) notes.Add(allNotes.allNotes[2]);

        if (eighthThenDouble16thNoteToggle.isOn) notes.Add(allNotes.allNotes[3]);

        if (double16THTheneighthNoteToggle.isOn) notes.Add(allNotes.allNotes[4]);

        NotesManager.Instance.SetNotesToBeUsed(notes.ToArray());
    }

    private void ApplyNotesBackgroundColorChange()
    {
        IEnumerable<Toggle> activeToggles = notesHighlightColorToggleGroup.ActiveToggles();
        foreach(Toggle toggle in activeToggles)
        {
            NotesBackgroundChanger.Instance.SetHighlightedColor(toggle.colors.normalColor);
        }
    }

    private void ApplyClickSoundChange()
    {
        Clicker.Instance.SetStartClickSound(allClickSounds.allClickSounds[startClickSoundDropDown.value]);
        Clicker.Instance.SetGeneralHiClickSound(allClickSounds.allClickSounds[generalHiClickSoundDropDown.value]);
        Clicker.Instance.SetGeneralLoClickSound(allClickSounds.allClickSounds[generalLoClickSoundDropDown.value]);
    }
}
