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
    [SerializeField] private FlowerCodeSpawner Spawner;
    [SerializeField] private Image[] ImagesFlower;
    [SerializeField] private GameObject NotePanel;
    [SerializeField] private TextMeshProUGUI TextNote;
    [SerializeField] private FollowObjectUI FollowObject;

    [SerializeField] private Sprite[] Colours=new Sprite[6];
    

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool _nearNote;
    private bool _openNote;
    private Sprite _spriteColor;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
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
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
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

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    private Sprite GetSpriteByColor(string color)
    {
        switch (color)
        {
            case "RED": _spriteColor= Colours[0];break; 
            case "BLUE": _spriteColor = Colours[1]; break;
            case "YELLOW": _spriteColor= Colours[2]; break;
            case "CYAN": _spriteColor= Colours[3]; break;
            case "BLACK": _spriteColor= Colours[4]; break;
            case "WHITE": _spriteColor= Colours[5]; break;
        }
        return _spriteColor;
    }
    
    #endregion

} // class NoteFlower 
// namespace
