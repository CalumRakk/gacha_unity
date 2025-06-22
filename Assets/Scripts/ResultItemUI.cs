using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultItemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText;

    public void Setup(ItemData item)
    {
        iconImage.sprite = item.sprite;
        nameText.text = item.nombre;
        rarityText.text = item.rareza.ToString();

        switch (item.rareza)
        {
            case Rarity.Comun:
                rarityText.color = Color.gray;
                break;
            case Rarity.Raro:
                rarityText.color = new Color(0.1f, 0.5f, 1f); // Un azul más bonito
                break;
            case Rarity.Epico:
                rarityText.color = new Color(0.8f, 0.4f, 1f); // Un morado/violeta
                break;
        }
    }
}