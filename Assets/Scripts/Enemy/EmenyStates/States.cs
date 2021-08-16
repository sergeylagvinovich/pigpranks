using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class States : MonoBehaviour
{
    public abstract void Execute();
    public abstract void Exit();

}
