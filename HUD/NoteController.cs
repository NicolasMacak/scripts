﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Help;
using static SoundController;
using static Constants;

public class NoteController : MonoBehaviour
{
    public TextMeshProUGUI note;
    public SoundController playerSoundManager;

    private readonly Dictionary<string, string> notes = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        InitNotes();
        this.gameObject.SetActive(false);
        note.text = "";
    }

    private void InitNotes()
    {
        notes.Add(interactObjectNames.NOTEMission, Notes.Mission);
        notes.Add(interactObjectNames.NOTENakabot, Notes.Nakabot);
        notes.Add(interactObjectNames.NOTESyringe, Notes.Syringe);
        notes.Add(interactObjectNames.NOTEBottle, Notes.Bottle);
        notes.Add(interactObjectNames.NOTEPhone, Notes.Phone);
    }

    private string GetNote(string noteName)
    {
        var note = notes.ContainsKey(noteName) ? notes[noteName] : "";

        return note.Replace("@", System.Environment.NewLine);
    }

    /// <summary>Displays note for user</summary>
    public bool DisplayNote(string noteName) 
    {
        this.gameObject.SetActive(true);
        note.text = GetNote(noteName);

        StarPlatinumZaWarudo(); // stops time
        playerSoundManager.PlayClip(SoundName.Read);

        return true;
    }

    public bool HideNote()
    {
        this.gameObject.SetActive(false);
        TimeHasBegunToMoveAgain(); // time flows again
        return false;
    }
}
