using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSettingsButton : UIButton
{
    private MenuManager myMenuManager;
    private GameObject settingsMenu;

    protected override void Start()
    {
        base.Start();
        myMenuManager = GameObject.FindWithTag(DeldunProject.Tags.UIManager).GetComponent<MenuManager>();
        settingsMenu = myMenuManager.GetSettingsMenu();
    }

    protected override void ButtonPressed()
    {
        myMenuManager.MenuButtonPressed(settingsMenu);
    }
}
