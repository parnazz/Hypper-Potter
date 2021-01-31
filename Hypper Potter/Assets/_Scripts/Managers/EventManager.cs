using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager
{
    public static Action onGameStarted = delegate { };
    public static Action onGameEnd = delegate { };
    public static Action<int> onGameEndScreenOpened = delegate { };
    public static Action onCoinPickup = delegate { };
    public static Action onCrossFinishLine = delegate { };
}
