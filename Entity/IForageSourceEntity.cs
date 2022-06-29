namespace API.Entity
{
    /// <summary>
    /// Enumeration of all forage source bar indices
    /// </summary>
    public enum TfsBarIndex
    {
        /// <summary>
        /// Time left
        /// </summary>
        FsbTime = 0,

        /// <summary>
        /// Quantity of materials
        /// </summary>
        FsbQuantiy = 1,
        
        /// <summary>
        /// Source life
        /// </summary>
        FsbD = 2,
        
        /// <summary>
        /// Aggressiveness
        /// </summary>
        FsbE = 3,

        /// <summary>
        /// Regional kami value
        /// </summary>
        FsbK = 4,

        NbFsBarIndices = 5
    }

    /// <summary>
    /// Representation of an forage source
    /// </summary>
    public interface IForageSourceEntity
    {
        /// <summary>
        /// Get the current value for a specific bar
        /// </summary>
        byte GetCurrentBarValue(TfsBarIndex index);
    }
}