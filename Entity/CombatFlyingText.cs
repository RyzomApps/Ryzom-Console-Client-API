namespace API.Entity
{
    /// <summary>
    /// Type of combat flying text (original: COMBAT_FLYING_TEXT::TCombatFlyingText).
    /// </summary>
    public enum TCombatFlyingText
    {
        None = 0,
        TargetDodge,
        TargetParry,
        TargetEvade,
        SelfEvade,
        TargetResist,
        SelfResist,
        SelfInterrupt,
        SelfFailure
    }

    /// <summary>
    /// Item special effect triggered in combat (original: ITEM_SPECIAL_EFFECT::TItemSpecialEffect).
    /// </summary>
    public enum TItemSpecialEffect
    {
        FightAddCritical,
        FightVampirism,
        MagicDivineIntervention,
        MagicShootAgain,
        CraftAddStatBonus,
        CraftAddLimit,
        ForageAddRm,
        ForageNoRisk,
        Undefined
    }
}
