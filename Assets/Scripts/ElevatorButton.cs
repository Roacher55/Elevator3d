using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorButton : MonoBehaviour
{
    [SerializeField] int floor;
    public ElevatorController elevatorController;
    public List<Button> buttons;

  
    public void SetFloor()
    {
        elevatorController.MoveToFloor(floor);
    }

}
