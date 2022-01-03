using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlayerDetection : MonoBehaviour
{
    [SerializeField]ElevatorController elevatorController;

    private void OnTriggerEnter(Collider other)
    {
        elevatorController.isPlayerBetweenDoors = true;
    }

    private void OnTriggerExit(Collider other)
    {
        elevatorController.isPlayerBetweenDoors = false;
    }
}
