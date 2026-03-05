//---------------------------------------------------------
// Manager del sistema de diálogos. 
// Rafa Campos García.
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Este script convierte a una instancia en DialogManager, que permite controlar todo el sistema de diálogo y controlar la velocidad de las letras.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    /// <summary>
    /// Necesitamos de un canvas para mostrar el diálogo.
    /// </summary>
    [SerializeField] private DialogueUI dialogueUI;

    /// <summary>
    /// Velocidad de la reproducción de las letras del texto.
    /// </summary>
    [SerializeField] private float typingSpeed = 0.03f;


    /// <summary>
    /// Necesitamos de un player para frenar su movimiento.
    /// </summary>
    [SerializeField] private GameObject player;
    //[SerializeField] private AudioSource typingAudioSource;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    /// <summary>
    /// Propiedad global de solo lectura para otros scripts.
    /// </summary>
    public static DialogueManager Instance { get; private set; } 

    /// <summary>
    /// Control de turnos entre las listas de diálogos.
    /// </summary>
    private DialogueTurn currentTurn;

    /// <summary>
    /// Bool para la separación entre carácteres en curso.
    /// </summary>
    private bool _isTyping = false;

    /// <summary>
    /// Bool que espera al input (E)
    /// </summary>
    private bool _waitingForInput = false;

    /// <summary>
    /// Bool qe comprueba si hay un dialogo en proceso.
    /// </summary>
    private bool _isDialogInProgress = false;

    /// <summary>
    /// Cola de turnos de dialogo,, el primero qe entra es el primero qe sale.
    /// </summary>
    private Queue<DialogueTurn> _dialogueTurnQueue; 

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// En el Awake controlamos la instancia y la designamos como DialogManager.
    /// </summary>
    private void Awake()
    {
        //Esta es la instancia DialogueManager.

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //Inicializa ocultando el diálogo.
        dialogueUI.HideDialogBox();
    }

    /// <summary>
    /// Método de inicio de diálogo.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(DialogueRound dialogue)
    {
        //Impedir que se reproduzcan dos dialogos simultáneamente.
        if (_isDialogInProgress)
        {
            Debug.LogWarning($"Dialogue is already in progress");
            return;
        }

        //Diálogo en proceso.
        _isDialogInProgress = true;

        //Agarramos la lista de turnos y lo convertimos en la cola de dialogo tipo Queue.
        _dialogueTurnQueue = new Queue<DialogueTurn>(dialogue.DialogueTurnsList); 

        //Desactivamos el input del player.
        player.GetComponent<PlayerMovement>().enabled = false;

        //Mostramos caja de diálogo.
        dialogueUI.ShowDialogBox();

        //Método para reproducir el siguiente turno de la Queue.
        NextTurn();
    }

    /// <summary>
    /// Corrutina provisional para la aparición de carácteres en el texto de diálogo.
    /// </summary>
    /// <param name="dialogTurn"></param>

    private IEnumerator TypeSentence(DialogueTurn dialogTurn)
    {
        //Método para reproducir texto carácter por carácter. Es una corrutina provisional.
        var typingWaitSeconds = new WaitForSeconds(typingSpeed);

        foreach (char letter in dialogTurn.DialogueLine.ToCharArray())
        {
            dialogueUI.AppendToDialogArea(letter);

            //if (!char.IsWhiteSpace(letter))
                //typingAudioSource.Play();

            yield return typingWaitSeconds;
        }

        _isTyping = false;
        _waitingForInput = true; // ahora esperamos input manualmente
    }
   

    /// <summary>
    /// En el Update() controlamos la interacción con el input.
    /// </summary>
    void Update()
    {
        //Si no hay diálogo en proceso, sale inmediatamente de este método.
        if (!_isDialogInProgress)
            return;

        // Espera del input.
        if (_waitingForInput)
        {
            if (InputManager.Instance.InteractWasPressedThisFrame())
            {
                _waitingForInput = false;
                NextTurn();
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    /// <summary>
    /// Método para reproducir el siguiente turno de la lista en Queue.
    /// </summary>
    private void NextTurn()
    {
        //Si no hay mas turnos en la Queue = diálogo terminado.
        if (_dialogueTurnQueue.Count == 0)
        {
            EndDialogue();
            return;
        }


        currentTurn = _dialogueTurnQueue.Dequeue();

        //Adjuntamos información de los scriptableObjects en la caja de diálogo.
        dialogueUI.setCharacterInfo(currentTurn.Character);
        dialogueUI.ClearDialogArea();

        //Corrutina para la reproducción por carácteres.
        StartCoroutine(TypeSentence(currentTurn)); 

        //Mientras escribe, no podemos pasar al siguiente turno.
        _isTyping = true;
    }

    /// <summary>
    /// Método para terminar diálogo.
    /// </summary>
    private void EndDialogue()
    {
        dialogueUI.HideDialogBox();
        _isDialogInProgress = false;

        //Retomamos el input del player.
        player.GetComponent<PlayerMovement>().enabled = true;
    }
    #endregion   

} // class DialogueManager 
// namespace
