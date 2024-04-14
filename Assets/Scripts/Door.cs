using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum DoorType
{
    Start,
    Exit
}
public class Door : MonoBehaviour
{

    [SerializeField] private DoorType doorType;
    [SerializeField] private bool debugMode;

    [SerializeField] private MainMenu mainMenu;

    public void Open()
    {
        if(debugMode){Debug.Log("Door: Open called");}

    }

    public void Activate()
    {
        if(debugMode){Debug.Log("Door: Activate called for door type " + doorType);}
        if(doorType == DoorType.Exit)
        {
            mainMenu.QuitGame();
        }
        else if (doorType == DoorType.Start)
        {
            mainMenu.NewGame();
        }
    }
}
