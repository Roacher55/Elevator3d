using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] Transform elevator;

    [SerializeField] List<float> floors_Position_Y;
    private int currentFloor = 0;
    public static bool elevatorIsMoving = false;
    [SerializeField] float timerCloseDoor = 10f;
    float settimerCloseDoor;
    private bool playerInTrigger = false;
    public bool isPlayerBetweenDoors = false;
    [SerializeField] GameObject openCloseElevatorText;

    [SerializeField] Animator elevatorAnimation;

    [SerializeField] GameObject panel;

    [SerializeField] ElevatorButton elevatorButton;

    [SerializeField] AudioSource elevatorAudio;

    [SerializeField] ElevatorSounds elevatorSounds;

    private void Start()
    {
        settimerCloseDoor = timerCloseDoor;
    }
    private void Update()
    {
        CloseDoorAfterTime();
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && !ElevatorController.elevatorIsMoving)
        {
            MoveToFloor(currentFloor);
        }
    }
    public void MoveToFloor(int targetFloor)
    {
        if (targetFloor == currentFloor)
        {
            OpenCloseDoor();
            return;
        }

        if(isPlayerBetweenDoors)
        {
            OpenDoor();
            return;
        }

        float targetPosition = floors_Position_Y[targetFloor];

        elevatorIsMoving = true;

        if (elevatorAnimation.GetCurrentAnimatorStateInfo(0).IsName("ElavatorAnimationOpen"))
            elevatorAnimation.SetTrigger("CloseDoor");

        elevatorAudio.PlayOneShot(elevatorSounds.startElevatorSound);
        elevatorAudio.DOFade(1f, elevatorSounds.startElevatorSound.length).OnComplete(()=> 
        {
            elevatorAudio.PlayOneShot(elevatorSounds.moveElevatorSound);
        });

        elevator.DOLocalMoveY(targetPosition, Mathf.Abs(targetFloor - currentFloor) * 3).SetDelay(elevatorAnimation.GetCurrentAnimatorStateInfo(0).length * 2).OnComplete(() =>
        {
            elevatorButton.buttons[currentFloor].interactable = true;
            currentFloor = targetFloor;
            elevatorButton.buttons[currentFloor].interactable = false;
            elevatorIsMoving = false;

            elevatorAudio.Stop();
            elevatorAudio.PlayOneShot(elevatorSounds.endElavatorSound);

            elevatorAnimation.SetTrigger("OpenDoor");
        });
    }


    void CloseDoorAfterTime()
    {
        if(elevatorAnimation.GetCurrentAnimatorStateInfo(0).IsName("ElavatorAnimationOpen"))
        {
            timerCloseDoor -= Time.deltaTime;

            if(timerCloseDoor <= 0f && !isPlayerBetweenDoors)
                {
                elevatorAnimation.SetTrigger("CloseDoor");
                timerCloseDoor = settimerCloseDoor;
            }
        }
    }

    void OpenCloseDoor()
    {
        if (elevatorAnimation.GetCurrentAnimatorStateInfo(0).IsName("ElevatorAnimationClose"))
        {
            OpenDoor();
        }
        else if (elevatorAnimation.GetCurrentAnimatorStateInfo(0).IsName("ElavatorAnimationOpen"))
        {
            CloseDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            panel.SetActive(true);
            playerInTrigger = true;
            openCloseElevatorText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            panel.SetActive(false);
            playerInTrigger = false;
            openCloseElevatorText.SetActive(false);
        }
    }

    IEnumerator playElevatorSound()
    {
        elevatorAudio.clip = elevatorSounds.startElevatorSound;
        elevatorAudio.Play();
        yield return new WaitForSeconds(elevatorAudio.clip.length);
        elevatorAudio.PlayOneShot(elevatorSounds.startElevatorSound);
    }

    void OpenDoor()
    {
        elevatorAnimation.SetTrigger("OpenDoor");
    }

    void CloseDoor()
    {
        elevatorAnimation.SetTrigger("CloseDoor");
        timerCloseDoor = settimerCloseDoor;
    }

}
