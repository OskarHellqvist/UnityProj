using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessTrigger : MonoBehaviour
{
    public void Trigger()
    {
        EventManager.manager.chessActivateEvent.Invoke();
    }
}
