using EasyTransition;
using UnityEngine;
using UnityEngine.UI;

public class VideoScene : MonoBehaviour
{
    public Image holdEnterToSkipImage;
    public TransitionSettings ts;
    private bool skipped = false;
    public string targetSceneName = "Chapter";
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && !skipped)
        {
            holdEnterToSkipImage.fillAmount += Time.deltaTime;

            if (holdEnterToSkipImage.fillAmount > 0.9f && !skipped)
            {
                skipped = true;
                TransitionManager.Instance().Transition(targetSceneName, ts, 1);
            }
        }
        else
        {
            if (holdEnterToSkipImage.fillAmount > 0)
                holdEnterToSkipImage.fillAmount -= Time.deltaTime * 4;
        }
    }
}
