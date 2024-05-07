using Core.Interfaces;

namespace Model
{
    public class SlotModel : ISlotModel
    {
        public IItemModel Item { get; set; }
    }
}