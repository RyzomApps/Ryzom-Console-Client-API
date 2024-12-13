using API.Inventory;

namespace API.Sheet
{
    public interface IItemSheet
    {
        /// <summary>Item Family</summary>
        ItemFamily Family { get; set; }

        /// <summary>Item Type</summary>
        ItemType ItemType { get; set; }
    }
}