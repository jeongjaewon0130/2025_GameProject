using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SC_SlotTemplate : MonoBehaviour, IPointerClickHandler
{
    public Image container;
    public Image item;
    public Text count;

    [HideInInspector]
    public bool hasClicked = false;
    [HideInInspector]
    public SC_ItemCrafting craftingController;

    //이 스크립트가 첨부된 선택 가능한 개체 위에 마우스를 클릭하면 이 작업이 수행이 실행됨
    public void OnPointerClick(PointerEventData eventData)
    {
        hasClicked = true;
        craftingController.ClickEventRecheck();
    }
}