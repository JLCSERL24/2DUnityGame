using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This class provides physics for all objects in our game.
    /// Intances of objects and specific enemies or characters will inherit from their class when
    /// their physics is scripted.
    /// 
    /// Note: For the time being, this is not original code. 
    /// Started with the unity2D game development code to use as a framework to learn from and build upon.
    /// 
    /// See the original souce code and tutorial here: https://unity3d.com/learn/tutorials/topics/2d-game-creation/intro-and-session-goals?playlist=17093
    /// </summary>
    public class PhysicsObject : MonoBehaviour
    {

        public float MinGroundNormalY = .65f;
        public float GravityModifier = 1f;

        protected Vector2 TargetVelocity;
        protected bool Grounded;
        protected Vector2 GroundNormal;
        protected Rigidbody2D Rb2D;
        protected Vector2 Velocity;
        protected ContactFilter2D ContactFilter;
        protected RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
        protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);


        protected const float MinMoveDistance = 0.001f;
        protected const float ShellRadius = 0.01f;

        /// <summary>
        /// OnEnable is called when the object becomes available and active.
        /// Called during initialization step after Awake but before start.
        /// </summary>
        void OnEnable()
        {
            Rb2D = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            ContactFilter.useTriggers = false;
            ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            ContactFilter.useLayerMask = true;
        }

        void Update()
        {
            TargetVelocity = Vector2.zero;
            ComputeVelocity();
        }

        protected virtual void ComputeVelocity()
        {

        }

        void FixedUpdate()
        {
            Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
            Velocity.x = TargetVelocity.x;

            Grounded = false;

            Vector2 deltaPosition = Velocity * Time.deltaTime;

            Vector2 moveAlongGround = new Vector2(GroundNormal.y, -GroundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement(move, false);

            move = Vector2.up * deltaPosition.y;

            Movement(move, true);
        }

        void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > MinMoveDistance)
            {
                int count = Rb2D.Cast(move, ContactFilter, HitBuffer, distance + ShellRadius);
                HitBufferList.Clear();
                for (int i = 0; i < count; i++)
                {
                    HitBufferList.Add(HitBuffer[i]);
                }

                for (int i = 0; i < HitBufferList.Count; i++)
                {
                    Vector2 currentNormal = HitBufferList[i].normal;
                    if (currentNormal.y > MinGroundNormalY)
                    {
                        Grounded = true;
                        if (yMovement)
                        {
                            GroundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(Velocity, currentNormal);
                    if (projection < 0)
                    {
                        Velocity = Velocity - projection * currentNormal;
                    }

                    float modifiedDistance = HitBufferList[i].distance - ShellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }


            }

            Rb2D.position = Rb2D.position + move.normalized * distance;
        }

    }
}