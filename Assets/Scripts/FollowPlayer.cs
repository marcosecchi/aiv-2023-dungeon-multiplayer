using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class FollowPlayer : MonoBehaviour
    {
        private Transform _target;
        private bool _isSearching;

        private void Start()
        {
            StartCoroutine(nameof(FindPlayer));
        }

        IEnumerator FindPlayer()
        {
            _isSearching = true;
            while (true)
            {
                yield return new WaitForSeconds(.5f);
                var player = FindObjectOfType<DungeonPlayerController>();
                if (player != null && player.isLocalPlayer)
                {
                    _target = player.transform;
                    _isSearching = false;
                    StopCoroutine(nameof(FindPlayer));
                }
            }
        }
        
        void Update()
        {
            if (_target != null)
            {
                transform.position = _target.position;
                return;
            }
            else if(!_isSearching)
            {
                StartCoroutine(nameof(FindPlayer));
            }

        }
    }
}
