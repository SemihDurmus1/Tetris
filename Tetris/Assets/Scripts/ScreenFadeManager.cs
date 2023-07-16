using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeManager : MonoBehaviour
{
    public float baslangicAlpha = 1.0f;
    public float bitisAlpha = 0f;

    public float beklemeSuresi = 0;
    public float fadeSuresi;


    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = baslangicAlpha;
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(beklemeSuresi);
        GetComponent<CanvasGroup>().DOFade(bitisAlpha, fadeSuresi);

    }
}
