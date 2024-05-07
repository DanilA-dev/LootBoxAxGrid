using AxGrid;
using AxGrid.Base;
using Data;
using Model;
using UnityEngine;

namespace Core
{
    public class ItemsModelBinder : MonoBehaviourExt
    {
        [SerializeField] private ItemData[] _itemDatas;

        private ItemsModel _itemsModel;

        [OnAwake]
        private void OnAwake()
        {
            _itemsModel = new ItemsModel();
        }

        [OnStart]
        private void OnStart()
        {
            foreach (var itemData in _itemDatas)
                _itemsModel.AddItem(new ItemModel(itemData.ItemName, itemData.Icon));


            Settings.Model.Set(Constants.Models.ITEMS_MODEL, _itemsModel);
        }
    }
}