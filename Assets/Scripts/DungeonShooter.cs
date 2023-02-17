using System;
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

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (!isLocalPlayer) return;
            if (Input.GetButtonDown("Fire1"))
            {
                CmdStartFireSequence();
            }
        }

        [Command]
        void CmdStartFireSequence()
        {
            RpcOnFireStarted();
        }

        [ClientRpc]
        void RpcOnFireStarted()
        {
            if (_animator != null)
            {
                _animator.SetTrigger("Shoot");
            }
        }

        public void Fire()
        {
            if (!isLocalPlayer) return;
            CmdFire();
        }

        [Command]
        void CmdFire()
        {
            var go = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.Spawn(go);
        }
    }
}
