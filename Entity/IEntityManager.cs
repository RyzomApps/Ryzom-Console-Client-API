namespace API.Entity
{
    public interface IEntityManager
    {
        /// <summary>
        /// Player Entity
        /// </summary>
        IUserEntity GetApiUserEntity();

        /// <summary>
        /// Contain all 256 entities around the player
        /// </summary>
        IEntity[] GetApiEntities();

        /// <summary>
        /// Get an entity by name. Returns NULL if the entity is not found.
        /// </summary>
        /// <param name="name">of the entity to find</param>  
        /// <param name="caseSensitive">type of test to perform</param>  
        /// <param name="complete">if true, the name must match the full name of the entity</param>
        public IEntity GetEntityByName(string name, bool caseSensitive, bool complete);

        /// <summary>
        /// Get an entity by dataset index. Returns null if the entity is not found.
        /// </summary>
        /// <param name="compressedIndex">The dataset index to search for.</param>
        /// <returns>An instance of <see cref="API.Entity.IEntity"/> if found; otherwise, null.</returns>
        public IEntity GetEntityByCompressedIndex(uint compressedIndex);
    }
}