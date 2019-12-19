using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void FlipValues();
}

public interface BossStarter
{
    void StartActions();
}
