//---------------------------------------------------------
// Script que hace que las flores aparezcan en posiciones aleatorias
// Diego Martín
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using

/// <summary>
/// Script que maneja la generación aleatoria de flores y del código de la puerta final, cada vez que el jugador
/// pasa por el trigger, se genera una nueva serie de flores en otro orden y en posiciones aleatorias.
/// </summary>
public class FlowerCodeSpawner : MonoBehaviour
{

    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    // posiciones de las flores
    [SerializeField]
    private Transform[] Positions;

    [SerializeField]
    private GameObject FlowerCodeOfScene;

    [SerializeField]
    private Padlock Padlock;

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // cuando el jugador pasa por el trigger, se crean todas las flores
        _player = collision.GetComponent<PlayerMovement>();
        if (_player != null)
        {
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
                        }
                        else j++;
                    }
                }
                
            } while (alreadyChosen);

            // añadimos el índice al array de posiciones ya escogidas
            numbers[i] = index;

            // colocamos la flor en la posición marcada por la variable index
            _singleFlower = Instantiate(FlowerCodeOfScene, Positions[index].position, Positions[index].rotation);

            // declaramos el tipo de la flor
            _flowers[i] = _singleFlower.GetComponent<FlowerTypes>();
            _flowers[i].DefineType(index);
        }
        if (Padlock != null)
        {
            Padlock.CreatePadlockCode();
        }
        this.enabled = false;
    } // GenerateFlowers
    #endregion

} // class FlowerCodeSpawner 
// namespace
