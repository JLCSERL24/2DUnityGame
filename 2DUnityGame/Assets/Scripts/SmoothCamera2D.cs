using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This script is an example of a follow camera script found online.
    /// We can learn from this code and use it as an implementation for now.
    /// 
    /// Source: https://answers.unity.com/questions/29183/2d-camera-smooth-follow.html
    /// Note this one is a bit old.  I suspect we can get more out of the other one.
    /// </summary>
    public class SmoothCamera2D : MonoBehaviour
    {

        public float DampTime = 0.15f;
        private Vector3 _velocity = Vector3.zero;
        public Transform Target;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Target)
            {
                var point = GetComponent<Camera>().WorldToViewportPoint(Target.position);
                var delta = Target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
                var destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, DampTime);
            }

        }
    }
}
