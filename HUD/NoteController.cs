using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteController : MonoBehaviour
{
    public TextMeshProUGUI note;

    private Dictionary<string, string> notes = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        initNotes();
        this.gameObject.SetActive(false);
        note.text = "";
    }

    private void initNotes()
    {
        notes.Add("Note01", "Neuveritelne poznamky");
    }

    private string getNote(string noteName)
    {
        return notes.ContainsKey(noteName) ? notes[noteName] : "";
    }

    /// <summary>Displays note for user</summary>
    public bool displayNote(string noteName)
    {
        this.gameObject.SetActive(true);
        note.text = getNote(noteName);
        Time.timeScale = 0; // stop game while reading
        return true;
    }

    public bool hideNote()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        return false;
    }
}
