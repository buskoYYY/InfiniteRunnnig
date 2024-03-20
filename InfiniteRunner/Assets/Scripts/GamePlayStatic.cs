using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayStatic
{
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
}
