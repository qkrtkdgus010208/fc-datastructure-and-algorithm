using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StackPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    public delegate void StackPanelNextDelegate();
    public delegate void StackPanelPreviousDelegate();

    public StackPanelNextDelegate stackPanelNextDelegate;
    public StackPanelPreviousDelegate stackPanelPreviousDelegate;

    private int index;

    public int Index
    {
        get => index;

        set
        {
            index = value;
            text.text = index.ToString();
            previousButton.interactable = index > 0;
        }
    }

    public void Next()
    {
        stackPanelNextDelegate?.Invoke();
    }

    public void Previous() 
    { 
        stackPanelPreviousDelegate?.Invoke();
    }
}
