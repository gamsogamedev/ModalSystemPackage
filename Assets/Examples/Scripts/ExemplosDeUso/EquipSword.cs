using UnityEngine;

public class EquipSword : MonoBehaviour, IInteractable
{
    [SerializeField] private ModalScriptableObject modal;
    
    public void Interact(PlayerController player)
    {
        ModalManager.Instance.ShowModal(new ModalDetails
        {
            title = modal.title,
            
            layout =  modal.layout,
            message = modal.message,
            contentImage =  modal.contentImage,
            
            confirmText = modal.confirmText,
            onConfirm = () =>
            {
                player.EquipSword();
                player.EnableMovement();
                Destroy(gameObject);
            },
            
            cancelText = modal.cancelText,
            onCancel = player.EnableMovement
        });
    }


}
