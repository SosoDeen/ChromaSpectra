using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    [Header("Pickup/Inventory")]
    public float pickupCooldown = 3f; // Cooldown between pickups
    public float pickupRange = .25f; // Range of pickup
    public KeyCode pickupKey = KeyCode.E; // Pickup key
    public KeyCode inventoryKey = KeyCode.Tab; // Inventory key
    public bool noSpamPickup = false;

    private string itemUsed = "";

    public GameManager manager;
    public UIManager uIManager;
    public SoundManager soundManager;

    // Interface used to call interaction code from interactable objects
    public interface Interact
    {
        void interaction();
    }

    // Interface used to call code from objects using Items
    public interface ItemUser
    {
        void useItem();
        void interaction();
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        uIManager = GameObject.FindWithTag("UI").GetComponent<UIManager>();
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pickupKey) && !GameManager.cameraMoving && !GameManager.inCinematic && !noSpamPickup && !GameManager.isInPlayMode)
        {
            checkPickup();
        }

        if (Input.GetKeyDown(inventoryKey) && !GameManager.cameraMoving && !GameManager.inCinematic && !GameManager.isInPlayMode)
        {
            toggleInventory();
        }
    }

    // checks if objects can be picked up
    private void checkPickup()
    {
        noSpamPickup = true;
        StartCoroutine(pickupDelay());

        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.gameObject.CompareTag("Item"))
            {
                Debug.Log("Item pickup");
                GameManager.inCinematic = true;
                // string name = collider.gameObject.name;
                // Sprite sprite = collider.gameObject.GetComponent<SpriteRenderer>().sprite;

                // finds item from dictionary and sets its sprite and sound
                int itemIndex = manager.allInteractables.IndexOf(collider.gameObject);
                GameObject item = manager.allInteractables[itemIndex];
                Sprite sprite = item.GetComponent<SpriteRenderer>().sprite;

                // adds item to inventory
                manager.pickupItems.Add(item.name);
                manager.pickupSprite.Add(sprite);

                // plays sound of item
                soundManager.playSFX(itemIndex);

                Destroy(collider.gameObject);
                uIManager.addItem(manager.pickupSprite[manager.pickupSprite.Count - 1]);
                break;
            }
            else if (collider.gameObject.CompareTag("Interactable"))
            {
                Debug.Log("Interactable pickup");
                // finds item from dictionary and sets its sound index
                int itemIndex = manager.allInteractables.IndexOf(collider.gameObject);

                // plays sound of item
                soundManager.playSFX(itemIndex);
                
                collider.gameObject.GetComponent<Interact>().interaction();
                noSpamPickup = false;
                break;
            }
            else if (collider.gameObject.CompareTag("ItemUser"))
            {
                Debug.Log("ItemUser pickup");
                collider.gameObject.GetComponent<ItemUser>().interaction();
                noSpamPickup = false;
                break;
            }
        }
    }

    public void toggleInventory()
    {
        GameManager.inventoryOpen = !GameManager.inventoryOpen;
        uIManager.inventoryToggle(GameManager.inventoryOpen);
    }

    public void useItem(int itemNumber)
    {
        toggleInventory();

        if (itemNumber < manager.pickupItems.Count)
        {
            itemUsed = manager.pickupItems[itemNumber];
            Debug.Log("using " + itemUsed);

            Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("ItemUser"))
                {
                    GameObject item = collider.gameObject;

                    if (item.name == itemUsed + "Used")
                    {
                        collider.gameObject.GetComponent<ItemUser>().useItem();

                        // plays sound of item
                        int itemIndex = manager.allInteractables.IndexOf(collider.gameObject);
                        soundManager.playSFX(itemIndex);

                        manager.pickupItems.RemoveAt(itemNumber);
                        manager.pickupSprite.RemoveAt(itemNumber);
                        uIManager.removeItem(itemNumber);
                    }
                }
            }
        }
    }

    private IEnumerator pickupDelay()
    {
        yield return new WaitForSeconds(pickupCooldown);
        noSpamPickup = false;
    }
}
