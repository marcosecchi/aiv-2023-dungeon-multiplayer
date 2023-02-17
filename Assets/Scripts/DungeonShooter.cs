using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace DungeonCrawler
{
    public class DungeonShooter : NetworkBehaviour
    {
        public GameObject prefab;
        public Transform spawnPoint;

        void Update()
        {
            if (!isLocalPlayer) return;
            if (Input.GetButtonDown("Fire1"))
            {
                CmdFire();
            }
        }

        [Command]
        void CmdFire()
        {
            var go = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.Spawn(go);
            RpcOnBulletFired();
        }

        [ClientRpc]
        void RpcOnBulletFired()
        {
            Debug.Log("Fired");
        }
    }
}
