using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Singleton<Player_Movement>
{
    public static event Action Action_StepTaken;
    public static event Action Action_BodyRotated;
    public static event Action Action_resetBlockColor;

    [Header("Current Movement Cost")]
    public int currentMovementCost;

    [Header("Movement State")]
    public MovementStates movementStates;
    public ButtonsToPress lastMovementButtonPressed;

    [Header("Player Movement over Blocks")]
    [HideInInspector] public float heightOverBlock = 0.95f;
    public float fallSpeed = 6f;

    //Other
    Vector3 endDestination;
    public bool iceGliding;


    //--------------------


    private void Start()
    {
        Action_StepTaken += IceGlide;
    }
    private void Update()
    {
        KeyInputs();

        if (movementStates == MovementStates.Moving && endDestination != (Vector3.zero + (Vector3.up * heightOverBlock)))
        {
            MovePlayer();
        }
        else
        {
            movementStates = MovementStates.Still;
        }

        PlayerHover();
    }


    //--------------------


    void KeyInputs()
    {
        if (movementStates == MovementStates.Moving) { return; }
        if (MainManager.Instance.pauseGame) { return; }
        if (Cameras.Instance.isRotating) { return; }
        if (Player_Interact.Instance.isInteracting) { return; }

        //If pressing UP - Movement
        if (Input.GetKey(KeyCode.W))
        {
            lastMovementButtonPressed = ButtonsToPress.W;
            MovementKeyIsPressed(MainManager.Instance.canMove_Forward, MainManager.Instance.block_Vertical_InFront, 0);
        }

        //If pressing DOWN - Movement
        else if (Input.GetKey(KeyCode.S))
        {
            lastMovementButtonPressed = ButtonsToPress.S;
            MovementKeyIsPressed(MainManager.Instance.canMove_Back, MainManager.Instance.block_Vertical_InBack, 180);
        }

        //If pressing LEFT - Movement
        else if (Input.GetKey(KeyCode.A))
        {
            lastMovementButtonPressed = ButtonsToPress.A;
            MovementKeyIsPressed(MainManager.Instance.canMove_Left, MainManager.Instance.block_Vertical_ToTheLeft, -90);
        }

        //If pressing RIGHT - Movement
        else if (Input.GetKey(KeyCode.D))
        {
            lastMovementButtonPressed = ButtonsToPress.D;
            MovementKeyIsPressed(MainManager.Instance.canMove_Right, MainManager.Instance.block_Vertical_ToTheRight, 90);
        }


        //--------------------


        //If pressing - E - ASCEND
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.GetComponent<Player_Ascend>().playerCanAscend)
            {
                gameObject.GetComponent<Player_Ascend>().Ascend();
            }
        }
        //If pressing - Q - DESCEND
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gameObject.GetComponent<Player_Descend>().playerCanDescend)
            {
                gameObject.GetComponent<Player_Descend>().Descend();
            }
        }

        //If pressing - Dash - Space
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameObject.GetComponent<Player_Dash>().playerCanDash)
            {
                gameObject.GetComponent<Player_Dash>().Dash();
            }
        }

        //If pressing - Hammer - Enter
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (gameObject.GetComponent<Player_Hammer>().playerCanHammer)
            {
                gameObject.GetComponent<Player_Hammer>().Hammer();
            }
        }
    }
    void MovementKeyIsPressed(bool canMove, DetectedBlockInfo block_Vertical, int rotation)
    {
        if (canMove)
        {
            if (block_Vertical != null)
            {
                if (block_Vertical.block != null)
                {
                    if (block_Vertical.block.GetComponent<BlockInfo>())
                    {
                        MainManager.Instance.block_MovingTowards = block_Vertical;

                        block_Vertical.block.GetComponent<BlockInfo>().movementCost = block_Vertical.block.GetComponent<BlockInfo>().movementCost;

                        endDestination = block_Vertical.blockPosition + (Vector3.up * heightOverBlock);
                        //SetPlayerBodyRotation(rotation);
                        movementStates = MovementStates.Moving;

                        Action_resetBlockColor?.Invoke();
                    }
                }
            }
        }

        SetPlayerBodyRotation(rotation);
    }
    void SetPlayerBodyRotation(int rotationValue)
    {
        //Set new Rotation - Based on the key input
        switch (Cameras.Instance.cameraState)
        {
            case CameraState.Forward:
                MainManager.Instance.playerBody.transform.SetPositionAndRotation(MainManager.Instance.playerBody.transform.position, Quaternion.Euler(0, 0 + rotationValue, 0));
                
                if (rotationValue == 0 || rotationValue == 360)
                    Cameras.Instance.directionFacing = Vector3.forward;
                else if (rotationValue == 180)
                    Cameras.Instance.directionFacing = Vector3.back;
                else if (rotationValue == 90)
                    Cameras.Instance.directionFacing = Vector3.right;
                else if (rotationValue == -90 || rotationValue == 270)
                    Cameras.Instance.directionFacing = Vector3.left;
                break;
            case CameraState.Backward:
                MainManager.Instance.playerBody.transform.SetPositionAndRotation(MainManager.Instance.playerBody.transform.position, Quaternion.Euler(0, 180 + rotationValue, 0));
                
                if (180 + rotationValue == 0 || 180 + rotationValue == 360)
                    Cameras.Instance.directionFacing = Vector3.back;
                else if (180 + rotationValue == 180)
                    Cameras.Instance.directionFacing = Vector3.forward;
                else if (180 + rotationValue == 90)
                    Cameras.Instance.directionFacing = Vector3.left;
                else if (180 + rotationValue == -90 || 180 + rotationValue == 270)
                    Cameras.Instance.directionFacing = Vector3.right;
                break;
            case CameraState.Left:
                MainManager.Instance.playerBody.transform.SetPositionAndRotation(MainManager.Instance.playerBody.transform.position, Quaternion.Euler(0, 90 + rotationValue, 0));
                
                if (90 + rotationValue == 0 || 90 + rotationValue == 360)
                    Cameras.Instance.directionFacing = Vector3.left;
                else if (90 + rotationValue == 180)
                    Cameras.Instance.directionFacing = Vector3.right;
                else if (90 + rotationValue == 90)
                    Cameras.Instance.directionFacing = Vector3.forward;
                else if (90 + rotationValue == -90 || 90 + rotationValue == 270)
                    Cameras.Instance.directionFacing = Vector3.back;
                break;
            case CameraState.Right:
                MainManager.Instance.playerBody.transform.SetPositionAndRotation(MainManager.Instance.playerBody.transform.position, Quaternion.Euler(0, -90 + rotationValue, 0));
                
                if (-90 + rotationValue == 0 || -90 + rotationValue == 360)
                    Cameras.Instance.directionFacing = Vector3.right;
                else if (-90 + rotationValue == 180 || -90 + rotationValue == -180)
                    Cameras.Instance.directionFacing = Vector3.left;
                else if (-90 + rotationValue == 90)
                    Cameras.Instance.directionFacing = Vector3.back;
                else if (-90 + rotationValue == -90 || -90 + rotationValue == 270)
                    Cameras.Instance.directionFacing = Vector3.forward;
                break;

            default:
                break;
        }

        Action_BodyRotated?.Invoke();
    }

    void MovePlayer()
    {
        //Move with a set speed
        if (MainManager.Instance.block_MovingTowards != null)
        {
            if (MainManager.Instance.block_MovingTowards.block != null)
            {
                //Check if the block standing on is different from the one entering, to move with what the player stands on
                if (MainManager.Instance.block_StandingOn_Current.block != MainManager.Instance.block_MovingTowards.block && MainManager.Instance.block_StandingOn_Current.block)
                {
                    if (MainManager.Instance.block_MovingTowards.block.GetComponent<BlockInfo>().movementSpeed <= 0)
                        MainManager.Instance.player.transform.position = Vector3.MoveTowards(MainManager.Instance.player.transform.position, endDestination, 5 * Time.deltaTime);
                    else
                    {
                        if (MainManager.Instance.block_StandingOn_Current.block.GetComponent<Block_IceGlide>() && Player_Stats.Instance.stats.abilities.IceSpikes)
                            MainManager.Instance.player.transform.position = Vector3.MoveTowards(MainManager.Instance.player.transform.position, endDestination, (MainManager.Instance.block_StandingOn_Current.block.GetComponent<BlockInfo>().movementSpeed / 2) * Time.deltaTime);
                        else
                            MainManager.Instance.player.transform.position = Vector3.MoveTowards(MainManager.Instance.player.transform.position, endDestination, MainManager.Instance.block_StandingOn_Current.block.GetComponent<BlockInfo>().movementSpeed * Time.deltaTime);

                    }
                }
                else
                {
                    if (MainManager.Instance.block_MovingTowards.block.GetComponent<BlockInfo>().movementSpeed <= 0)
                        MainManager.Instance.player.transform.position = Vector3.MoveTowards(MainManager.Instance.player.transform.position, endDestination, 5 * Time.deltaTime);
                    else
                    {
                        if (MainManager.Instance.block_MovingTowards.block.GetComponent<Block_IceGlide>() && Player_Stats.Instance.stats.abilities.IceSpikes)
                            MainManager.Instance.player.transform.position = Vector3.MoveTowards(MainManager.Instance.player.transform.position, endDestination, (MainManager.Instance.block_MovingTowards.block.GetComponent<BlockInfo>().movementSpeed / 2) * Time.deltaTime);
                        else
                            MainManager.Instance.player.transform.position = Vector3.MoveTowards(MainManager.Instance.player.transform.position, endDestination, MainManager.Instance.block_MovingTowards.block.GetComponent<BlockInfo>().movementSpeed * Time.deltaTime);

                    }
                }
            }
            else
            {
                MainManager.Instance.player.transform.position = Vector3.MoveTowards(MainManager.Instance.player.transform.position, endDestination, 5 * Time.deltaTime);
            }
        }
        else
        {
            MainManager.Instance.player.transform.position = Vector3.MoveTowards(MainManager.Instance.player.transform.position, endDestination, 5 * Time.deltaTime);
        }

        //Snap into place when close enough
        if (Vector3.Distance(MainManager.Instance.player.transform.position, endDestination) <= 0.03f)
        {
            MainManager.Instance.player.transform.position = endDestination;
            movementStates = MovementStates.Still;

            Action_StepTaken?.Invoke();
        }
    }
    void PlayerHover()
    {
        //Don't hover if teleporting
        if (MainManager.Instance.isTeleporting) { return; }

        //Don't fall if moving
        if (movementStates == MovementStates.Moving)
        {
            return;
        }

        //Hover over blocks you're standing on
        else if (movementStates == MovementStates.Still && MainManager.Instance.block_StandingOn_Current.block)
        {
            gameObject.transform.position = MainManager.Instance.block_StandingOn_Current.block.transform.position + (Vector3.up * heightOverBlock);
        }

        //Fall if standing still and no block is under the player
        else if (movementStates == MovementStates.Still && !MainManager.Instance.block_StandingOn_Current.block)
        {
            gameObject.transform.position = gameObject.transform.position + (Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    //Begin Ice Gliding
    void IceGlide()
    {
        if (MainManager.Instance.block_StandingOn_Current.block)
        {
            if (MainManager.Instance.block_StandingOn_Current.block.GetComponent<Block_IceGlide>() && !Player_Stats.Instance.stats.abilities.IceSpikes)
            {
                iceGliding = true;
                Player_Stats.Instance.stats.steps_Current += MainManager.Instance.block_StandingOn_Current.block.GetComponent<BlockInfo>().movementCost;

                switch (lastMovementButtonPressed)
                {
                    case ButtonsToPress.W:
                        if (MainManager.Instance.canMove_Forward)
                            MovementKeyIsPressed(MainManager.Instance.canMove_Forward, MainManager.Instance.block_Vertical_InFront, 0);
                        break;
                    case ButtonsToPress.S:
                        if (MainManager.Instance.canMove_Back)
                            MovementKeyIsPressed(MainManager.Instance.canMove_Back, MainManager.Instance.block_Vertical_InBack, 180);
                        break;
                    case ButtonsToPress.A:
                        if (MainManager.Instance.canMove_Left)
                            MovementKeyIsPressed(MainManager.Instance.canMove_Left, MainManager.Instance.block_Vertical_ToTheLeft, -90);
                        break;
                    case ButtonsToPress.D:
                        if (MainManager.Instance.canMove_Right)
                            MovementKeyIsPressed(MainManager.Instance.canMove_Right, MainManager.Instance.block_Vertical_ToTheRight, 90);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                iceGliding = false;
            }
        }
        else
        {
            iceGliding = false;
        }
    }

    public void Action_StepTakenInvoke()
    {
        Action_StepTaken?.Invoke();
    }
    public void Action_ResetBlockColorInvoke()
    {
        Action_resetBlockColor?.Invoke();
    }
}

public enum MovementStates
{
    Still,
    Moving
}
public enum ButtonsToPress
{
    None,

    W,
    S,
    A,
    D,

    Arrow_Left,
    ArrowRight,

    Space,
    X,
    
}