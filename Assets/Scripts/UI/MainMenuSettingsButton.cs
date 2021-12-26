using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSettingsButton : UIButton
{
    private MenuManager myMenuManager;

    protected override void Start()
    {
        base.Start();
        myMenuManager = GameObject.FindWithTag(DeldunProject.Tags.UIManager).GetComponent<MenuManager>();
    }

    protected override void ButtonPressed()
    {
        myMenuManager.MainMenuSettingsButtonPressed();
    }
}
