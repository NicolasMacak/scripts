using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Help;
using static SoundController;

public class NoteController : MonoBehaviour
{
    public TextMeshProUGUI note;

    private readonly Dictionary<string, string> notes = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        initNotes();
        this.gameObject.SetActive(false);
        note.text = "";
    }

    private void initNotes()
    {
        notes.Add("Note01", "Neuveritelne poznamky @ ohoho ohoho oho @ hhhhh");
    }

    private string getNote(string noteName)
    {
        var note = notes.ContainsKey(noteName) ? notes[noteName] : "";

        return note.Replace("@", System.Environment.NewLine);
    }

    /// <summary>Displays note for user</summary>
    public bool displayNote(string noteName)
    {
        this.gameObject.SetActive(true);
        note.text = getNote(noteName);
        Time.timeScale = 0; // stop game while reading
        ChangeAudioState(AudioState.Pause);
        return true;
    }

    public bool hideNote()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        ChangeAudioState(AudioState.Play);
        return false;
    }
}
