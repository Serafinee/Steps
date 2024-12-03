using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad_MapInfo : Singleton<SaveLoad_MapInfo>
{
    public void LoadData()
    {
        //Don't save MapInfo if not in a level
        MainMenuManager mainMenuManager = FindObjectOfType<MainMenuManager>();
        if (mainMenuManager) { return; }

        if (DataManager.Instance.mapInfo_StoreList != null)
        {
            if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List != null)
            {
                for (int i = 0; i < DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List.Count; i++)
                {
                    print("MapName: " + DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].mapName + " | Saved Name: " + SceneManager.GetActiveScene().name);

                    if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].mapName == SceneManager.GetActiveScene().name)
                    {
                        print("MapInfo is already in the system");

                        MapManager.Instance.mapInfo_ToSave = DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i];
                        MapManager.Instance.mapInfo_ToSave.CorrectingMapObjects();
                        SaveGame();

                        return;
                    }
                }
            }
        }

        MapManager.Instance.mapInfo_ToSave.SetupMap();
        SaveGame();
    }
    public void SaveGame()
    {
        //Don't save MapInfo if not in a level
        MainMenuManager mainMenuManager = FindObjectOfType<MainMenuManager>();
        if (mainMenuManager) { return; }

        if (DataManager.Instance.mapInfo_StoreList != null)
        {
            if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List != null)
            {
                for (int i = 0; i < DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List.Count; i++)
                {
                    if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].mapName == MapManager.Instance.mapInfo_ToSave.mapName)
                    {
                        DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i] = MapManager.Instance.mapInfo_ToSave;
                        DataPersistanceManager.instance.SaveGame();

                        return;
                    }
                }
            }
        }

        SaveNewLevel();
    }
    void SaveNewLevel()
    {
        if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List.Count <= 0)
        {
            print("1. Add new Level");
            List<Map_SaveInfo> Map_SaveInfoList_Temp = new List<Map_SaveInfo>();
            DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List = Map_SaveInfoList_Temp;
        }

        print("2. Add new Level");
        DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List.Add(MapManager.Instance.mapInfo_ToSave);
        DataPersistanceManager.instance.SaveGame();
    }


    //--------------------


    public Map_SaveInfo GetMapInfo(string mapName)
    {
        if (DataManager.Instance.mapInfo_StoreList != null)
        {
            if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List != null)
            {
                for (int i = 0; i < DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List.Count; i++)
                {
                    if (DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i].mapName == mapName)
                    {
                        return DataManager.Instance.mapInfo_StoreList.map_SaveInfo_List[i];
                    }
                }
            }
        }

        return null;
    }
}
