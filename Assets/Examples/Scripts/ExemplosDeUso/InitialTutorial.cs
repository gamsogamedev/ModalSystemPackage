using UnityEngine;

public class InitialTutorial : MonoBehaviour
{
    [Header("Modal References")]
    [SerializeField] private ModalScriptableObject tutorial1;
    [SerializeField] private ModalScriptableObject tutorial2;
    
    [SerializeField] private PlayerController player;
    
    private void Start()
    {
        ShowTutorial1();
    }
    
    
    private void ShowTutorial1()
    {
        player.DisableMovement();
        ModalManager.Instance.ShowModal(tutorial1, onConfirm: ShowTutorial2, autoCloseConfirm: false);
    }

    private void ShowTutorial2()
    {
        ModalManager.Instance.ShowModal(tutorial2, onConfirm: player.EnableMovement, onCancel: ShowTutorial1, autoCloseCancel: false);
    }
    
}
