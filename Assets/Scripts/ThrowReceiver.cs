using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class ThrowReceiver : MonoBehaviour
    {
        public void Throw()
        {
            var shooter = GetComponentInParent<DungeonShooter>();
            if (shooter != null)
            {
                shooter.Fire();
            }
        }
    }
}
