using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private MainMenu _menu;

    public void ClosePanel() => _menu.ShowMainMenu();
}