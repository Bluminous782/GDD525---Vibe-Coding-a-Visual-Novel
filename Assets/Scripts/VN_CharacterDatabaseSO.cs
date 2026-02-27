using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VN/Character Database", fileName = "VN_CharacterDatabase")]
public class VN_CharacterDatabaseSO : ScriptableObject
{
    [SerializeField] private List<VN_CharacterDefinitionSO> characters = new();

    // Optional: quick lookup cache (built on demand)
    private Dictionary<string, VN_CharacterDefinitionSO> _lookup;

    public VN_CharacterDefinitionSO GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return null;

        BuildLookupIfNeeded();

        _lookup.TryGetValue(id.Trim(), out var def);
        return def;
    }

    private void BuildLookupIfNeeded()
    {
        if (_lookup != null) return;

        _lookup = new Dictionary<string, VN_CharacterDefinitionSO>();

        foreach (var def in characters)
        {
            if (def == null) continue;
            if (string.IsNullOrWhiteSpace(def.characterId)) continue;

            var key = def.characterId.Trim();

            // If duplicates exist, keep the first and warn.
            if (_lookup.ContainsKey(key))
            {
                Debug.LogWarning($"[VN_CharacterDatabase] Duplicate characterId '{key}' in database '{name}'. Keeping first.");
                continue;
            }

            _lookup.Add(key, def);
        }
    }
}