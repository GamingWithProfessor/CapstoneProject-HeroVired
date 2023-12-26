using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    public Image starImage;

    // Enable or disable the star image
    public void SetStarActive(bool isActive)
    {
        starImage.enabled = isActive;
    }
}
