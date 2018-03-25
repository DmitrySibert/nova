using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine.Builder
{
    public interface IBuilder
    {
        object Build();
    }
}
