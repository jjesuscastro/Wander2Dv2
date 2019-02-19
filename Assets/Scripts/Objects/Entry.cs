using UnityEngine;

namespace Object
{
    [CreateAssetMenu(fileName = "New Journal Entry", menuName = "Journal/Entry")]
    public class Entry : ScriptableObject
    {
        new public string name = "New Journal Entry";
        public Sprite icon = null;
    }
}
