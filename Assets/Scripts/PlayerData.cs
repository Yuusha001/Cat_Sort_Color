using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public int currentLevel;
    public Dictionary<Cats, bool> UnlockedCharactors = new Dictionary<Cats, bool>()
    {
        { Cats.Cat_0, true },
        { Cats.Cat_1, true },
        { Cats.Cat_2, true },
        { Cats.Cat_3, true },
        { Cats.Cat_4, false },
        { Cats.Cat_5, false },
        { Cats.Cat_6, false },
    };
    public Dictionary<SpaceLifts, bool> UnlockedSpaceLifts = new Dictionary<SpaceLifts, bool>()
    {
        { SpaceLifts.UFO, true },
        { SpaceLifts.ROCKET, true }
    };
}
