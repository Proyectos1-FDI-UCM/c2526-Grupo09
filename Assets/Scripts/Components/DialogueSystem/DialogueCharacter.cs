using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue Character", menuName = "Scriptable Object/Dialogue Character")] //Crear personajes desde el editor de Unity

    public class DialogueCharacter : ScriptableObject //Hereda de scriptable Objects
    {
        //Para el inspector
        [Header("Character Info")] //Tener la info del personaje en el inspector
        [SerializeField] private string characterName;
        [SerializeField] private Sprite profilePhoto;

        public string Name => characterName;
        public Sprite ProfilePhoto => profilePhoto;
    }


}