using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ItemCrafting : MonoBehaviour
{
    public RectTransform playerSlotsContainer;
    public RectTransform craftingSlotsContainer;
    public RectTransform resultSlotContainer;
    public Button craftButton;
    public SC_SlotTemplate slotTemplate;
    public SC_SlotTemplate resultSlotTemplate;

    [System.Serializable]
    public class SlotContainer
    {
        public Sprite itemSprite; //Sprite of the assigned item (Must be the same sprite as in items array), or leave null for no item
        public int itemCount; //How many items in this slot, everything equal or under 1 will be interpreted as 1 item
        [HideInInspector]
        public int tableID;
        [HideInInspector]
        public SC_SlotTemplate slot;
    }

    [System.Serializable]
    public class Item
    {
        public Sprite itemSprite;
        public bool stackable = false; //Can this item be combined (stacked) together?
        public string craftRecipe; //Item Keys that are required to craft this item, separated by comma (Tip: Use Craft Button in Play mode and see console for printed recipe)
    }

    public SlotContainer[] playerSlots;
    SlotContainer[] craftSlots = new SlotContainer[9];
    SlotContainer resultSlot = new SlotContainer();
    //List of all available items
    public Item[] items;

    SlotContainer selectedItemSlot = null;

    int craftTableID = -1; //ID of table where items will be placed one at a time (ex. Craft table)
    int resultTableID = -1; //ID of table from where we can take items, but cannot place to

    ColorBlock defaultButtonColors;

    // Start is called before the first frame update
    void Start()
    {
        //Setup slot element template
        slotTemplate.container.rectTransform.pivot = new Vector2(0, 1);
        slotTemplate.container.rectTransform.anchorMax = slotTemplate.container.rectTransform.anchorMin = new Vector2(0, 1);
        slotTemplate.craftingController = this;
        slotTemplate.gameObject.SetActive(false);
        //Setup result slot element template
        resultSlotTemplate.container.rectTransform.pivot = new Vector2(0, 1);
        resultSlotTemplate.container.rectTransform.anchorMax = resultSlotTemplate.container.rectTransform.anchorMin = new Vector2(0, 1);
        resultSlotTemplate.craftingController = this;
        resultSlotTemplate.gameObject.SetActive(false);

        //Attach click event to craft button
        craftButton.onClick.AddListener(PerformCrafting);
        //Save craft button default colors
        defaultButtonColors = craftButton.colors;

        //InitializeItem Crafting Slots
        InitializeSlotTable(craftingSlotsContainer, slotTemplate, craftSlots, 5, 0);
        UpdateItems(craftSlots);
        craftTableID = 0;

        //InitializeItem Player Slots
        InitializeSlotTable(playerSlotsContainer, slotTemplate, playerSlots, 5, 1);
        UpdateItems(playerSlots);

        //InitializeItemResult Slot
        InitializeSlotTable(resultSlotContainer, resultSlotTemplate, new SlotContainer[] { resultSlot }, 0, 2);
        UpdateItems(new SlotContainer[] { resultSlot });
        resultTableID = 2;

        //Reset Slot element template (To be used later for hovering element)
        slotTemplate.container.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        slotTemplate.container.raycastTarget = slotTemplate.item.raycastTarget = slotTemplate.count.raycastTarget = false;
    }

    void InitializeSlotTable(RectTransform container, SC_SlotTemplate slotTemplateTmp, SlotContainer[] slots, int margin, int tableIDTmp)
    {
        int resetIndex = 0;
        int rowTmp = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = new SlotContainer();
            }
            GameObject newSlot = Instantiate(slotTemplateTmp.gameObject, container.transform);
            slots[i].slot = newSlot.GetComponent<SC_SlotTemplate>();
            slots[i].slot.gameObject.SetActive(true);
            slots[i].tableID = tableIDTmp;

            float xTmp = (int)((margin + slots[i].slot.container.rectTransform.sizeDelta.x) * (i - resetIndex));
            if (xTmp + slots[i].slot.container.rectTransform.sizeDelta.x + margin > container.rect.width)
            {
                resetIndex = i;
                rowTmp++;
                xTmp = 0;
            }
            slots[i].slot.container.rectTransform.anchoredPosition = new Vector2(margin + xTmp, -margin - ((margin + slots[i].slot.container.rectTransform.sizeDelta.y) * rowTmp));
        }
    }

    //Update Table UI
    void UpdateItems(SlotContainer[] slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Item slotItem = FindItem(slots[i].itemSprite);
            if (slotItem != null)
            {
                if (!slotItem.stackable)
                {
                    slots[i].itemCount = 1;
                }
                //Apply total item count
                if (slots[i].itemCount > 1)
                {
                    slots[i].slot.count.enabled = true;
                    slots[i].slot.count.text = slots[i].itemCount.ToString();
                }
                else
                {
                    slots[i].slot.count.enabled = false;
                }
                //Apply item icon
                slots[i].slot.item.enabled = true;
                slots[i].slot.item.sprite = slotItem.itemSprite;
            }
            else
            {
                slots[i].slot.count.enabled = false;
                slots[i].slot.item.enabled = false;
            }
        }
    }

    //Find Item from the items list using sprite as reference
    Item FindItem(Sprite sprite)
    {
        if (!sprite)
            return null;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemSprite == sprite)
            {
                return items[i];
            }
        }

        return null;
    }

    //Find Item from the items list using recipe as reference
    Item FindItem(string recipe)
    {
        if (recipe == "")
            return null;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].craftRecipe == recipe)
            {
                return items[i];
            }
        }

        return null;
    }

    //Called from SC_SlotTemplate.cs
    public void ClickEventRecheck()
    {
        if (selectedItemSlot == null)
        {
            //Get clicked slot
            selectedItemSlot = GetClickedSlot();
            if (selectedItemSlot != null)
            {
                if (selectedItemSlot.itemSprite != null)
                {
                    selectedItemSlot.slot.count.color = selectedItemSlot.slot.item.color = new Color(1, 1, 1, 0.5f);
                }
                else
                {
                    selectedItemSlot = null;
                }
            }
        }
        else
        {
            SlotContainer newClickedSlot = GetClickedSlot();
            if (newClickedSlot != null)
            {
                bool swapPositions = false;
                bool releaseClick = true;

                if (newClickedSlot != selectedItemSlot)
                {
                    //We clicked on the same table but different slots
                    if (newClickedSlot.tableID == selectedItemSlot.tableID)
                    {
                        //Check if new clicked item is the same, then stack, if not, swap (Unless it's a crafting table, then do nothing)
                        if (newClickedSlot.itemSprite == selectedItemSlot.itemSprite)
                        {
                            Item slotItem = FindItem(selectedItemSlot.itemSprite);
                            if (slotItem.stackable)
                            {
                                //Item is the same and is stackable, remove item from previous position and add its count to a new position
                                selectedItemSlot.itemSprite = null;
                                newClickedSlot.itemCount += selectedItemSlot.itemCount;
                                selectedItemSlot.itemCount = 0;
                            }
                            else
                            {
                                swapPositions = true;
                            }
                        }
                        else
                        {
                            swapPositions = true;
                        }
                    }
                    else
                    {
                        //Moving to different table
                        if (resultTableID != newClickedSlot.tableID)
                        {
                            if (craftTableID != newClickedSlot.tableID)
                            {
                                if (newClickedSlot.itemSprite == selectedItemSlot.itemSprite)
                                {
                                    Item slotItem = FindItem(selectedItemSlot.itemSprite);
                                    if (slotItem.stackable)
                                    {
                                        //Item is the same and is stackable, remove item from previous position and add its count to a new position
                                        selectedItemSlot.itemSprite = null;
                                        newClickedSlot.itemCount += selectedItemSlot.itemCount;
                                        selectedItemSlot.itemCount = 0;
                                    }
                                    else
                                    {
                                        swapPositions = true;
                                    }
                                }
                                else
                                {
                                    swapPositions = true;
                                }
                            }
                            else
                            {
                                if (newClickedSlot.itemSprite == null || newClickedSlot.itemSprite == selectedItemSlot.itemSprite)
                                {
                                    //Add 1 item from selectedItemSlot
                                    newClickedSlot.itemSprite = selectedItemSlot.itemSprite;
                                    newClickedSlot.itemCount++;
                                    selectedItemSlot.itemCount--;
                                    if (selectedItemSlot.itemCount <= 0)
                                    {
                                        //We placed the last item
                                        selectedItemSlot.itemSprite = null;
                                    }
                                    else
                                    {
                                        releaseClick = false;
                                    }
                                }
                                else
                                {
                                    swapPositions = true;
                                }
                            }
                        }
                    }
                }

                if (swapPositions)
                {
                    //Swap items
                    Sprite previousItemSprite = selectedItemSlot.itemSprite;
                    int previousItemConunt = selectedItemSlot.itemCount;

                    selectedItemSlot.itemSprite = newClickedSlot.itemSprite;
                    selectedItemSlot.itemCount = newClickedSlot.itemCount;

                    newClickedSlot.itemSprite = previousItemSprite;
                    newClickedSlot.itemCount = previousItemConunt;
                }

                if (releaseClick)
                {
                    //Release click
                    selectedItemSlot.slot.count.color = selectedItemSlot.slot.item.color = Color.white;
                    selectedItemSlot = null;
                }

                //Update UI
                UpdateItems(playerSlots);
                UpdateItems(craftSlots);
                UpdateItems(new SlotContainer[] { resultSlot });
            }
        }
    }

    SlotContainer GetClickedSlot()
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i].slot.hasClicked)
            {
                playerSlots[i].slot.hasClicked = false;
                return playerSlots[i];
            }
        }

        for (int i = 0; i < craftSlots.Length; i++)
        {
            if (craftSlots[i].slot.hasClicked)
            {
                craftSlots[i].slot.hasClicked = false;
                return craftSlots[i];
            }
        }

        if (resultSlot.slot.hasClicked)
        {
            resultSlot.slot.hasClicked = false;
            return resultSlot;
        }

        return null;
    }

    void PerformCrafting()
    {
        string[] combinedItemRecipe = new string[craftSlots.Length];

        craftButton.colors = defaultButtonColors;

        for (int i = 0; i < craftSlots.Length; i++)
        {
            Item slotItem = FindItem(craftSlots[i].itemSprite);
            if (slotItem != null)
            {
                combinedItemRecipe[i] = slotItem.itemSprite.name + (craftSlots[i].itemCount > 1 ? "(" + craftSlots[i].itemCount + ")" : "");
            }
            else
            {
                combinedItemRecipe[i] = "";
            }
        }

        string combinedRecipe = string.Join(",", combinedItemRecipe);
        print(combinedRecipe);

        //Search if recipe match any of the item recipe
        Item craftedItem = FindItem(combinedRecipe);
        if (craftedItem != null)
        {
            //Clear Craft slots
            for (int i = 0; i < craftSlots.Length; i++)
            {
                craftSlots[i].itemSprite = null;
                craftSlots[i].itemCount = 0;
            }

            resultSlot.itemSprite = craftedItem.itemSprite;
            resultSlot.itemCount = 1;

            UpdateItems(craftSlots);
            UpdateItems(new SlotContainer[] { resultSlot });
        }
        else
        {
            ColorBlock colors = craftButton.colors;
            colors.selectedColor = colors.pressedColor = new Color(0.8f, 0.55f, 0.55f, 1);
            craftButton.colors = colors;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Slot UI follow mouse position
        if (selectedItemSlot != null)
        {
            if (!slotTemplate.gameObject.activeSelf)
            {
                slotTemplate.gameObject.SetActive(true);
                slotTemplate.container.enabled = false;

                //Copy selected item values to slot template
                slotTemplate.count.color = selectedItemSlot.slot.count.color;
                slotTemplate.item.sprite = selectedItemSlot.slot.item.sprite;
                slotTemplate.item.color = selectedItemSlot.slot.item.color;
            }

            //Make template slot follow mouse position
            slotTemplate.container.rectTransform.position = Input.mousePosition;
            //Update item count
            slotTemplate.count.text = selectedItemSlot.slot.count.text;
            slotTemplate.count.enabled = selectedItemSlot.slot.count.enabled;
        }
        else
        {
            if (slotTemplate.gameObject.activeSelf)
            {
                slotTemplate.gameObject.SetActive(false);
            }
        }
    }
}