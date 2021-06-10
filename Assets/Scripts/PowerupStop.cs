using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class PowerupStop : Powerup
    {
        public override void OnPickedByBike(Bike bike)
        {
            bike.Braking();
        }
    }
}
