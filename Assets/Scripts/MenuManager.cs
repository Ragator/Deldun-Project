using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject systemMenu;

    public void MenuButtonPressed(GameObject menu)
    {
        if (!menu.activeSelf)
        {
            CloseAllMenus();
        }
        menu.SetActive(!menu.activeSelf);
    }

    public void CloseAllMenus()
    {
        inventoryMenu.SetActive(false);
        systemMenu.SetActive(false);
    }
}
