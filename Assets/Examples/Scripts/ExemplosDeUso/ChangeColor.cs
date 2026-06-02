using UnityEngine;

public class ChangeColor : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite blueKnight;
    [SerializeField] private Sprite redKnight;

    private Sprite oppositeColor;
    
    public void Interact(PlayerController player)
    {
        ModalManager.Instance.ShowModal(new ModalDetails
        {
            title = "ChangeColor",
            contentImage = ReturnOpposite(player.GetPlayerSprite()),
            message = "O script ChangeColor mostra como manipular a mensagem com variáveis.",
            layout = ModalLayout.Horizontal,
            
            confirmText = "Trocar de cor",
            onConfirm = () =>
            {
                player.SetPlayerSprite(ReturnOpposite(player.GetPlayerSprite()));
                player.EnableMovement();
            },
            
            cancelText = "Manter atual",
            onCancel = player.EnableMovement,
            
        });
    }

    private Sprite ReturnOpposite(Sprite sprite)
    {
        return sprite == blueKnight ? redKnight : blueKnight;
    }
}
