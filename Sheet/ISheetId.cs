namespace API.Sheet
{
    public interface ISheetId
    {
        /// <summary>
        /// Return the sheet type (sub part of the sheetid)
        /// </summary>
        uint GetSheetType();

        /// <summary>
        /// Return the **whole** sheet id (id+type)
        /// </summary>
        uint AsInt();

        /// <summary>
        /// Return the sheet sub id (sub part of the sheetid)
        /// </summary>
        uint GetShortId();

        string Name { get; }
    }
}