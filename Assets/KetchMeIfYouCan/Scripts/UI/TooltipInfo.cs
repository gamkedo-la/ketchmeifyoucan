using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Stealable, Interactable }
public class TooltipInfo : MonoBehaviour {
    public ItemType itemType;
    [TextArea(3,10)]
    public string tooltipDescription = "This is an item";

    public string ItemInfo() {

        string typeInfo = " ";

        switch (itemType) {
            case ItemType.Interactable:
            typeInfo = "Press E to interact." +
                "" +
                "Name: " + transform.name;
            break;

            case ItemType.Stealable:
            typeInfo = "Stealable: Press \"E\" to steal. \n" +     //type
            "\n" +
            transform.name + "\n" +         //name
            "\n" +
            tooltipDescription ;            //description
            break;
        }

        return typeInfo;
    }

}
