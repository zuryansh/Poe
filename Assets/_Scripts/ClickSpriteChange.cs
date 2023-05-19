using UnityEngine;
using UnityEngine.UI;

public class ClickSpriteChange : MonoBehaviour
{
    [SerializeField] Transform applyTo;
    [SerializeField] Sprite otherSprite;

    Sprite ogSprite;
    Image buttonImage;

    void Start()
    {
        buttonImage = applyTo.GetComponent<Image>();
        ogSprite = buttonImage.sprite;
    }

    public void SpriteChange()
    {
        if(buttonImage.sprite == ogSprite) buttonImage.sprite = otherSprite;
        else if(buttonImage.sprite == otherSprite) buttonImage.sprite = ogSprite;
    }
}
