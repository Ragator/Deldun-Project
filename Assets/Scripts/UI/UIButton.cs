using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIButton : MonoBehaviour
{
    private Button myButton;

    protected virtual void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ButtonPressed);
    }

    protected abstract void ButtonPressed();
}
