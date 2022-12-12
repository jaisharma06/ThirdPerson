using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int m_targetFrameRate = 120;
    [SerializeField] private CursorLockMode m_cursorLockMode = CursorLockMode.Locked;

    private void Awake()
    {
        Application.targetFrameRate = m_targetFrameRate;
        Cursor.visible = false;
        Cursor.lockState = m_cursorLockMode;
    }
}
