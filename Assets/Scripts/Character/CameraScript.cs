using Parent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
   public class CameraScript : MonoBehaviour
   {
      [SerializeField] private float RotationPower = 10;
      [SerializeField] private float HorizontalDamping = 1;
      [SerializeField] private GameObject FollowTarget;

      private Transform FollowTargetTransform;
      private Vector2 PreviousMouseDelta = Vector2.zero;

      private new void Awake()
      {
         FollowTargetTransform = FollowTarget.transform;
      }
      
      private void OnLook(InputValue obj)
      {
         Vector2 aimValue = obj.Get<Vector2>();

         Quaternion addedRotation = Quaternion.AngleAxis(
            Mathf.Lerp(PreviousMouseDelta.x, aimValue.x, 1f / HorizontalDamping) * RotationPower,
            transform.up);

         FollowTargetTransform.rotation *= addedRotation;

         PreviousMouseDelta = aimValue;
         
         transform.rotation = Quaternion.Euler(0, FollowTargetTransform.rotation.eulerAngles.y, 0);
         
         FollowTargetTransform.localEulerAngles = Vector3.zero;
      }
   }
}
   