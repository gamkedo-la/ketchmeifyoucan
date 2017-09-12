using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System;

[CreateAssetMenu(menuName ="Item/Stealable Item", fileName ="Stealable Item File Name")]
public class Item : ScriptableObject
{
    [Header("General Properties"), Tooltip("Name of the item to display in UI")]
    public string Name = "New Item";

    [Tooltip("Description of item to display on UI"), Multiline(3)]
    public string Description = "Item Description";
	
    [Tooltip("Item Image that is displayed on the UI")]
    public Sprite Sprite;

   
}
