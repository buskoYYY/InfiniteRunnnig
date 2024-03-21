using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayStatic
{
    static GameMode gameMode;
    public static bool IsPositionOccupied(Vector3 position, Vector3 detectionHalfExtend, string OccupationCheckTag)
    {
        {
            Collider[] colliders = Physics.OverlapBox(position, detectionHalfExtend);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag == OccupationCheckTag)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static GameMode GetGameMode()
    {
        if(gameMode == null)
        {
            gameMode = GameObject.FindObjectOfType<GameMode>();
        }
        return gameMode;
    }
}
