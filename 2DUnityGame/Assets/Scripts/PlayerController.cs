using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This class is responsible for handling player scripting.
    /// 
    /// Link to source: https://unity3d.com/learn/tutorials/topics/2d-game-creation/intro-and-session-goals?playlist=17093
    /// </summary>
    public class PlayerController : PhysicsObject
    {
        public float MaxSpeed = 7;
        public float JumpTakeOffSpeed = 7;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        // Use this for initialization
        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        protected override void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            move.x = Input.GetAxis("Horizontal");
            if (move.x != 0)
            {
                _animator.SetBool("isRunning", true);
            }
            else if (move.x == 0)
            {
                _animator.SetBool("isRunning", false);
            }

            if (Input.GetButtonDown("Jump") && Grounded)
            {
                Velocity.y = JumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (Velocity.y > 0)
                {
                    Velocity.y = Velocity.y * 0.5f;
                }
            }

            bool flipSprite = (_spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.00f));
            if (flipSprite)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
            }

            _animator.SetBool("grounded", Grounded);
            _animator.SetFloat("velocityX", Mathf.Abs(Velocity.x) / MaxSpeed);

            TargetVelocity = move * MaxSpeed;
        }
    }
}