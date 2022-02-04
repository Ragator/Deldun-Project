using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    right,
    left,
    up,
    down,
    interact,
    inventory,
    dash
}

public class Keybinds : MonoBehaviour
{
    public Dictionary<Action, KeyCode> keybinds = new Dictionary<Action, KeyCode>();

    public delegate void OnKeybindChanged();
    public OnKeybindChanged onKeybindChangedCallback;

    public void Awake()
    {
        keybinds.Add(Action.right, KeyCode.D);
        keybinds.Add(Action.left, KeyCode.Q);
        keybinds.Add(Action.up, KeyCode.Z);
        keybinds.Add(Action.down, KeyCode.S);
        keybinds.Add(Action.interact, KeyCode.E);
        keybinds.Add(Action.inventory, KeyCode.Tab);
        keybinds.Add(Action.dash, KeyCode.Space);

        if (onKeybindChangedCallback != null)
        {
            onKeybindChangedCallback.Invoke();
        }
    }

    public void ModifyKeybind(Action actionToModify, KeyCode newKey)
    {
        keybinds[actionToModify] = newKey;

        if (onKeybindChangedCallback != null)
        {
            onKeybindChangedCallback.Invoke();
        }
    }
}