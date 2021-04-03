using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class ItemPickUpComponent : MonoBehaviour
{
    [SerializeField] private ItemScriptable PickUpItem;

    [Tooltip("Manual Override for Drop Amount, if left at -1 it will use the amount from the scriptable object.")]
    [SerializeField] private int Amount = -1;

    [SerializeField] private MeshRenderer PropMeshRenderer;
    [SerializeField] private MeshFilter PropMeshFilter;

    private ItemScriptable ItemInstance;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate();
    }

    private void Instantiate()
    {
        ItemInstance = Instantiate(PickUpItem);

        if (Amount > 0)
        {
            ItemInstance.SetAmount(Amount);
        }

        ApplyMesh();
    }

    private void ApplyMesh()
    {
        if (PropMeshFilter) PropMeshFilter.mesh = PickUpItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        if (PropMeshRenderer) PropMeshRenderer.materials = PickUpItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log($"{PickUpItem.name} - Picked Up");

        ItemInstance.UseItem(other.GetComponent<PlayerController>());

        Destroy(gameObject);
    }

    private void OnValidate()
    {
        ApplyMesh();
    }
}
