using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Ascend : Singleton<Player_Ascend>
{
    [Header("Ascending")]
    public bool playerCanAscend;
    public GameObject ascendingBlock_Previous;
    public GameObject ascendingBlock_Current;
    public float AscendingDistance = 3;

    RaycastHit hit;


    //--------------------


    private void Update()
    {
        if (RaycastForAscending())
        {
            playerCanAscend = true;
        }
        else
        {
            playerCanAscend = false;
        }
    }


    //--------------------


    bool RaycastForAscending()
    {
        if (gameObject.GetComponent<Player_Stats>().stats.abilities.Ascend)
        {
            if (Physics.Raycast(transform.position, Vector3.up, out hit, AscendingDistance))
            {
                Debug.DrawRay(transform.position, Vector3.up * AscendingDistance, Color.cyan);

                if (hit.transform.GetComponent<BlockInfo>())
                {
                    if (!hit.transform.GetComponent<BlockInfo>().upper_Center)
                    {
                        ascendingBlock_Previous = ascendingBlock_Current;
                        ascendingBlock_Current = hit.transform.gameObject;

                        if (ascendingBlock_Current != ascendingBlock_Previous)
                        {
                            if (ascendingBlock_Previous)
                            {
                                if (ascendingBlock_Previous.GetComponent<BlockInfo>())
                                {
                                    ascendingBlock_Previous.GetComponent<BlockInfo>().ResetColor();
                                }
                            }
                        }

                        ascendingBlock_Current.GetComponent<BlockInfo>().DarkenColors();

                        return true;
                    }
                }
            }

            if (ascendingBlock_Current)
            {
                if (ascendingBlock_Current.GetComponent<BlockInfo>())
                {
                    ascendingBlock_Current.GetComponent<BlockInfo>().ResetColor();
                    ascendingBlock_Current = null;
                }
            }
            if (ascendingBlock_Previous)
            {
                if (ascendingBlock_Previous.GetComponent<BlockInfo>())
                {
                    ascendingBlock_Previous.GetComponent<BlockInfo>().ResetColor();
                    ascendingBlock_Previous = null;
                }
            }
        }
        
        return false;
    }

    public void Ascend()
    {
        if (gameObject.GetComponent<Player_Stats>().stats.abilities.Ascend)
        {
            StartCoroutine(AscendingWait(0.01f));
        }
    }
    public IEnumerator AscendingWait(float waitTime)
    {
        gameObject.GetComponent<Player_Teleport>().isTeleporting = true;

        yield return new WaitForSeconds(waitTime);

        if (ascendingBlock_Current)
        {
            Vector3 newPos = ascendingBlock_Current.transform.position;

            gameObject.transform.position = new Vector3(newPos.x, newPos.y + gameObject.GetComponent<Player_Movement>().heightOverBlock, newPos.z);
        }

        gameObject.GetComponent<Player_Teleport>().isTeleporting = false;
    }
}
