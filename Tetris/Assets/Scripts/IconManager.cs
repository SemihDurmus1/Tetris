using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconManager : MonoBehaviour
{
    public Sprite openIcon;
    public Sprite closedIcon;

    private Image iconImage;

    public bool iconActive = true;

    private void Start()
    {
        iconImage = GetComponent<Image>();

        iconImage.sprite = iconActive ? openIcon : closedIcon;
    }

    public void IconOpenClose(bool iconStatus)
    {
        if (!iconImage || !openIcon || !closedIcon) { return; }

        iconImage.sprite = iconActive ? openIcon : closedIcon;
    }
}
