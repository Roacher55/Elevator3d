using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElavatorTrigger : MonoBehaviour
{
    private bool playerInTrigger = false;
    [SerializeField] private int floor;
    public ElevatorController elevatorController;
    [SerializeField] GameObject summonText;
    
    void Update()
    {
        if(playerInTrigger && Input.GetKeyDown(KeyCode.E) && !ElevatorController.elevatorIsMoving)
        {
            elevatorController.MoveToFloor(floor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInTrigger = true;
            summonText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTrigger = false;
            summonText.SetActive(false);
        }
    }

}
