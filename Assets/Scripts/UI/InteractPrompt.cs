using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private Keybinds myKeybinds;
    [SerializeField] private Action myAction;
    [SerializeField] private string textBefore;
    [SerializeField] private string textAfter;

    private TextMeshProUGUI myText;

    private void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        myKeybinds.onKeybindChangedCallback += UpdateKey;
    }

    private void UpdateKey()
    {
        myText.text = (textBefore + myKeybinds.keybinds[myAction].ToString() + textAfter);
    }
}