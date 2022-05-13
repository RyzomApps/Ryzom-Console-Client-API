using System.Collections.Generic;

namespace API.Entity
{
    public interface IEntityManager
    {
        /// <summary>
        /// Player Entity
        /// </summary>
        IEntity GetApiUserEntity();

        /// <summary>
        /// Contain all 256 entities around the player
        /// </summary>
        List<IEntity> GetApiEntities();
    }
}