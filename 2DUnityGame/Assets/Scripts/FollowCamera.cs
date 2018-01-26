using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This script is an example of a follow camera script found online.
    /// We can learn from this code and use it as an implementation for now.
    /// 
    /// Souce: https://answers.unity.com/questions/29183/2d-camera-smooth-follow.html
    /// Github: https://gist.github.com/unity3diy/5aa0b098cb06b3ccbe47
    /// </summary>
    public class FollowCamera : MonoBehaviour
    {

        public float InterpVelocity;
        public float MinDistance;
        public float FollowDistance;
        public GameObject Target;
        public Vector3 Offset;
        Vector3 _targetPos;
        // Use this for initialization
        void Start()
        {
            _targetPos = transform.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Target)
            {
                Vector3 posNoZ = transform.position;
                posNoZ.z = Target.transform.position.z;

                Vector3 targetDirection = (Target.transform.position - posNoZ);

                InterpVelocity = targetDirection.magnitude * 5f;

                _targetPos = transform.position + (targetDirection.normalized * InterpVelocity * Time.deltaTime);

                transform.position = Vector3.Lerp(transform.position, _targetPos + Offset, 0.25f);

            }
        }
    }
}

// Original post with image here  >  http://unity3diy.blogspot.com/2015/02/unity-2d-camera-follow-script.html