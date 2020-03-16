using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer pointer;
    
    // To activate the pointer
    public void SetActivePointer(bool flag)
    {
        pointer.enabled = flag;
    }

    // To set the pointer is a catcher or a player
    public void SetPointerStatus(string command)
    {
        switch(command.ToLower())
        {
            case "player":
                pointer.color = Color.blue;
                break;
            case "catcher":
                pointer.color = Color.red;
                break;
        }
    }
}
