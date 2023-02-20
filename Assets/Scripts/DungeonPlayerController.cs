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

        [SyncVar(hook = nameof(OnColorChanged))]
        public int colorIndex = -1;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            
            colorIndex = NetworkServer.connections.Count - 1 ;
            OnColorChanged(0, 0);
        }

        void OnColorChanged(int _, int newValue)
        {
            var mat = Resources.Load<Material>("Materials/Mat_" + newValue);
            var rend = GetComponentsInChildren<Renderer>();
            if (mat != null)
            {
                foreach (var r in rend)
                {
                    r.material = mat;
                }
            }
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
