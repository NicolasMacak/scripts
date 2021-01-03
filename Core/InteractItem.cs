using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem
{
    // Objekty v inventary sa budu snapovat s tymi v realite pomocou nazvu

    public string objectName { get; set; } // identifier of object in game
    public string title { get; set; }
    public int type { get; set; }
    public int state { get; set; }

    public class Types
    {
        public const int PRIMARY = 0;
        public const int SECONDARY = 1;
        public const int READABLE = 2;
    }

    public class States
    {
        public const int LOCKED = 0;
        public const int PICKABLE = 1;
        public const int USABLE = 2;
        public const int REMOVED = 3;
    }

    public InteractItem(string objectName, string title, int type, int state)
    {
        this.objectName = objectName;
        this.title = title;
        this.type = type;
        this.state = state;
    }

}
