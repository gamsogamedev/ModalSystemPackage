# ModalSystemPackage

* Tenha o prefab **ModalManagerCanvas** na hierarquia inicial do seu jogo.
* Crie a mensagem direto em código utilizando o construtor de um `ModalDetails`.
* Ou crie utilizando um **ModalScriptableObject** acessando `Create -> Modal Message -> Modal Details`.

// [Inserir imagem: ModalMessageSOCreation]

O `ModalDetails` é a classe base que armazena todas as informações necessárias para construir a janela.

```csharp
public class ModalDetails
{
    public string title;
    public string message;
    public Sprite contentImage;
    public ModalLayout layout = ModalLayout.Vertical;

    public string confirmText = "Confirmar";
    public Action onConfirm;

    public string cancelText; 
    public Action onCancel;

    public string alternativeText;
    public Action onAlternative;
}

```

| Parâmetro | Descrição |
| --- | --- |
| `Title` | Cabeçalho/Título da sua janela. |
| `Message` | A parte textual principal do conteúdo da sua mensagem. |
| `ContentImage` | Imagem (`Sprite`) de suporte para o conteúdo. |
| `Layout` | Define como a UI vai se alinhar internamente (**Vertical** ou **Horizontal**). |
| `Confirm/Cancel/Alt Text` | O texto que aparece dentro de cada botão respectivo. |
| `Actions (onConfirm/Cancel/Alt)` | A função (delegate) que será executada ao clicar no botão. |

> Para cada variável acima, caso você a deixe em branco, aquele elemento não será renderizado pelo Manager. O sistema reconstrói o layout pra se adequar apenas ao conteúdo preenchido.

Para criar a mensagem com **ModalScriptableObject**, a interface no inspetor segue exatamente a mesma nomenclatura.

// [Inserir imagem: InspectorViewSO]

---

### Como chamar o ModalManager

O método principal para invocar o sistema é `ModalManager.Instance.ShowModal()`. Ele tem dois overloads dependendo de como você deseja injetar os dados.

**1. Usando a classe ModalDetails:**
Ideal caso queira escrever a mensagem direto em código. *(Veja exemplos em `ChangeColor.cs` e `ChangeMovement.cs`)*.

```csharp
public void ShowModal(
    ModalDetails modalDetails, 
    bool autoCloseConfirm = true,  
    bool autoCloseCancel = true, 
    bool autoCloseAlt = true)

```

**2. Usando o ScriptableObject:**
Melhor se estiver trabalhando com um game designer separado ou simplesmente prefira escrever no inspetor *(Veja exemplos em `InitialTutorial.cs` e `EquipSword.cs`)*.

```csharp
public void ShowModal(
    ModalScriptableObject modalScriptableObject,
    Action onConfirm = null,
    Action onCancel = null,
    Action onAlternative = null,
    bool autoCloseConfirm = true,
    bool autoCloseCancel = true,
    bool autoCloseAlt = true)

```

> Como os `ScriptableObjects` da Unity não possuem suporte nativo para serializar eventos/ações em tempo de execução, as `Actions` tem que ser injetadas separadamente na hora de chamar o método.

#### O Parâmetro `autoClose`

Por padrão, o Manager fecha a janela instantaneamente quando qualquer botão é clicado. Caso você queira criar uma **sequência de mensagens**, você pode definir o `autoClose` do botão específico como `false` *(Exemplo em `InitialTutorial.cs`)*.

---

### Notas sobre Inputs e EventSystem

Para os botões funcionarem, certifique-se de que a sua cena possui um `EventSystem`, o Manager automaticamente gera um na hierarquia caso não tenha, mas é uma boa prática adicionar o seu próprio