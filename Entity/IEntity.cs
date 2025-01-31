using System.Numerics;

namespace API.Entity
{
    /// <summary>
    /// Representation of an object that interacts with the game and responds to player input or other entities
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Position Vector of the entity
        /// </summary>
        Vector3 Pos { get; set; }

        /// <summary>
        /// Used to get a position in front of the entity based on the direction they are facing
        /// </summary>
        Vector3 Front { get; set; }

        /// <summary>
        /// Direction the entity is facing
        /// </summary>
        Vector3 Dir { get; set; }

        /// <summary>
        /// Flags to know what is possible to do with the entity (selectable, lift-able, etc.).
        /// </summary>
        EntityProperties Properties { get; set; }

        /// <summary>
        /// Return a displayable name
        /// </summary>
        string GetDisplayName();

        /// <summary>
        /// Returns the name of the entities guild
        /// </summary>
        string GetGuildName();

        /// <summary>
        /// Returns the title of the entity
        /// </summary>
        string GetTitle();

        /// <summary>
        /// Primitive type of the entity
        /// </summary>
        EntityType GetEntityType();

        /// <summary>
        /// Return the current slot for the entity or CLFECOMMON::INVALID_SLOT if the entity is not in any slot.
        /// </summary>
        byte Slot();

        /// <summary>
        /// Return the current target of the entity or CLFECOMMON::INVALID_SLOT
        /// </summary>
        byte TargetSlot();

        /// <summary>
        /// Return the sheet Id of the entity
        /// </summary>
        uint SheetId();

        /// <summary>
        /// Return the entity Id (persistent as long as the entity is connected) (CLFECOMMON::INVALID_CLIENT_DATASET_INDEX for an invalid one)
        /// </summary>
        uint DataSetId();

        /// <summary>
        /// Set the current target of the entity
        /// </summary>
        /// <param name="slot">TCLEntityId slot</param>
        /// <remarks>CLFECOMMON::INVALID_SLOT for no target</remarks>
        void SetTargetSlot(byte slot);

        /// <summary>
        /// Return true if the character is currently dead.
        /// </summary>
        bool IsDead();
    }
}