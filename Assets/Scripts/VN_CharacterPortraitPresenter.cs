using UnityEngine;
using UnityEngine.UI;

public sealed class VN_CharacterPortraitPresenter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private RectTransform portraitRect;

    [Header("Layout")]
    [Tooltip("How far from centre (X axis) the portrait sits when on left/right.")]
    [SerializeField] private float xOffset = 420f;

    [Header("Debug (Temporary)")]
    [SerializeField] private VN_CharacterDefinitionSO debugCharacter;
    [SerializeField] private bool debugShowOnStart = false;

    private void Reset()
    {
        // Helpful defaults when you add the component.
        portraitImage = GetComponent<Image>();
        portraitRect = GetComponent<RectTransform>();
    }

    private void Awake()
    {
        // Ensure we start hidden (matches milestone 6 expectation).
        Hide();
    }

    private void Start()
    {
        if (debugShowOnStart && debugCharacter != null)
            Show(debugCharacter);
    }

    public void Show(VN_CharacterDefinitionSO def)
    {
        if (def == null)
        {
            Hide();
            return;
        }

        if (portraitImage == null || portraitRect == null)
        {
            Debug.LogError("[VN_CharacterPortraitPresenter] Missing portraitImage/portraitRect reference.", this);
            return;
        }

        if (def.portraitSprite == null)
        {
            Debug.LogWarning($"[VN_CharacterPortraitPresenter] Character '{def.characterId}' has no portraitSprite. Hiding.", this);
            Hide();
            return;
        }

        portraitImage.enabled = true;
        portraitImage.sprite = def.portraitSprite;

        // 1) Position: left/right side of screen via anchoredPosition.x
        var pos = portraitRect.anchoredPosition;
        pos.x = def.defaultScreenRight ? +xOffset : -xOffset;
        portraitRect.anchoredPosition = pos;

        // 2) Flip: artworkFacesRight tells us what direction the source art is facing by default.
        // If the art faces right, we do NOT flip. If it faces left, we flip on X.
        var scale = portraitRect.localScale;
        scale.x = def.artworkFacesRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        portraitRect.localScale = scale;
    }

    public void Hide()
    {
        if (portraitImage != null)
            portraitImage.enabled = false;
    }
}