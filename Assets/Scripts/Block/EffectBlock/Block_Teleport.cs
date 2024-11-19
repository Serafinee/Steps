using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Teleport : MonoBehaviour
{
    public GameObject newLandingSpot;


    //--------------------


    private void OnEnable()
    {
        Player_Movement.Action_StepTaken += TeleportPlayer;
    }

    private void OnDisable()
    {
        Player_Movement.Action_StepTaken -= TeleportPlayer;
    }


    //--------------------


    void TeleportPlayer()
    {
        if (PlayerManager.Instance.block_StandingOn_Current.block == gameObject && newLandingSpot)
        {
            StartCoroutine(TeleportWait(0.01f));
        }
    }

    IEnumerator TeleportWait(float waitTime)
    {
        PlayerManager.Instance.isTeleporting = true;
        PlayerManager.Instance.pauseGame = true;
        Player_Movement.Instance.movementStates = MovementStates.Moving;

        yield return new WaitForSeconds(waitTime);

        Vector3 newPos = gameObject.GetComponent<Block_Teleport>().newLandingSpot.transform.position;

        PlayerManager.Instance.player.transform.position = new Vector3(newPos.x, newPos.y + PlayerManager.Instance.player.GetComponent<Player_Movement>().heightOverBlock, newPos.z);

        yield return new WaitForSeconds(waitTime);

        Player_Movement.Instance.movementStates = MovementStates.Still;
        PlayerManager.Instance.isTeleporting = false;
        PlayerManager.Instance.pauseGame = false;
    }
}
