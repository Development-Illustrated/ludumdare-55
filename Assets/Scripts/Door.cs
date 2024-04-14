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
        if(DoorType.Exit == doorType)
        {
            if(debugMode){Debug.Log("Door: Activate called");}
            mainMenu.QuitGame();
        }
        else if (DoorType.Start == doorType)
        {
            if(debugMode){Debug.Log("Door: Activate called");}
            mainMenu.NewGame();
        }
    }
}
