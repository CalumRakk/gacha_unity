using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GachaManager : MonoBehaviour
{
    [Header("Configuración del Gacha")]
    [SerializeField] private List<ItemData> todosLosItems;
    private Dictionary<Rarity, float> probabilidadesRareza;

    [Header("Datos del Jugador")]
    [SerializeField] private int gemas = 200;
    [SerializeField] private int pityContador = 0;
    private int pityMaximo = 9;
    private Dictionary<ItemData, int> inventario;

    [Header("Referencias de UI Principal")]
    [SerializeField] private TextMeshProUGUI gemasText;
    [SerializeField] private TextMeshProUGUI pityText;
    [SerializeField] private Button invocarX1Button;
    [SerializeField] private Button invocarX10Button;

    [Header("UI - Resultados")]
    [SerializeField] private GameObject resultItemPrefab;
    [SerializeField] private Transform resultsGridContainer;

    [Header("UI - Inventario")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Transform slotsGridContainer;

    void Awake()
    {
        probabilidadesRareza = new Dictionary<Rarity, float>
        {
            { Rarity.Comun, 0.89f },
            { Rarity.Raro, 0.10f },
            { Rarity.Epico, 0.01f }
        };
        inventario = new Dictionary<ItemData, int>();
    }

    void Start()
    {
        inventoryPanel.SetActive(false);
        UpdateUI();
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        UpdateInventoryUI();
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }

    private void UpdateInventoryUI()
    {
        foreach (Transform child in slotsGridContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<ItemData, int> itemEntry in inventario)
        {
            GameObject slotGO = Instantiate(inventorySlotPrefab, slotsGridContainer);
            slotGO.GetComponent<InventorySlotUI>().Setup(itemEntry.Key, itemEntry.Value);
        }
    }

    public void OnClick_InvocarX1()
    {
        int costo = 10;
        if (gemas >= costo)
        {
            gemas -= costo;
            List<ItemData> resultados = new List<ItemData>();
            resultados.Add(RealizarInvocacion());
            DisplayResults(resultados);
            UpdateUI();
        }
    }

    public void OnClick_InvocarX10()
    {
        int costo = 100;
        if (gemas >= costo)
        {
            gemas -= costo;
            List<ItemData> resultados = new List<ItemData>();
            for (int i = 0; i < 10; i++)
            {
                resultados.Add(RealizarInvocacion());
            }
            DisplayResults(resultados);
            UpdateUI();
        }
    }

    private void DisplayResults(List<ItemData> items)
    {
        foreach (Transform child in resultsGridContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in items)
        {
            if (item != null)
            {
                GameObject itemGO = Instantiate(resultItemPrefab, resultsGridContainer);
                itemGO.GetComponent<ResultItemUI>().Setup(item);
            }
        }
    }

    private ItemData RealizarInvocacion()
    {
        bool pityActivo = pityContador >= pityMaximo;
        if (pityActivo) { Debug.LogWarning("¡PITY ACTIVADO! Invocación garantizada."); }
        Rarity rarezaSeleccionada = SeleccionarRareza(pityActivo);
        List<ItemData> itemsDisponibles = todosLosItems.Where(item => item.rareza == rarezaSeleccionada).ToList();
        if (itemsDisponibles.Count == 0) { return null; }
        ItemData itemObtenido = itemsDisponibles[Random.Range(0, itemsDisponibles.Count)];
        AñadirItemAlInventario(itemObtenido);
        if (itemObtenido.rareza == Rarity.Raro || itemObtenido.rareza == Rarity.Epico) { pityContador = 0; }
        else { pityContador++; }
        return itemObtenido;
    }

    private void UpdateUI()
    {
        gemasText.text = $"Gemas: {gemas}";
        pityText.text = $"Pity: {pityContador}/{pityMaximo + 1}";
        invocarX1Button.interactable = gemas >= 10;
        invocarX10Button.interactable = gemas >= 100;
    }

    private void AñadirItemAlInventario(ItemData item)
    {
        if (inventario.ContainsKey(item)) { inventario[item]++; }
        else { inventario.Add(item, 1); }
    }

    private Rarity SeleccionarRareza(bool forzarPity)
    {
        if (forzarPity) { return (Random.value < 0.9f) ? Rarity.Raro : Rarity.Epico; }
        float randomValue = Random.value;
        if (randomValue < 0.89f) return Rarity.Comun;
        if (randomValue < 0.99f) return Rarity.Raro;
        return Rarity.Epico;
    }
}