using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSlyce
{
    public class ListManager : MonoBehaviour
    {
        public Scrollbar scrollbarV;
        //public GameObject loadingPanel;
        public Sprite[] stateSprites;
        public enum ToggleState
        {
            Unchecked, Partial, Checked
        };
        public ToggleState tglStateSlctAll = ToggleState.Unchecked;
        //Reference to Invite Button and select All- toggler
        public GameObject btnSlctAll;

        List<GSItem> gsList = new List<GSItem>();
        // List containers that list Items - (Dynamically Increasing ListView <Custom>)
        public GameObject GS_ListContainer;
        //Prefabs that holds items that will be places in the containers.
        
        public GSItem gsItemPrefab;

        void Start()
        {
            gsList.Clear();
            SetScrollBarVisibility(false);
        }


        //Click Handler of Select All Button
        public void TglSelectAllClickHandler()
        {
            switch (tglStateSlctAll)
            {
                case ToggleState.Partial:
                case ToggleState.Unchecked:
                    foreach (var item in gsList)
                    {
                        item.tglBtn.isOn = true;
                    }
                    tglStateSlctAll = ToggleState.Checked;
                    ChangeToggleState(ToggleState.Checked);
                    break;
                case ToggleState.Checked:
                    foreach (var item in gsList)
                    {
                        item.tglBtn.isOn = false;
                    }
                    ChangeToggleState(ToggleState.Unchecked);
                    break;
            }
        }
        //Method to change Toggle State On the Fly
        public void ChangeToggleState(ToggleState state)
        {
            switch (state)
            {
                case ToggleState.Unchecked:
                    tglStateSlctAll = state;
                    btnSlctAll.GetComponent<Image>().sprite = stateSprites[0];
                    break;
                case ToggleState.Partial:
                    bool flagOn = false, flagOff = false;
                    foreach (var item in gsList)
                    {
                        if (item.tglBtn.isOn)
                        {
                            flagOn = true;
                        }
                        else
                        {
                            flagOff = true;
                        }
                    }
                    if (flagOn && flagOff)
                    {
                        tglStateSlctAll = state;
                        btnSlctAll.GetComponent<Image>().sprite = stateSprites[1];
                        //Debug.Log("Partial");
                    }
                    else if (flagOn && !flagOff)
                    {
                        ChangeToggleState(ToggleState.Checked);
                        //Debug.Log("Checked");
                    }
                    else if (!flagOn && flagOff)
                    {
                        ChangeToggleState(ToggleState.Unchecked);
                        //Debug.Log("Unchecked");
                    }
                    break;
                case ToggleState.Checked:
                    tglStateSlctAll = state;
                    btnSlctAll.GetComponent<Image>().sprite = stateSprites[2];
                    break;
            }
        }

        //Method to add item to the custom invitable dynamically scrollable list
        public void AddItem()
        {
            GSItem tempItem = Instantiate(gsItemPrefab) as GSItem;
            tempItem.transform.SetParent(GS_ListContainer.transform, false);
            gsList.Add(tempItem);

            //if (gsList.Count > 3)
            //{
            //    SetScrollBarVisibility(true);
            //}
        }

        private void SetScrollBarVisibility(bool status)
        {
            scrollbarV.gameObject.SetActive(status);
        }

        public void OnScrollRectValueChanged()
        {
            scrollbarV.gameObject.SetActive(true);
            Invoke("HideScrollbar", 2);
        }


        void HideScrollbar()
        {
            SetScrollBarVisibility(false);
        }
        //Click Handler for Back Button
        public void BackMain()
        {
            Application.LoadLevel(0);
        }
    }
}