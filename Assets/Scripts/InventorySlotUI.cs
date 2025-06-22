using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private GameObject countContainer;
    [SerializeField] private TextMeshProUGUI countText;


    public void Setup(ItemData item, int count)
    {
        // Asignar los datos básicos
        iconImage.sprite = item.sprite;
        nameText.text = item.nombre;
        rarityText.text = item.rareza.ToString();

        // Cambiar color de la rareza
        switch (item.rareza)
        {
            case Rarity.Comun:
                rarityText.color = Color.gray;
                break;
            case Rarity.Raro:
                rarityText.color = new Color(0.1f, 0.5f, 1f); // Azul
                break;
            case Rarity.Epico:
                rarityText.color = new Color(0.8f, 0.4f, 1f); // Violeta
                break;
        }

        // Gestionar el contador
        if (count > 1)
        {
            countContainer.SetActive(true);
            countText.text = $"x{count}";
        }
        else
        {
            countContainer.SetActive(false);
        }
    }
}