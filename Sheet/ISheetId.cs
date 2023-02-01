namespace API.Sheet
{
    public interface ISheetId
    {
        uint Id { get; set; }
        uint Type { get; set; }
        string Name { get; }
    }
}