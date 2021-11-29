using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public CanvasGroup UpgradePanel;

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(UpgradePanel, UpgradePanel.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(UpgradePanel, UpgradePanel.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cgm , float start, float end, float lerpTime = 0.5f)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while(true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start,end,percentageComplete);

            cgm.alpha = currentValue;

            if(percentageComplete >= 1 ) break;

            yield return new WaitForEndOfFrame();   
        }
    }

}
