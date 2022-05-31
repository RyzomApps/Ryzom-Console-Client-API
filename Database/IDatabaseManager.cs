namespace API.Database
{
    public interface IDatabaseManager
    {
        /// <summary>
        /// Return the value of a property (the update flag is set to false)
        /// </summary>
        /// <param name="name">is the name of the property</param>
        /// <returns>the value of the property</returns>
        long GetProp(string name);
    }
}