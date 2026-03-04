//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
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

    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private InputManager input;
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

    //propiedad global de solo lectura para otros scripts
    public static DialogueManager Instance { get; private set; } 

    private DialogueTurn currentTurn;
    //bool para la separación entre carácteres
    private bool isTyping = false;
    //bool que espera al input
    private bool waitingForInput = false;
    //bool qe comprueba si hay un dialogo en proceso
    private bool isDialogInProgress = false;
    //cola de turnos de dialogo,, el primero qe entra es el primero qe sale.
    private Queue<DialogueTurn> dialogueTurnQueue; 

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    /// 
    private void Awake()
    {
        //esta es la instancia DialogueManager

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //Inicializa ocultando el diálogo.
        dialogueUI.HideDialogBox();
    }

    public void StartDialogue(DialogueRound dialogue) //metodo publico de facil acceso67
    {
        //Impedir que se reproduzcan dos dialogos simultáneamente.
        if (isDialogInProgress)
        {
            Debug.LogWarning($"Dialogue is already in progress");
            return;
        }

        //Diálogo en proceso.
        isDialogInProgress = true;

        //Agarramos la lista de turnos y lo convertimos en la cola de dialogo tipo Queue.
        dialogueTurnQueue = new Queue<DialogueTurn>(dialogue.DialogueTurnsList); 

        //Desactivamos el input del player.
        player.GetComponent<PlayerMovement>().enabled = false;

        //Mostramos caja de diálogo.
        dialogueUI.ShowDialogBox();

        //Método para reproducir el siguiente turno de la Queue.
        NextTurn();
    }

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

        isTyping = false;
        waitingForInput = true; // ahora esperamos input manualmente
    }
   

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Si no hay diálogo en proceso, sale inmediatamente de este método.
        if (!isDialogInProgress)
            return;

        // Espera del input.
        if (waitingForInput)
        {
            if (input.InteractWasPressedThisFrame())
            {
                waitingForInput = false;
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

    private void NextTurn()
    {
        //Si no hay mas turnos en la Queue = diálogo terminado.
        if (dialogueTurnQueue.Count == 0)
        {
            EndDialogue();
            return;
        }


        currentTurn = dialogueTurnQueue.Dequeue();

        //Adjuntamos información de los scriptableObjects en la caja de diálogo.
        dialogueUI.setCharacterInfo(currentTurn.Character);
        dialogueUI.ClearDialogArea();

        //Corrutina para la reproducción por carácteres.
        StartCoroutine(TypeSentence(currentTurn)); 

        //Mientras escribe, no podemos pasar al siguiente turno.
        isTyping = true;
    }


    private void EndDialogue()
    {
        dialogueUI.HideDialogBox();
        isDialogInProgress = false;

        //Retomamos el input del player.
        player.GetComponent<PlayerMovement>().enabled = true;
    }
    #endregion   

} // class DialogueManager 
// namespace
