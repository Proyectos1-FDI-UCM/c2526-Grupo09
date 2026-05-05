//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Hao Zheng
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class NoteFlower : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private FlowerCodeSpawner Spawner;
    [SerializeField] private Image[] ImagesFlower;
    [SerializeField] private GameObject NotePanel;
    [SerializeField] private TextMeshProUGUI TextNote;
    [SerializeField] private FollowObjectUI FollowObject;


    [SerializeField] private Sprite Red;
    [SerializeField] private Sprite Blue;
    [SerializeField] private Sprite Cyan;
    [SerializeField] private Sprite Yellow;
    [SerializeField] private Sprite Black;
    [SerializeField] private Sprite White;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private bool _nearNote;
    private bool _openNote;
    private Sprite _spriteColor;

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
    void Start()
    {
        NotePanel.SetActive(false);
        _openNote = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //solo si esta tocando la nota y presionando E se ejecutara
        if (_nearNote && InputManager.Instance.InteractWasPressedThisFrame())
        {
            if (_openNote)
            {
                HideNote();
            }
            else ShowNote();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _nearNote = true;
            FollowObject.SetNewTarget(transform);
            FollowObject.ChangeText("interact");
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _nearNote = false;
            FollowObject.Deactivate();

        }
    }

    /// <summary>
    /// enseña la nota que contiene el código
    /// </summary>
    public void ShowNote()
    {
        //recorre el array de las 4 flores y coge sus sprites
        FlowerTypes[] flowers = Spawner.GetFlowersArray();
        for (int i = 0; i < flowers.Length; i++)
        {
            Sprite newSprite = GetSpriteByColor(flowers[i].FlowerColor);
            ImagesFlower[i].sprite = newSprite;
        }

        NotePanel.SetActive(true);
        _openNote = true;
        TextNote.text = "Pray thy God may save us, for this world is doomed.\r\nHope is a fragile thing… yet it lingers.\r\n\r\nFour flowers remain.\r\nFollow their hues, and heed what they carry.\r\n\r\nFor even in silence, they speak.";
    }
    public void HideNote()
    {

        NotePanel.SetActive(false);
        _openNote = false;

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
    private Sprite GetSpriteByColor(string color)
    {
        switch (color)
        {
            case "RED": _spriteColor= Red;break; 
            case "BLUE": _spriteColor = Blue;break;
            case "YELLOW": _spriteColor= Yellow;break;
            case "CYAN": _spriteColor= Cyan;break;
            case "BLACK": _spriteColor= Black;break;
            case "WHITE": _spriteColor= White;break;
        }
        return _spriteColor;
    }
    
    #endregion

} // class NoteFlower 
// namespace
