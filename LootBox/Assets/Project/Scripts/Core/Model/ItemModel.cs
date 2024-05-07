using Core.Interfaces;
using UnityEngine;

namespace Model
{
    public class ItemModel : IItemModel
    {
        public string ItemName { get; private set; }
        public Sprite Icon { get; private set; }
        
        public ItemModel(string itemName, Sprite icon)
        {
            ItemName = itemName;
            Icon = icon;
        }
    }
}