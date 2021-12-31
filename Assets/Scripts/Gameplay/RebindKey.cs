using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RebindKey : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private Button rebindButton;
    [SerializeField] private Action action;
    [SerializeField] private Keybinds myKeybinds;

    private bool waitingForInput = false;

    private void Start()
    {
        rebindButton.onClick.AddListener(ButtonPressed);
        myKeybinds.onKeybindChangedCallback += UpdateKeybind;
        UpdateKeybind();
    }

    private void ButtonPressed()
    {
        GameManager.instance.isInputEnabled = false;
        waitingForInput = true;
        keyText.text = "Waiting for input";
    }

    private void OnGUI()
    {
        if (waitingForInput)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (e.keyCode != KeyCode.None)
                {
                    if (e.keyCode != KeyCode.Escape)
                    {
                        myKeybinds.ModifyKeybind(action, e.keyCode);
                    }
                    waitingForInput = false;
                    UpdateKeybind();
                    StartCoroutine("EnableInput");
                }
            }
        }
    }

    private void UpdateKeybind()
    {
        actionText.text = action.ToString();
        keyText.text = myKeybinds.keybinds[action].ToString();
    }

    private IEnumerator EnableInput()
    {
        yield return new WaitForSeconds(0.001f);
        GameManager.instance.isInputEnabled = true;
    }
}
