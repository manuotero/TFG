

using System;
using System.Collections.Generic;

public class Turn
{
    
    public Turn()
    {
        _actions = new List<Action>();
    }

    public void AddAction(Action action)
    {
        _actions.Add(action);
        _actions.Sort(CompareAction);
    }

    private static int CompareAction(Action action1, Action action2)
    {
        if (action1.GetPrio() > action2.GetPrio())
        {
            return 1;
        }else if (action1.GetPrio() < action2.GetPrio())
        {
            return -1;
        }
        else
        {
            if (action1.GetSpeed() < action2.GetSpeed())
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    private readonly List<Action> _actions;
}