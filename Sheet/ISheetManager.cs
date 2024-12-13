namespace API.Sheet
{
    public interface ISheetManager
    {
        /// <summary>
        /// Get a sheet from its number
        /// </summary>
        /// <param name="num">sheet number</param>
        /// <returns>pointer on the sheet according to the param or 0 if any pb</returns>
        ISheet Get(ISheetId num);
    }
}