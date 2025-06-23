using UnityEngine;

namespace TopDown_Project
{
    public class ItemHandler : MonoBehaviour
    {
        [SerializeField] private ItemData itemData;
        public ItemData ItemData => itemData;
    }
}
