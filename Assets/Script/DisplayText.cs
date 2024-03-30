using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    [SerializeField] private Text m_TextInformationKeyboard;
    [SerializeField] private Text m_TextInformationGamepad;

    private string[] m_JoystickNames;
    private void Start()
    {
        m_JoystickNames = Input.GetJoystickNames();
    }

    /// <summary>
    /// Called when the Collider2D component enters another collider trigger.
    /// Enables the appropriate UI text based on the presence of a gamepad.
    /// </summary>
    /// <param name="collision">The Collider2D that triggered the event.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (m_JoystickNames.Length > 0)
            {
                m_TextInformationGamepad.enabled = true;
            } else
            {
                m_TextInformationKeyboard.enabled = true;
            }
        }
    }

    /// <summary>
    /// Called when the Collider2D component exits another collider trigger.
    /// Disables the appropriate UI text based on the presence of a gamepad.
    /// </summary>
    /// <param name="collision">The Collider2D that triggered the event.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (m_JoystickNames.Length > 0)
            {
                m_TextInformationGamepad.enabled = false;
            }
            else
            {
                m_TextInformationKeyboard.enabled = false;
            }
        }
    }
}
