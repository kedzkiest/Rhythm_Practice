/* SettingMenu.cs
 * 
 * The script for serving the setting menu functionality for users.
 * 
 */

using System;
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
    [Space(20)]
    [SerializeField] private ToggleGroup beatToggleGroup;
    [SerializeField] private GameObject notesBackground4Partitioned;
    [SerializeField] private GameObject notesBackground3Partitioned;

    public void ApplySettings()
    {
        ApplyNotesChange();
        ApplyNotesBackgroundColorChange();
        ApplyClickSoundChange();
        ApplyNotesChangeFrequencyChange();
        ApplyBeatChange();
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

    private void ApplyNotesChangeFrequencyChange()
    {
        int notesChangeFreqency = DialNumberManager.Instance.GetDialNumber();
        NotesManager.Instance.SetNotesChangeFrequency(notesChangeFreqency);
    }

    private void ApplyBeatChange()
    {
        IEnumerable<Toggle> activeToggles = beatToggleGroup.ActiveToggles();
        foreach (Toggle toggle in activeToggles)
        {
            // the second child of toggle is Label, which contains a text component.
            Text beatText = toggle.gameObject.transform.GetChild(1).GetComponent<Text>();
            int beat = Int32.Parse(beatText.text);
            RhythmManager.Instance.SetBeat(beat);

            GameObject[] notesBackgroundGameobjets = new GameObject[beat];
            Image[] notesBackgroundImages = new Image[beat];
            
            for (int i = 0; i < beat; i++)
            {
                GameObject go = null;
                if(beat == 3) go = notesBackground3Partitioned.transform.GetChild(i).gameObject;
                if(beat == 4) go = notesBackground4Partitioned.transform.GetChild(i).gameObject;


                notesBackgroundGameobjets[i] = go;
                notesBackgroundImages[i] = go.GetComponent<Image>();
            }

            NotesManager.Instance.SetNotesBackgrounds(notesBackgroundGameobjets);
            NotesBackgroundChanger.Instance.SetNotesBackgrounds(notesBackgroundImages);
        }
    }
}
