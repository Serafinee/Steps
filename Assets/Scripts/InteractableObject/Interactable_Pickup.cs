using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_Pickup : MonoBehaviour
{
    public Items itemReceived;
    public Abilities abilityReceived;
    public bool goal;


    //--------------------


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            print("Pickup entered");

            GetItems();
            GetAbility();

            //Hide Pickup
            gameObject.SetActive(false);
        }
    }


    //--------------------


    public void ShowPickup()
    {
        gameObject.gameObject.SetActive(true);
    }

    public void GetItems()
    {
        if (PlayerManager.Instance.player)
        {
            if (PlayerManager.Instance.player.GetComponent<PlayerStats>())
            {
                if (PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.itemsGot != null)
                {
                    switch (itemReceived)
                    {
                        case Items.None:
                            break;

                        case Items.Coin:
                            PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.itemsGot.coin += 1 /*itemReceived.amount*/;
                            break;
                        case Items.Collectable:
                            PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.itemsGot.collectable += 1 /*itemReceived.amount*/;
                            break;
                        case Items.IncreaseMaxSteps:
                            PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.steps_Max += 1 /*itemReceived.amount*/;
                            break;

                        default:
                            break;
                    }

                    MarkedAsTaken();

                    PlayerManager.Instance.SavePlayerStats();

                    if (goal)
                    {
                        PlayerManager.Instance.player.GetComponent<Player_Movement>().QuitLevel();
                    }
                }
            }
        }
    }
    void MarkedAsTaken()
    {
        for (int i = 0; i < DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List.Count; i++)
        {
            if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].mapName == SceneManager.GetActiveScene().name)
            {
                switch (itemReceived)
                {
                    case Items.None:
                        break;

                    case Items.Coin:
                        for (int j = 0; j < DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].coinList.Count; j++)
                        {
                            if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].coinList[j].pos.x == gameObject.transform.position.x
                                && DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].coinList[j].pos.z == gameObject.transform.position.z)
                            {
                                DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].coinList[j].isTaken = true;

                                return;
                            }
                        }
                        break;
                    case Items.Collectable:
                        for (int j = 0; j < DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].collectableList.Count; j++)
                        {
                            if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].collectableList[j].pos.x == gameObject.transform.position.x
                                && DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].collectableList[j].pos.z == gameObject.transform.position.z)
                            {
                                DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].collectableList[j].isTaken = true;

                                return;
                            }
                        }
                        break;

                    case Items.IncreaseMaxSteps:
                        for (int j = 0; j < DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].maxStepList.Count; j++)
                        {
                            if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].maxStepList[j].pos.x == gameObject.transform.position.x
                                && DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].maxStepList[j].pos.z == gameObject.transform.position.z)
                            {
                                DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].maxStepList[j].isTaken = true;

                                return;
                            }
                        }
                        break;

                    default:
                        break;
                }

                //If it's a goal
                if (goal)
                {
                    DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].isCompleted = true;
                }
            }
        }
    }
    public void GetAbility()
    {
        switch (abilityReceived)
        {
            case Abilities.None:
                break;

            case Abilities.FenceSneak:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.FenceSneak = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.SwimSuit:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.SwimSuit = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.SwiftSwim:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.SwiftSwim = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.Flippers:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.Flippers = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.LavaSuit:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.LavaSuit = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.LavaSwiftSwim:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.LavaSwiftSwim = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.HikersKit:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.HikerGear = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;

            case Abilities.IceSpikes:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.IceSpikes = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.GrapplingHook:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.GrapplingHook = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.Hammer:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.Hammer = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.ClimbingGear:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.ClimbingGear = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.Dash:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.Dash = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.Ascend:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.Ascend = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.Descend:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.Descend = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;
            case Abilities.ControlStick:
                PlayerManager.Instance.player.GetComponent<PlayerStats>().stats.abilitiesTempGot.ControlStick = true;
                PlayerManager.Instance.player.GetComponent<PlayerStats>().RefillStepsToMax(1);
                break;

            default:
                break;
        }
    }
}

[Serializable]
public class ItemStats
{
    public Items item;
    public int amount;
}

public enum Items
{
    None,

    Coin,
    Collectable,
    IncreaseMaxSteps
}

public enum Abilities
{
    None,

    FenceSneak,

    SwimSuit,
    SwiftSwim,
    Flippers,

    LavaSuit,
    LavaSwiftSwim,

    HikersKit,
    ClimbingGear,

    IceSpikes,

    Dash,
    Ascend,
    Descend,

    Hammer,

    GrapplingHook,

    ControlStick,
}
