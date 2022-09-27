using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneElementsController
{
    private static int playingCell;

    public static int PlayingCell { get => playingCell; set => playingCell = value; }

    public enum Cells : int
    {
        red = 0,
        white = 1,
        platalet = 2
    }
}
