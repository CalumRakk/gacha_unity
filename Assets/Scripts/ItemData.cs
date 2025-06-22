using UnityEngine;

public enum Rarity
{
    Comun,
    Raro,
    Epico
}

// El atributo [CreateAssetMenu] nos permite crear instancias de este objeto
// directamente desde el menú "Assets > Create" de Unity.
[CreateAssetMenu(fileName = "NuevoItem", menuName = "Gacha/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Información del Ítem")]
    public string nombre;
    public Rarity rareza;
    public Sprite sprite;
}