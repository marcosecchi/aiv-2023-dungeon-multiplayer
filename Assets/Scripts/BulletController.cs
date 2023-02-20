using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace DungeonCrawler
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletController : NetworkBehaviour
    {
        private Rigidbody _rb;

        public float speedMultiplier = 10;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.velocity = transform.forward * speedMultiplier;
            
            Invoke(nameof(SelfDestroy), 5f);
        }

        [Server]
        void SelfDestroy()
        {
            Debug.Log("Self Destroy");
            NetworkServer.Destroy(gameObject);
        }

        [ServerCallback]
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision with " + collision.gameObject.name);
            // Make some damage
            
            SelfDestroy();
        }
    }
}
