using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Pickup
{
    [SerializeField] int scoreIncrease;

    public override void OnPickup()
    {
        GameManager.Instance.IncreaseScore(scoreIncrease);
    }
}
