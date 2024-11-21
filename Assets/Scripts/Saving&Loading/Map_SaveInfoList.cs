using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Map_SaveInfoList
{
    public List<Map_SaveInfo> map_SaveInfo_List;
}

[Serializable]
public class Map_SaveInfo
{
    public string mapName;
    public bool isCompleted;

    public List<CoinInfo> coinList;
    public List<CollectableInfo> collectableList;
    public List<AbilityInfo> abilityList;


    //--------------------


    //Setup the Map's info
    public void SetupMap()
    {
        SetMapName();
        AddInteractableInfo();
    }
    void SetMapName()
    {
        mapName = SceneManager.GetActiveScene().name;
    }
    void AddInteractableInfo()
    {
        //Find all objects in the scene with a Interactable_GetItem script attached
        Interactable_Pickup[] objectsWithScript = UnityEngine.Object.FindObjectsOfType<Interactable_Pickup>();

        //Add all coins to the list
        foreach (Interactable_Pickup obj in objectsWithScript)
        {
            if (obj.itemReceived == Items.Coin /*&& obj.itemReceived.amount > 0*/)
            {
                CoinInfo coinInfo = new CoinInfo();
                coinInfo.coinObj = obj.gameObject;
                coinInfo.pos = obj.gameObject.transform.position;
                coinInfo.isTaken = false;

                coinList.Add(coinInfo);
            }
            else if (obj.itemReceived == Items.Collectable /*&& obj.itemReceived.amount > 0*/)
            {
                CollectableInfo collectableInfo = new CollectableInfo();
                collectableInfo.collectableObj = obj.gameObject;
                collectableInfo.pos = obj.gameObject.transform.position;
                collectableInfo.isTaken = false;

                collectableList.Add(collectableInfo);
            }

            //Abilities
            else if (obj.abilityReceived != Abilities.None)
            {
                AbilityInfo abilitiesInfo = new AbilityInfo();
                abilitiesInfo.abilityObj = obj.gameObject;
                abilitiesInfo.pos = obj.gameObject.transform.position;
                abilitiesInfo.isTaken = false;

                abilityList.Add(abilitiesInfo);
                break;
            }
        }
    }


    //-----


    public void CorrectingMapObjects()
    {
        CheckTakenObjects();
    }
    void CheckTakenObjects()
    {
        //Find all objects in the scene with a Interactable_GetItem script attached
        Map_SaveInfo mapSaveInfo = MapManager.Instance.mapInfo_ToSave;
        Interactable_Pickup[] pickUpList = UnityEngine.Object.FindObjectsOfType<Interactable_Pickup>();

        //Coin Pickups
        for (int i = 0; i < mapSaveInfo.coinList.Count; i++)
        {
            if (mapSaveInfo.coinList[i].isTaken)
            {
                foreach (Interactable_Pickup pickup in pickUpList)
                {
                    if (pickup.itemReceived == Items.Coin)
                    {
                        if (mapSaveInfo.coinList[i].pos.x == pickup.gameObject.transform.position.x
                        && mapSaveInfo.coinList[i].pos.z == pickup.gameObject.transform.position.z)
                        {
                            pickup.gameObject.SetActive(false);

                            break;
                        }
                    }
                }
            }
        }

        //Collectable Pickups
        for (int i = 0; i < mapSaveInfo.collectableList.Count; i++)
        {
            if (mapSaveInfo.collectableList[i].isTaken)
            {
                foreach (Interactable_Pickup pickup in pickUpList)
                {
                    if (pickup.itemReceived == Items.Collectable)
                    {
                        if (mapSaveInfo.collectableList[i].pos.x == pickup.gameObject.transform.position.x
                        && mapSaveInfo.collectableList[i].pos.z == pickup.gameObject.transform.position.z)
                        {
                            pickup.gameObject.SetActive(false);

                            break;
                        }
                    }
                }
            }
        }
    }
}

[Serializable]
public class CoinInfo
{
    public GameObject coinObj;
    public Vector3 pos;
    public bool isTaken;
}

[Serializable]
public class CollectableInfo
{
    public GameObject collectableObj;
    public Vector3 pos;
    public bool isTaken;
}

[Serializable]
public class AbilityInfo
{
    public GameObject abilityObj;
    public Vector3 pos;
    public bool isTaken;
}