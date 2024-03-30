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
