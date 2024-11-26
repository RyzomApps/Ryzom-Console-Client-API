using System.Collections.Generic;

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
    }
}