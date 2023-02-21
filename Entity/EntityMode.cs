namespace API.Entity
{
    /// <remarks>
    ///IMPORTANT : IF YOU MODIFY THIS ENUM DO NOT FORGET TO CHANGE stringToMode() TOO
    /// </remarks>>
    public enum EntityMode
    {
        UnknownMode = 0,

        Normal,
        CombatFloat,
        Combat,

        Swim,
        Sit,

        //---------- MOUNT --------//
        MountNormal,
        MountSwim,
        //-------------------------//

        Eat,
        Rest,
        Alert,
        Hungry,

        //--------- DEAD ----------//
        Death,
        SwimDeath,
        //-------------------------//

        NumberOfModes
    }
}