using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : UIButton
{
    [SerializeField] private GameObject myMenu;
    [SerializeField] private MenuManager myMenuManager;
    [SerializeField] private string keyPress;

    private void Update()
    {
        if (!string.IsNullOrEmpty(keyPress))
        {
            if (Input.GetKeyDown(keyPress))
            {
                ButtonPressed();
            }
        }
    }

    protected override void ButtonPressed()
    {
        myMenuManager.MenuButtonPressed(myMenu);
    }
}
