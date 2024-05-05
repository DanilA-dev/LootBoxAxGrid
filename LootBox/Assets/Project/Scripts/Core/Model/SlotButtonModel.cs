using UnityEngine;

namespace Model
{
    public class SlotButtonModel
    {
        public string ID { get; private set; }
        public KeyCode KeyCode { get; private set; }
        
        public SlotButtonModel(string id, KeyCode keyCode)
        {
            ID = id;
            KeyCode = keyCode;
        }

    }
}