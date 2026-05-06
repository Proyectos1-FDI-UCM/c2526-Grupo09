//---------------------------------------------------------
// Detectar si el jugador gana / coge una flor.
// Hao Zheng
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Detecta si el jugador entra en contacto con esta para ganar / recoger las flores del último nivel.
/// </summary>
public class WinTriggerFlower : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [Header("Sólo para el nivel final")]
    [SerializeField] private FlowerUI FlowerUI;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private FlowerTypes _flower = null;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// método para detectar si colisiona con un objeto con trigger que tenga el componente platermovement
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _flower = GetComponent<FlowerTypes>();
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            // comprobamos que sea una flor del nivel final, si no, ignoramos todo este proceso y simplemente recogemos la flor y actualizamos el GUI.
            if (_flower != null)
            {
                FlowerUI.ManageUI(_flower.NumPetals, _flower.FlowerColor);

            } else if (LevelManager.Instance != null) // else, es una flor normal (final del nivel)
            {
                LevelManager.Instance.FlowerPicked();
            }
            Destroy(this.gameObject);
        } 
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion

} // class WinTriggerFlower 
// namespace
