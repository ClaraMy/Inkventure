using UnityEditor;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float m_TargetX;

    [SerializeField] private float m_TimeOffset = 0.5f;
    [SerializeField] private float m_CameraOffsetX = 4f;

    private Vector3 m_Velocity;

    [SerializeField] private Vector3 m_PosOffset = new(0f, 1f, -10f);

    private SpriteRenderer m_PlayerSprite;
    private GameObject m_Player;

    private void Start()
    {
        m_TargetX = m_PosOffset.x + m_CameraOffsetX;
        m_Player = PlayerMovement.Instance.gameObject;
        m_PlayerSprite = PlayerMovement.Instance.GetComponent<SpriteRenderer>();
    }
 
    void Update()
    {
        // Check if the player GameObject is not null
        if (m_Player != null)
        {
            // Determine the target X position based on the player's direction
            if (m_PlayerSprite.flipX == false)
            {
                m_TargetX = m_PosOffset.x + m_CameraOffsetX;
            }
            else
            {
                m_TargetX = m_PosOffset.x - m_CameraOffsetX;
            }

            // Calculate the target position for the camera
            Vector3 targetPosition = new Vector3(m_TargetX, m_PosOffset.y, m_PosOffset.z);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, m_Player.transform.position + targetPosition, ref m_Velocity, m_TimeOffset);
            
        }
    }
}
