using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoDisplay : Singleton<LevelInfoDisplay>
{
    [Header("LevelInfo Parent")]
    [SerializeField] GameObject LevelInfo_Parent;

    [Header("Info")]
    public GameObject levelName;
    public GameObject AbilityHeader;
    [SerializeField] GameObject coinAmount;
    [SerializeField] GameObject collectableAmount;
    [SerializeField] GameObject stepAmount;

    [SerializeField] Image levelImage;

    [Header("Ability Sprites")]
    [SerializeField] Image ability_FenceSneak;
    [SerializeField] Image ability_SwimSuit;
    [SerializeField] Image ability_SwiftSwim;
    [SerializeField] Image ability_Flippers;
    [SerializeField] Image ability_LavaSuit;
    [SerializeField] Image ability_LavaSwiftSwim;
    [SerializeField] Image ability_HikerGear;
    [SerializeField] Image ability_IceSpikes;
    [SerializeField] Image ability_GrapplingHook;
    [SerializeField] Image ability_Hammer;
    [SerializeField] Image ability_ClimbingGear;
    [SerializeField] Image ability_Dash;
    [SerializeField] Image ability_Ascend;
    [SerializeField] Image ability_Descend;
    [SerializeField] Image ability_ControlStick;

    [Header("Ability Got Sprites")]
    [SerializeField] Image ability_FenceSneak_Got;
    [SerializeField] Image ability_SwimSuit_Got;
    [SerializeField] Image ability_SwiftSwim_Got;
    [SerializeField] Image ability_Flippers_Got;
    [SerializeField] Image ability_LavaSuit_Got;
    [SerializeField] Image ability_LavaSwiftSwim_Got;
    [SerializeField] Image ability_HikerGear_Got;
    [SerializeField] Image ability_IceSpikes_Got;
    [SerializeField] Image ability_GrapplingHook_Got;
    [SerializeField] Image ability_Hammer_Got;
    [SerializeField] Image ability_ClimbingGear_Got;
    [SerializeField] Image ability_Dash_Got;
    [SerializeField] Image ability_Ascend_Got;
    [SerializeField] Image ability_Descend_Got;
    [SerializeField] Image ability_ControlStick_Got;


    //--------------------


    private void OnEnable()
    {
        HideDisplayLevelInfo();
    }


    //--------------------


    public void ShowDisplayLevelInfo(Map_SaveInfo mapInfo, LoadLevel level)
    {
        HideDisplayLevelInfo();

        //Display MapName
        levelName.GetComponent<TextMeshProUGUI>().text = mapInfo.mapName;

        //Display Level Image
        levelImage.sprite = level.levelSprite;

        //Display Coins got
        int coinCounter = 0;
        for (int i = 0; i < mapInfo.coinList.Count; i++)
        {
            if (mapInfo.coinList[i].isTaken)
            {
                coinCounter++;
            }
        }
        coinAmount.GetComponentInChildren<TextMeshProUGUI>().text = coinCounter + " / 10";

        //Display Coins got
        int collectionCounter = 0;
        for (int i = 0; i < mapInfo.collectableList.Count; i++)
        {
            if (mapInfo.collectableList[i].isTaken)
            {
                collectionCounter++;
            }
        }
        collectableAmount.GetComponentInChildren<TextMeshProUGUI>().text = collectionCounter + " / 3";

        //Display Coins got
        int stepCounter = 0;
        for (int i = 0; i < mapInfo.maxStepList.Count; i++)
        {
            if (mapInfo.maxStepList[i].isTaken)
            {
                stepCounter++;
            }
        }
        stepAmount.GetComponentInChildren<TextMeshProUGUI>().text = stepCounter + " / 3";

        //Display all abilities in the Level
        if (mapInfo.abilitiesInLevel.FenceSneak)
            ability_FenceSneak.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.SwimSuit)
            ability_SwimSuit.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.SwiftSwim)
            ability_SwiftSwim.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.Flippers)
            ability_Flippers.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.LavaSuit)
            ability_LavaSuit.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.LavaSwiftSwim)
            ability_LavaSwiftSwim.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.HikerGear)
            ability_HikerGear.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.IceSpikes)
            ability_IceSpikes.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.GrapplingHook)
            ability_GrapplingHook.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.Hammer)
            ability_Hammer.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.ClimbingGear)
            ability_ClimbingGear.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.Dash)
            ability_Dash.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.Ascend)
            ability_Ascend.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.Descend)
            ability_Descend.gameObject.SetActive(true);
        if (mapInfo.abilitiesInLevel.ControlStick)
            ability_ControlStick.gameObject.SetActive(true);

        //Display all abilities YOU in the Level
        if (mapInfo.abilitiesGotInLevel.FenceSneak)
            ability_FenceSneak_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.SwimSuit)
            ability_SwimSuit_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.SwiftSwim)
            ability_SwiftSwim_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.Flippers)
            ability_Flippers_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.LavaSuit)
            ability_LavaSuit_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.LavaSwiftSwim)
            ability_LavaSwiftSwim_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.HikerGear)
            ability_HikerGear_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.IceSpikes)
            ability_IceSpikes_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.GrapplingHook)
            ability_GrapplingHook_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.Hammer)
            ability_Hammer_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.ClimbingGear)
            ability_ClimbingGear_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.Dash)
            ability_Dash_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.Ascend)
            ability_Ascend_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.Descend)
            ability_Descend_Got.gameObject.SetActive(true);
        if (mapInfo.abilitiesGotInLevel.ControlStick)
            ability_ControlStick_Got.gameObject.SetActive(true);

        //Show all Displays
        LevelInfo_Parent.SetActive(true);
    }

    public void HideDisplayLevelInfo()
    {
        LevelInfo_Parent.SetActive(false);

        ability_FenceSneak.gameObject.SetActive(false);
        ability_SwimSuit.gameObject.SetActive(false);
        ability_SwiftSwim.gameObject.SetActive(false);
        ability_Flippers.gameObject.SetActive(false);
        ability_LavaSuit.gameObject.SetActive(false);
        ability_LavaSwiftSwim.gameObject.SetActive(false);
        ability_HikerGear.gameObject.SetActive(false);
        ability_IceSpikes.gameObject.SetActive(false);
        ability_GrapplingHook.gameObject.SetActive(false);
        ability_Hammer.gameObject.SetActive(false);
        ability_ClimbingGear.gameObject.SetActive(false);
        ability_Dash.gameObject.SetActive(false);
        ability_Ascend.gameObject.SetActive(false);
        ability_Descend.gameObject.SetActive(false);
        ability_ControlStick.gameObject.SetActive(false);

        ability_FenceSneak_Got.gameObject.SetActive(false);
        ability_SwimSuit_Got.gameObject.SetActive(false);
        ability_SwiftSwim_Got.gameObject.SetActive(false);
        ability_Flippers_Got.gameObject.SetActive(false);
        ability_LavaSuit_Got.gameObject.SetActive(false);
        ability_LavaSwiftSwim_Got.gameObject.SetActive(false);
        ability_HikerGear_Got.gameObject.SetActive(false);
        ability_IceSpikes_Got.gameObject.SetActive(false);
        ability_GrapplingHook_Got.gameObject.SetActive(false);
        ability_Hammer_Got.gameObject.SetActive(false);
        ability_ClimbingGear_Got.gameObject.SetActive(false);
        ability_Dash_Got.gameObject.SetActive(false);
        ability_Ascend_Got.gameObject.SetActive(false);
        ability_Descend_Got.gameObject.SetActive(false);
        ability_ControlStick_Got.gameObject.SetActive(false);
    }
}