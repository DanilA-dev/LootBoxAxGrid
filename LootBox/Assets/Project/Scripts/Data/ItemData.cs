using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/New Item")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }

}
