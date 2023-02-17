using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace DungeonCrawler
{
    [RequireComponent(typeof(CharacterController))]
    public class DungeonPlayerController : NetworkBehaviour
    {
        private CharacterController _controller;
        private Animator _animator;
        
        public float moveSpeed = 3;
        public float rotationSpeed = 1;
        public GameObject marker;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            marker.SetActive(isLocalPlayer);

            if (!isLocalPlayer) return;
            var vMove = Input.GetAxis("Vertical");
            vMove = Mathf.Clamp(vMove, 0, 1);
            var hMove = Input.GetAxis("Horizontal");
            var tr = transform;
    
            transform.Rotate(Vector3.up * (hMove * rotationSpeed));
            _controller.SimpleMove(tr.forward * (vMove * moveSpeed));

            if (_animator != null)
            {
                _animator.SetFloat("Speed", _controller.velocity.magnitude);
            }
        }
    }
}
