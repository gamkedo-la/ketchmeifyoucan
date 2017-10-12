using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System;
using UnityEngine.EventSystems;

public class InventoryMgr : MonoBehaviour
{
    [SerializeField]
    private Inventory inventoryList;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject ItemTemplate;//Inventory Item Container
    [SerializeField]
    public WorldItems worldItems;
    public GameManager gameManager;
    public GameObject playerInteract;

    private Text BagSpaceText;
    
	public void Awake()
    {
        ClearInventoryList();
        BagSpaceText = inventoryPanel.transform.Find("Footer/BagDetails/Stats").GetComponent<Text>();
    }

    public void Start()
    {
        inventoryList.TotalBagSlots=0;

        /*for (int i=0;i<worldItems.AvailableWorldItems.Count;i++)
        {
            //Debug.Log(" Name world item " + worldItems.AvailableWorldItems[i].name);
        }*/

        //map objective items into list and ui objects
        foreach (var item in gameManager.m_ObjectiveItems)
        {
            //Debug.Log(item.name + " is an objective item.SERGIO");
            
            Item objectiveItem = worldItems.AvailableWorldItems.Find(x => x.name.Equals(
            item.name));
            if (objectiveItem != null)
            {
                inventoryList.InventoryItems.Add(objectiveItem);
                UpdateBagSlotsUsed();
                //calculate number of targets
                inventoryList.TotalBagSlots++;

                        //add it to the UI Screen
                        Transform ScrollViewContent = inventoryPanel.transform.Find("InvPanel/Scroll View/Viewport/Content");
                GameObject newItem = Instantiate(ItemTemplate, ScrollViewContent);
                newItem.transform.localScale = Vector3.one;

                newItem.transform.Find("Image/ItemImage").GetComponent<Image>().sprite = objectiveItem.Sprite;
                newItem.transform.Find("ItemName").GetComponent<Text>().text = objectiveItem.Name;
                newItem.transform.Find("Description").GetComponent<Text>().text = objectiveItem.Description;

                objectiveItem.UIInventoryPanel = newItem;
            }
        }
        
        BagSpaceText.text = string.Format("{0}/{1}", inventoryList.InventoryItems.Count, inventoryList.TotalBagSlots);
    }
    
	public void UpdateBagSlotsUsed()
	{
		BagSpaceText.text = string.Format("{0}/{1}", inventoryList.InventoryItems.Count, inventoryList.TotalBagSlots);
	}

    public void GetItem(Item addedItem)
    {
		//add the item to the player's inventory
		inventoryList.InventoryItems.Add(addedItem);
		UpdateBagSlotsUsed();

		//add it to the UI Screen
		Transform ScrollViewContent = inventoryPanel.transform.Find("InvPanel/Scroll View/Viewport/Content");
		GameObject newItem = Instantiate(ItemTemplate, ScrollViewContent);
		newItem.transform.localScale = Vector3.one;

		newItem.transform.Find("Image/ItemImage").GetComponent<Image>().sprite = addedItem.Sprite;
		newItem.transform.Find("ItemName").GetComponent<Text>().text = addedItem.Name;
		newItem.transform.Find("Description").GetComponent<Text>().text = addedItem.Description;
	}

	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryWindow();
        }
    }
    private void ToggleInventoryWindow()
    {
		ClearBufferInventory();
        PopulateInventory(inventoryList);
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void ClearInventoryList()
    {
        inventoryList.InventoryItems = new List<Item>();
    }


    public void ClearBufferInventory()
    {
        Transform ScrollViewContent = inventoryPanel.transform.Find("InvPanel/Scroll View/Viewport/Content");

        foreach (Transform inspectorLiveContainerAsChild in ScrollViewContent.transform)
        {
            Debug.Log(inspectorLiveContainerAsChild.name);
            Destroy(inspectorLiveContainerAsChild.gameObject);
        }
    }

    public void RemoveObjective(GameObject item)
    {
        Item stolenObjective = worldItems.AvailableWorldItems.Find(x => x.name.Equals(
            item.gameObject.name));

        //update inventory List removing stolen objective
        inventoryList.InventoryItems.Remove(stolenObjective);
        UpdateBagSlotsUsed();

        //remove stolenObjective from the UI Screen
        Destroy(stolenObjective.UIInventoryPanel);
    }

    public void GetItem(GameObject pickedUpItem)
    {
		Item addedItem = worldItems.AvailableWorldItems.Find(x => x.Name.Equals(
			pickedUpItem.gameObject.name));
		
		Debug.Log("purchItem " + addedItem);
		GetItem(addedItem);
		
        pickedUpItem.SetActive(false);
    }

    public void PopulateInventory(Inventory inventoryList)
    {
        Transform ScrollViewContent = inventoryPanel.transform.Find("InvPanel/Scroll View/Viewport/Content");
        foreach(var item in inventoryList.InventoryItems)
        {
            GameObject newItem = Instantiate(ItemTemplate, ScrollViewContent);
            newItem.transform.localScale = Vector3.one;
            
            newItem.transform.Find("Image/ItemImage").GetComponent<Image>().sprite = item.Sprite;
            newItem.transform.Find("ItemName").GetComponent<Text>().text = item.Name;
            newItem.transform.Find("Description").GetComponent<Text>().text = item.Description;
        }
    }

    public void CloseInventoryWindow()
    {
        inventoryPanel.SetActive(false);
    }
    
}
