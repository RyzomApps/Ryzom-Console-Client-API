namespace API.Sheet
{
    public interface IEntitySheet
    {
        /// <summary>
        /// Return the type of the sheet
        /// </summary>
        SheetType Type { get; set; }
    }
}