using UnityEngine;

public class ChangeMovement : MonoBehaviour, IInteractable
{
    
    public void Interact(PlayerController player)
    {
        ModalManager.Instance.ShowModal(new ModalDetails
        {
            title = "Mudar velocidade de movimento",
            message = "Essa mensagem está no script ChangeMovement e mostra como criar um modal direto em código",
            
            confirmText =  "Aumentar velocidade",
            onConfirm =  () =>
            {
                player.moveSpeed += 5f;
                player.EnableMovement();
            },
            
            cancelText =  "Cancelar",
            onCancel =   () =>
            {
                player.EnableMovement();
            },
            
            alternativeText =   "Resetar a velocidade",
            onAlternative =   () =>
            {
                player.moveSpeed = 5f;
                player.EnableMovement();
            }
        });
    }
}
