//---------------------------------------------------------
// Movimiento del jugador y animaciones del movimiento
// Rafa Campos García
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    /// <summary>
    /// velocidad del jugador al caminar
    /// </summary>
    [SerializeField]
    private float WalkSpeed = 2.0f;
    /// <summary>
    /// velocidad del jugador al correr
    /// </summary>
    [SerializeField]
    private float RunSpeed = 3.5f;

    /// <summary>
    /// audio que suena al caminar/correr
    /// </summary>
    [SerializeField]
    private AudioSource walkSound;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)


    Vector3 _posAnterior;  //  posición que tenía antes el jugador
    Vector3 _posActual;  // posición actual del jugador

    /// <summary>
    /// booleano que se encarga de saber si es jugador esta escondido o no
    /// </summary>
    private bool _isHidden = false;
    private Animator _animator;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// En cada frame se mueve al jugador
    /// </summary>
    void Start()
    {
        _animator = GetComponent<Animator>();
        Debug.Log("PLAYER POS: " + transform.position);
        Debug.Log("PLAYER ACTIVE: " + gameObject.activeSelf);
        Debug.Log("SPRITE ENABLED: " + GetComponent<SpriteRenderer>().enabled);
    }

    void Update()
    {
        if (!PauseManager.Instance.Pause)
        {
            _animator.enabled = true;

            if (!_isHidden)
            {

                _posAnterior = transform.position;
                MovePlayer();
                // Solo intentamos hacer ruido si nos hemos movido Y tenemos el script de ruido
                if (_posAnterior != transform.position)
                {
                    PlayerNoise _playerNoise = this.gameObject.GetComponent<PlayerNoise>();
                    if (_playerNoise.enabled)
                    {
                        _playerNoise.PlayerMoving();
                    }
                }
            }
        }
        else
        {
            _animator.enabled = false;
        }
    }


    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Permite a otros scripts consultar si el jugador está escondido.
    /// </summary>
    public bool GetIsHidden()
    {
        return _isHidden;
    }

    /// <summary>
    /// Permite al escondite cambiar el estado de visibilidad del jugador.
    /// </summary>
    public void SetHidden(bool hidden)
    {
        _isHidden = hidden;
        Debug.Log("se ha cambiado isHiden a: " + GetIsHidden());
    }

    public float GetWalkSpeed()
    {
        return WalkSpeed;
    }

    public void SetWalkSpeed(float value)
    {
        WalkSpeed = value;
    }

    public float GetRunSpeed()
    {
        return RunSpeed;
    }

    public void SetRunSpeed(float value)
    {
        RunSpeed = value;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    /// <summary>
    /// Método que se encarga de manejar el movimeinto del jugador
    /// </summary>
    void MovePlayer()
    {
        //Obtenemos la dirección del InputManager
        Vector2 direction = InputManager.Instance.MovementVector;
        //Debug.Log(direction);

        //Variable para guardar la velocidad que se usa en ese momento
        float currentSpeed = WalkSpeed;
        // ¿Hay input de movimiento?
        bool isMoving = direction.magnitude > 0.1f;


        int incrX = Mathf.RoundToInt(direction.x);
        int incrY = Mathf.RoundToInt(direction.y);

        //Comprobamos si se pulsa el Shift
        if (incrY == 0 && incrX == 0)
        {
            _animator.SetInteger("Direction", 0); // Idle
        }
        else if (incrY > 0 && incrX == 0)
        {
            _animator.SetInteger("Direction", 2); // Arriba
        }
        else if (incrY < 0 && incrX == 0)
        {
            _animator.SetInteger("Direction", 1); // Abajo
        }
        else if (incrX < 0 && incrY == 0)
        {
            _animator.SetInteger("Direction", 3); // Izquierda
        }
        else if (incrX > 0 && incrY == 0)
        {
            _animator.SetInteger("Direction", 4); // Derecha
        }

        if (InputManager.Instance.RunIsPressed() && isMoving)
        {

            //Usamos la velocidad de correr
            currentSpeed = RunSpeed;
            //Cambiamos velocidad de animacion.
            _animator.speed = 1.5f;

        } else
        {
            _animator.speed = 1f;
        }

        // Aplicamos el movimiento
        // Multiplicamos la dirección por la velocidad y el tiempo
       transform.Translate(direction * currentSpeed * Time.deltaTime);

        if (isMoving && !_isHidden)
        {
            if (currentSpeed == RunSpeed)
            {
                walkSound.volume = 1.0f; // Volumen máximo al correr
                walkSound.pitch = 1.2f;  // Opcional: un poco más agudo al correr suena más realista
            }
            else
            {
                walkSound.volume = 0.5f; // Volumen medio al caminar
                walkSound.pitch = 1.0f;  // Pitch normal
            }

            // Si no estaba sonando, lo activamos
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
        }
        else
        {
            // Si nos detenemos, paramos el sonido
            walkSound.Stop();
        }
    }
    #endregion

} // class PlayerMovement 
// namespace
