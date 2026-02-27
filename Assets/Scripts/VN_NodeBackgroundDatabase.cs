using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VN/Node Background Database", fileName = "VN_NodeBackgroundDatabase")]
public class VN_NodeBackgroundDatabase : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        public string nodeName;
        public Sprite background;
    }

    [SerializeField] private List<Entry> entries = new();

    public bool TryGetBackground(string nodeName, out Sprite sprite)
    {
        sprite = null;

        if (string.IsNullOrWhiteSpace(nodeName))
            return false;

        for (int i = 0; i < entries.Count; i++)
        {
            if (string.Equals(entries[i].nodeName, nodeName, StringComparison.OrdinalIgnoreCase))
            {
                sprite = entries[i].background;
                return sprite != null;
            }
        }

        return false;
    }
}