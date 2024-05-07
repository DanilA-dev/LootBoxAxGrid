using UnityEngine;

namespace Core.Interfaces
{
    public interface IItemModel
    {
        public string ItemName { get; }
        public Sprite Icon { get; }
    }
}