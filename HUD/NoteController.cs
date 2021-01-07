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
        return notes.ContainsKey(noteName) ? notes[noteName] : "dd";
    }

    /// <summary>Displays note for user</summary>
    public void displayNote(string noteName)
    {
        this.gameObject.SetActive(true);
        note.text = getNote(noteName);
    }

    public void hideNote()
    {
        this.gameObject.SetActive(false);

    }
}
