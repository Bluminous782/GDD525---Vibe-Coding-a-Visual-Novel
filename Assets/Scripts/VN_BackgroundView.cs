using UnityEngine;
using UnityEngine.UI;

public class VN_BackgroundView : MonoBehaviour
{
    [SerializeField] private Image targetImage;

    private void Reset()
    {
        targetImage = GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        if (targetImage == null) return;
        targetImage.sprite = sprite;
        targetImage.enabled = sprite != null;
    }
}