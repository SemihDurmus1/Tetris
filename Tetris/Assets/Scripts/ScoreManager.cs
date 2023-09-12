using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int skor = 0;
    private int satirlar;
    private int level = 1;

    public int seviyedekiSatirSayisi = 5;

    private int minSatir = 1;
    private int maxSatir = 5;

    public TextMeshProUGUI satirText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI skorText;

    private void Start()
    {
        ResetFunc();
    }

    void ResetFunc()
    {
        level = 1;

        satirlar = seviyedekiSatirSayisi * level;
    }

    public void SatirSkoru(int n)
    {
        n = Mathf.Clamp(n, minSatir, maxSatir);

        switch (n)
        {
            case 1:
                skor += 30 * level;
                break;
            case 2:
                skor += 50 * level;
                break;
            case 3:
                skor += 150 * level;
                break;
            case 4:
                skor += 500 * level;
                break;
        }

        UpdateText();
    }

    void UpdateText()
    {
        if (skorText)
        {
            skorText.text = skor.ToString();
        }

        if (levelText)
        {
            levelText.text = level.ToString();
        }

        if (satirText)
        {
            satirText.text = satirlar.ToString();
        }
    }

}
