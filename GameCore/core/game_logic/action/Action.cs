

using System.Collections.Generic;

public class Action
{
    public Action(int prio, int speed)
    {
        _priotity = prio;
        _userSpeed = speed;
    }

    public int GetPrio()
    {
        return _priotity;
    }
    public int GetSpeed()
    {
        return _userSpeed;
    }

    protected int _priotity;
    protected int _userSpeed;
}