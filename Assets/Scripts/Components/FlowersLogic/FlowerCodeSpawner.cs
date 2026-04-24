//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Diego Martín
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using

/* -- NOTAS PERSONALES (BORRALO LUEGO) --
 * este script va a:
 * 1. coger 4 de los 6 tipos de flores que hay disponibles
 * 2. cada vez que elige un tipo, instanciar una flor, ponerla en una posicion y ponerle el tipo
 * 3. guardar el codigo de cada flor
 * 4. guardar los colores usados para luego randomizarlos 
 *      para el orden del codigo de la puerta?? (o no random, en orden de aparicion, que ya es random)
*/

/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class FlowerCodeSpawner : MonoBehaviour
{

    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    // posiciones de las flores
    [SerializeField]
    private Transform[] Positions;

    [SerializeField]
    private GameObject FlowerCodePrefab;

    [SerializeField]
    private Padlock Padlock = null;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)


    PlayerMovement _player;

    // array de flores que componen el código de 4 dígitos
    FlowerTypes[] _flowers = new FlowerTypes[4];

    GameObject _singleFlower;

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // cuando el jugador pasa por el trigger, se crean todas las flores
        _player = collision.GetComponent<PlayerMovement>();
        if (_player != null)
        {
            Debug.Log("detecta al player");
            GenerateFlowers();
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Metodo utilizado para coger el array de flores en el script PadLock y generar el codigo.
    /// </summary>
    /// <returns></returns>
    public FlowerTypes[] GetFlowersArray()
    {
        return _flowers;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    
    private void GenerateFlowers()
    {
        // array de posiciones ya escogidas (para que no haya repetición a la hora de colocar las flores)
        // cada posición estará atada a una flor en concreto, por lo que las flores que aparecen siempre estarán en el mismo sitio
        // Ejemplo: la flor ORANGE siempre aparecerá en la position 3, la BLUE en la 1, etc
        int[] numbers = new int[_flowers.Length];

        bool alreadyChosen = false;
        for (int i = 0; i < _flowers.Length; i++)
        {
            // índice de la posición en la que se encontrará la flor
            int index = 0;
            do
            {
                if (alreadyChosen)
                {
                    alreadyChosen = false;
                }
                Debug.Log("genera un num random");
                index = Random.Range(0, Positions.Length);

                int j = 0;
                // ignoramos esta parte del código si es la primera flor creada, ya que no hay que buscar ninguna
                // posición ya escogida (no hay ninguna)
                if (i != 0)
                {
                    // comprobamos si ya hemos escogido dicha posición
                    while (!alreadyChosen && j < numbers.Length)
                    {
                        if (index == numbers[j])
                        {
                            alreadyChosen = true;
                            Debug.Log("detecta que es el mismo");
                        }
                        else j++;
                    }
                }
                
            } while (alreadyChosen);

            // añadimos el índice al array de posiciones ya escogidas
            numbers[i] = index;

            // colocamos la flor en la posición marcada por la variable index
            _singleFlower = Instantiate(FlowerCodePrefab, Positions[index].position, Positions[index].rotation);
            Debug.Log("crea el floripondio");

            // declaramos el tipo de la flor
            _flowers[i] = _singleFlower.GetComponent<FlowerTypes>();
            _flowers[i].DefineType(index);
        }
        if (Padlock != null)
        {
            Padlock.CreatePadlockCode();
        }
        Destroy(this);
    } // GenerateFlowers
    #endregion

} // class FlowerCodeSpawner 
// namespace
