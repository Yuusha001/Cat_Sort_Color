using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public int currentLevel;
    public Dictionary<Cats, bool> UnlockedCharactors = new Dictionary<Cats, bool>();
    public Dictionary<SpaceLifts, bool> UnlockedSpaceLifts = new Dictionary<SpaceLifts, bool>()
    {
        { SpaceLifts.UFO, true },
        { SpaceLifts.ROCKET, true }
    };
}
