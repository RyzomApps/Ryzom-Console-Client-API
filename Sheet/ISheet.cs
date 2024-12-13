namespace API.Sheet
{
    public interface ISheet
    {
        /// <summary>
        /// Return the type of the sheet
        /// </summary>
        SheetType Type { get; set; }
    }
}