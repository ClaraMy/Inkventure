using UnityEditor;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;
    public float cameraOffsetX = 2f;

    private float targetX;
    private Vector3 velocity;

    private void Start()
    {
        targetX = posOffset.x + cameraOffsetX;
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");

            if (horizontalMovement > 0)
            {
                targetX = posOffset.x + cameraOffsetX;
            }
            else if (horizontalMovement < 0)
            {
                targetX = posOffset.x - cameraOffsetX;
            }

            Vector3 targetPosition = new Vector3(targetX, posOffset.y, posOffset.z);
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + targetPosition, ref velocity, timeOffset);
            
        }
    }
}
