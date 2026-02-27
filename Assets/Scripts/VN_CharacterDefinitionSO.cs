using UnityEngine;

[CreateAssetMenu(menuName = "VN/Character Definition", fileName = "VN_CharacterDefinition")]
public class VN_CharacterDefinitionSO : ScriptableObject
{
    [Header("Identity")]
    [Tooltip("String key used to match Yarn speaker name later (e.g. 'CHAR_A', 'NARRATOR', 'Mina').")]
    public string characterId;

    [Header("Art")]
    public Sprite portraitSprite;

    [Header("Orientation Defaults")]
    [Tooltip("True if the original artwork is drawn facing right by default.")]
    public bool artworkFacesRight = true;

    [Tooltip("True = render on the right side of the screen by default.")]
    public bool defaultScreenRight = true;
}