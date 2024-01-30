using System;
using UnityEngine;

public class QuestionDialog : MonoBehaviour
{
    public event Action OnYesEvent;
    public event Action OnNoEvent;

    public void Show()
    {
        gameObject.SetActive(true);
        OnYesEvent = null;
        OnNoEvent = null;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnYesButtonClick()
    {
        if (OnYesEvent != null) OnYesEvent();
        Hide();
    }
    public void OnNoButtonClick()
    {
        if (OnNoEvent != null) OnNoEvent();
        Hide();
    }
}
