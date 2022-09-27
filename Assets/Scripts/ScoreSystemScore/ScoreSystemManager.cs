using DataAndSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystemManager
{
    #region Private

    [Header("RedCell")]
    private static int maxRedScore = 0;
    private static int totalRedScore = 0;
    private static int minRedScoreToUnlockNext = 1000;
    private static int totalOxygenBalls = 0;
    private static int timesDraggedRedMinigame = 0;
    private static int numOxygenBallsScored = 0;
    private static float totalRedTime = 0.0f;

    private static readonly int pointsPerOxygenBall = 200;
    private static readonly float maxTotalRedTime = 300f;

    [Header("WhiteCell")]
    private static int maxWhiteScore = 0;
    private static int totalWhiteScore = 0;
    private static int minWhiteScoreToUnlockNext = 10000;
    private static int totalAmmo = 0;
    private static int numCellsInWound = 0;
    private static int numShootsScored = 0;
    private static float totalWhiteTime = 0.0f;

    private static readonly int pointsPerAmmo = 200;
    private static readonly float maxTotalWhiteTime = 250f;

    [Header("Platalet")]
    private static int maxPlataletScore = 0;
    private static int totalPlataletScore = 0;
    private static int numPlatalets = 0;
    private static float totalPlataletTime = 0.0f;

    private static readonly int pointsPerPlatalet = 100;
    private static readonly float maxTotalPlataletTime = 800f;

    private static readonly int averageTravelTime = 150;
    private static readonly int maxImpulse = 1500;

    private static bool miniGameFinished = false;

    #endregion

    #region Encapsulation

    //Red cell
    public static int TotalRedScore { get => totalRedScore; }
    public static int MaxRedScore { get => maxRedScore; set => maxRedScore = value; }
    public static int MinRedScoreToUnlock { get => minRedScoreToUnlockNext; set => minRedScoreToUnlockNext = value; }
    public static int TotalOxygenBalls { get => totalOxygenBalls; set => totalOxygenBalls = value; }
    public static int TimesDraggedRedMinigame { get => timesDraggedRedMinigame; set => timesDraggedRedMinigame = value; }
    public static int NumOxygenBallsScored { get => numOxygenBallsScored; set => numOxygenBallsScored = value; }
    public static float TotalRedTime { get => totalRedTime; set => totalRedTime = value; }
    public static bool MiniGameFinished { get => miniGameFinished; set => miniGameFinished = value; }

    //White cell
    public static int TotalWhiteScore { get => totalWhiteScore; }
    public static int MaxWhiteScore { get => maxWhiteScore; set => maxWhiteScore = value; }
    public static int MinWhiteScoreToUnlock { get => minWhiteScoreToUnlockNext; set => minWhiteScoreToUnlockNext = value; }
    public static int TotalAmmo { get => totalAmmo; set => totalAmmo = value; }
    public static int NumShootsScored { get => numShootsScored; set => numShootsScored = value; }
    public static float TotalWhiteTime { get => totalWhiteTime; set => totalWhiteTime = value; }
    public static int NumCellsInWound { get => numCellsInWound; set => numCellsInWound = value; }

    //Platalet
    public static int TotalPlataletScore { get => totalPlataletScore; }
    public static int MaxPlataletScore { get => maxPlataletScore; set => maxPlataletScore = value; }
    public static int NumPlatalets { get => numPlatalets; set => numPlatalets = value; }
    public static float TotalPlataletTime { get => totalPlataletTime; set => totalPlataletTime = value; }

    #endregion

    #region Reset

    public static void ResetVariables()
    {
        //Red cell
        totalRedScore = 0;
        TotalOxygenBalls = 0;
        NumOxygenBallsScored = 0;
        TotalRedTime = 0.0f;

        //White cell
        totalWhiteScore = 0;
        TotalAmmo = 0;
        NumShootsScored = 0;
        TotalWhiteTime = 0;

        //Platalet
        totalPlataletScore = 0;
        NumPlatalets = 0;
        TotalPlataletTime = 0.0f;
    }

    public static void ResetRedVariables()
    {
        totalRedScore = 0;
        TotalOxygenBalls = 0;
        NumOxygenBallsScored = 0;
        TotalRedTime = 0.0f;
    }

    public static void ResetWhiteVariables()
    {
        totalWhiteScore = 0;
        TotalAmmo = 0;
        NumShootsScored = 0;
        TotalWhiteTime = 0;
    }

    public static void ResetPlataletVariables()
    {
        totalWhiteScore = 0;
        TotalAmmo = 0;
        NumShootsScored = 0;
        TotalWhiteTime = 0;
    }

    #endregion

    #region Score calculation

    //                      RED
    /******************************************************/
    /// <summary>
    /// For oxygen balls, half score for surpasing finish line and other half for scoring it into the cell in one drag
    /// </summary>
    public static void CalculateFinalRedScore()
    {
        //Oxygen balls surpased finish line
        totalRedScore += totalOxygenBalls * pointsPerOxygenBall / 2;

        if(timesDraggedRedMinigame > 0)
        {
            totalRedScore += (numOxygenBallsScored / timesDraggedRedMinigame) * (pointsPerOxygenBall / 2);
        }

        //Time spent
        float timeFactor = totalRedTime / maxTotalRedTime;
        Debug.Log($"Red final score: {totalRedScore}, time: {totalRedTime} + score: {numOxygenBallsScored} + dragged: {timesDraggedRedMinigame}");
        if(Mathf.FloorToInt(timeFactor) != 0) totalRedScore /= Mathf.FloorToInt(timeFactor);
        Debug.Log($"Red final score: {totalRedScore}, timefactor: {timeFactor}");

        //Set the maximum score
        if (totalRedScore > maxRedScore)
        {
            maxRedScore = totalRedScore;
            GameData.redBestScore = maxRedScore;
        }
    }

    /// <summary>
    /// When colliding and loosing oxygen balls, it will be calculated with pointsPerOxygenBall / 2 as reference.
    /// </summary>
    /// <param name="impulse">force magnitud at which player has collided walls</param>
    /// <param name="time">when has collided</param>
    public static void OxygenFall(float impulse, float time)
    {
        float factor = (time / averageTravelTime) * (impulse / maxImpulse);
        int points = Mathf.FloorToInt(factor * (pointsPerOxygenBall / 2));
        totalRedScore += points;

        Debug.Log($"points: {points}, factor: {factor} + time: {time / maxTotalRedTime} + impulse: {impulse / 15000}");
    }

    //                      WHITE
    /******************************************************/
    public static void CalculateFinalWhiteScore()
    {
        totalWhiteScore += totalAmmo * pointsPerAmmo / 2;
        //The idea is to kill more cells than allowed to enter the wound
        totalWhiteScore += (numShootsScored - numCellsInWound) * (pointsPerAmmo / 2);

        //Time spent
        float timeFactor = totalWhiteTime / maxTotalWhiteTime;
        if (Mathf.FloorToInt(timeFactor) != 0)  totalWhiteScore /= Mathf.FloorToInt(timeFactor);

        Debug.Log($"points: {totalWhiteScore}, factor: {timeFactor} + scored: {numShootsScored} + in wound: {numCellsInWound}");

        //Set the maximum score
        if (totalWhiteScore > maxWhiteScore)
        {
            maxWhiteScore = totalWhiteScore;
            GameData.whiteBestScore = maxWhiteScore;
        }
    }

    public static void AmmoFall(float impulse, float time)
    {
        float factor = (time / averageTravelTime) * (impulse / maxImpulse);
        factor = factor > 1 ? 1 : factor;
        int points = Mathf.FloorToInt(factor * (pointsPerAmmo / 2));
        totalWhiteScore += points;
    }

    //                      PLATALET
    /******************************************************/
    public static void CalculateFinalPlataletScore()
    {
        int points = NumPlatalets * pointsPerPlatalet;
        float timeFactor = totalPlataletTime / 60;
        if (Mathf.FloorToInt(timeFactor) != 0) totalPlataletScore += Mathf.FloorToInt(points / timeFactor);

        //Set the maximum score
        if (totalPlataletScore > maxPlataletScore)
        {
            maxPlataletScore = totalPlataletScore;
            GameData.plataletBestScore = MaxPlataletScore;
        }
    }

    public static int MaxPuntuation()
    {
        int numInitialCargo = 10;
        return numInitialCargo * pointsPerOxygenBall;
    }

    #endregion
}
