using System.Collections.Generic;
using Core.Interfaces;

namespace Model
{
    public class ItemsModel
    {
        public IList<IItemModel> Items { get; private set; }

        public ItemsModel()
        {
            Items = new List<IItemModel>();
        }

        public void AddItem(IItemModel newItem)
        {
            if (!Items.Contains(newItem))
                Items.Add(newItem);
        }

        public void RemoveItem(IItemModel item)
        {
            if (Items.Contains(item))
                Items.Remove(item);
        }
    }
}