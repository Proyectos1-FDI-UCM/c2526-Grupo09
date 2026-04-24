//---------------------------------------------------------
// Manager del sistema de diálogos. 
// Rafa Campos García.
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

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


    /// <summary>
    /// Audio que suena mientras el dialogo habla
    /// </summary>
    [SerializeField] private AudioSource dialogSound;

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
    /// Bool para comprobar si muestra la línea de dialog entera
    /// </summary>
    private bool _skipTyping;

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
    private List<DialogueTurn> _dialogueTurnList;
    private int _currentTurnIndex = 0;

    /// <summary>
    /// Atributos para la aparición de caracteres uno por uno.
    /// </summary>
    private string _pendingLine = "";
    private int _currentCharIndex = 0;
    private float _typingTimer = 0f;

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
        //Inicializamos los turnos de diálogo
        _dialogueTurnList = dialogue.DialogueTurnsList;
        _currentTurnIndex = 0;

        //Impedir que se reproduzcan dos dialogos simultáneamente.
        if (_isDialogInProgress)
        {
            Debug.LogWarning($"Dialogue is already in progress");
            return;
        }

        //Diálogo en proceso.
        _isDialogInProgress = true;

        //Desactivamos el input del player.
        player.GetComponent<PlayerMovement>().enabled = false;

        //Mostramos caja de diálogo.
        dialogueUI.ShowDialogBox();

        //Método para reproducir el siguiente turno de la Queue.
        NextTurn();
    }

  


    /// <summary>
    /// En el Update() controlamos la interacción con el input.
    /// </summary>
    void Update()
    {
        //Si no hay dialogo en progreso,, no hace nada
        if (!_isDialogInProgress) return;

        //Tipeo letra a letra
        if (_isTyping && !_skipTyping)
        {
            _typingTimer += Time.deltaTime;
            while (_typingTimer >= typingSpeed && _currentCharIndex < _pendingLine.Length)
            {
                dialogueUI.AppendToDialogArea(_pendingLine[_currentCharIndex].ToString());
                _currentCharIndex++;
                _typingTimer -= typingSpeed;
            }

            if (_currentCharIndex >= _pendingLine.Length)
            {
                _isTyping = false;
                if (dialogSound != null)
                {
                    dialogSound.Stop();
                }
            }
        }

        //Skipear tipeo
        if (_isTyping && _skipTyping)
        {
            //Pasamos el resto del texto que queda, usando substring.
            string remaining = _pendingLine.Substring(_currentCharIndex);
            dialogueUI.AppendToDialogArea(remaining);
            _isTyping = false;
            _skipTyping = false;
            Debug.Log("Terminó de escribir (skip)");

            if (dialogSound != null)
            {
                dialogSound.Stop();
            }
        }

        // Input
        if (_waitingForInput && InputManager.Instance.InteractWasPressedThisFrame())
        {
            if (_isTyping)
            {
                _skipTyping = true;
            }
            else
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

    public bool GetIsDialogueInProgress()
    {
        return _isDialogInProgress;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void StartTyping(DialogueTurn dialogTurn)
    {
        _pendingLine = dialogTurn.DialogueLine;
        _currentCharIndex = 0;
        _typingTimer = 0f;
        _isTyping = true;
        _skipTyping = false;
        _waitingForInput = true;
        dialogueUI.ClearDialogArea();

        if (dialogSound != null)
        {
            dialogSound.Play();
        }
    }

    /// <summary>
    /// Método para reproducir el siguiente turno de la lista en Queue.
    /// </summary>
    private void NextTurn()
    {
        //Si no hay mas turnos en la Queue = diálogo terminado.
        if (_currentTurnIndex >= _dialogueTurnList.Count)
        {
            EndDialogue();
            return;
        }

        currentTurn = _dialogueTurnList[_currentTurnIndex];
        _currentTurnIndex++;

        //Adjuntamos información de los scriptableObjects en la caja de diálogo.
        dialogueUI.setCharacterInfo(currentTurn.GetCharacter());
        dialogueUI.ClearDialogArea();

        //Reproducción por carácteres.
        StartTyping(currentTurn);

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

        if (dialogSound != null)
        {
            dialogSound.Stop();
        }
    }

    private void SoundLogic()
    {
        
    }
    #endregion   

} // class DialogueManager 
// namespace
