//---------------------------------------------------------
// Script zona de escondite.
// Rafa Campos García.
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue Character", menuName = "Scriptable Object/Dialogue Character")] //Crear personajes desde el editor de Unity

    ///<summary>
    /// Clase que hereda de ScriptableObjects. 
    /// Recoge los datos para los nombres y las fotos del personaje en cuestión.
    ///</summary>
    public class DialogueCharacter : ScriptableObject 
    {
        //Para el inspector

        //Tener la info del personaje en el inspector
        [Header("Character Info")] 
        [SerializeField] private string characterName;
        [SerializeField] private Sprite profilePhoto;

        public string Name => characterName;
        public Sprite ProfilePhoto => profilePhoto;
    }


}