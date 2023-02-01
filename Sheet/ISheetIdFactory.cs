namespace API.Sheet
{
    public interface ISheetIdFactory
    {
        /// <summary>
        /// create an SheetId from a numeric reference
        /// </summary>
        ISheetId SheetId(uint sheetRef);

        /// <summary>
        /// Constructor
        /// </summary>
        ISheetId SheetId(string sheetName);
    }
}