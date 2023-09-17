using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class BoxManager : MonoBehaviour
{
    public BoxSlot[] BoxSlots;
    public GameObject boxItemPrefab;
    public GameObject wristMenu;
    public GameObject boxMenu;
    public GameObject rayInteractor;
    public GameObject teambox;
    public GameObject partnerManager;
    public GameObject settingsMenu;
    public List<GameObject> selected;

    public void selectSlot(GameObject slot)
    {
        if (selected.Count == 0)
        {
            if(slot.transform.childCount > 0)
            {
                slot.GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
                selected.Add(slot);
                
            }
        }
        else if (slot == selected[0])
        {
            selected.Clear();
            slot.GetComponent<UnityEngine.UI.Image>().color = new Color(0.22f,0.22f,0.22f,0.427f);
        }
        else
        {  
            slot.GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
            selected.Add(slot);
            if(!checkLast()){
                
                Transform child1 = null;
                Transform child2 = null;
                if (selected[0].transform.childCount > 0)
                {
                    child1 = selected[0].transform.GetChild(0);
                }
                if (selected[1].transform.childCount > 0)
                {
                    child2 = selected[1].transform.GetChild(0);
                }
                
                child1.SetParent(selected[1].transform);
                if(child2 != null) child2.SetParent(selected[0].transform);
                
                checkTeamBoxOrder();
            }
            selected[0].GetComponent<UnityEngine.UI.Image>().color = new Color(0.22f,0.22f,0.22f,0.427f);
            selected[1].GetComponent<UnityEngine.UI.Image>().color = new Color(0.22f,0.22f,0.22f,0.427f);
            selected.Clear();
        }

    }
    public void wristToBox()
    {
        wristMenu.SetActive(false);
        rayInteractor.SetActive(true);
        boxMenu.SetActive(true);
        for(int i = 0; i <6;i++){
            partnerManager.GetComponent<PartnerManager>().despawnPartner(i);
            
        }
    }
    
    public void wristToSettings()
    {
        wristMenu.SetActive(false);
        rayInteractor.SetActive(true);
        settingsMenu.SetActive(true);
    }
    public void settingsToWrist()
    {
        rayInteractor.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void boxToWrist()
    {
        rayInteractor.SetActive(false);
        if (selected.Count > 0)
        {
            foreach (var selection in selected)
            {
                selection.GetComponent<UnityEngine.UI.Image>().color = new Color(56,56,56,109);
            }
            selected.Clear();
        }
        boxMenu.SetActive(false);
    }

    public bool checkLast()
    {
        GameObject parentName1 = selected[0].transform.parent.gameObject;
        int counter = 0;
        bool last = false;
        if (parentName1.name == "TeamBox")
        {
            for (int i = 0; i < 6; i++)
            {
                if (teambox.transform.GetChild(i).childCount != 0)
                {
                    counter++;
                }
            }

            if (counter <= 1)
            {
                last = true;
            }
        }

        return last;
    }
    public void checkTeamBoxOrder()
    {
        GameObject parentName1 = selected[0].transform.parent.gameObject;
        GameObject parentName2 = selected[1].transform.parent.gameObject;
        if (parentName1.name == "TeamBox" || parentName2.name == "TeamBox")
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < 6; i++)
            {
                if (teambox.transform.GetChild(i).childCount != 0)
                {
                    queue.Enqueue(teambox.transform.GetChild(i).GetChild(0).gameObject);
                }
            }
            
            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject aux = queue.Dequeue();
                aux.transform.SetParent(teambox.transform.GetChild(i));
            }
        }
    }
}
