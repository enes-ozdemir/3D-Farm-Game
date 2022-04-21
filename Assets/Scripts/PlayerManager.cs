using inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public EquippableItem currentItem;
    [SerializeField] private QuickSlotPanel quickSlotPanel;
    [SerializeField] private GameObject handR;
    [SerializeField] private GameObject handL;
    [SerializeField] private Animator anim;
    private EquippableItem _oldItem;
    public GameObject _currentGameObject;
    [SerializeField] public ParticleSystem currentParticle;
    public bool isPlayerNotBlocked = true;
    [SerializeField] private PlayerAnimationManager playerAnimationManager;


    [Space] [Header("Interact")] [SerializeField]
    private UIManager UIManager;

    [SerializeField] private GameObject interactText;

    [SerializeField] private GameObject shopObject;
    //[SerializeField] [Range(0f,10f)] private float range;

    private void Awake()
    {
        anim.GetComponent<Animator>();
    }

    void Update()
    {
        CheckRange();
    }

    private void CheckRange()
    {
        float range = 12f;

        if (Vector3.Distance(this.transform.position, shopObject.transform.position) < range)
        {
            if (!UIManager._isShopActive) interactText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                UIManager.DisplayShop();
                interactText.SetActive(false);
            }
        }
        else
        {
            interactText.SetActive(false);
        }
    }

    private void CheckInteract()
    {
        if (currentItem == null) return;


        if (Input.GetKey(KeyCode.Space))
        {
            currentItem.Interact(anim);
        }
    }

    public PlayerAnimationManager.AnimState CheckIfInteract()
    {
        if (currentItem == null) return PlayerAnimationManager.AnimState.Player_Idle;

        if (Input.GetKey(KeyCode.Space))
        {
            //quickSlotPanel.RemoveItem(currentItem);
            return currentItem.animName;
        }

        return PlayerAnimationManager.AnimState.Player_Idle;
    }


    public void SetItemToHand()
    {
        if (quickSlotPanel.currentItem is EquippableItem)
        {
            if (currentItem == quickSlotPanel.currentItem) return;
            Debug.Log("Init Item at hand");
            currentItem = (EquippableItem) quickSlotPanel.currentItem;
            currentParticle = currentItem.particleSystem;


            if (_oldItem != currentItem)
            {
                RemoveOldItemFromHand(_oldItem);
            }

            //PlayHoldAnimation();
            var currentGameObject = SetEquippableItemTransform();
            _currentGameObject = Instantiate(currentGameObject, handR.transform);
            if (currentParticle != null)
            {
                var particlePoint = GameObject.Find("ParticlePoint");
                Debug.Log("particlePoint search");
                if (particlePoint != null)
                {
                    Instantiate(currentParticle, particlePoint.transform);
                    Debug.Log("particlePoint init");
                }
            }

            _oldItem = currentItem;
        }
    }

    private GameObject SetEquippableItemTransform()
    {
        _currentGameObject = currentItem.itemPrefab.gameObject;
        _currentGameObject.transform.position = currentItem.coordinates;
        _currentGameObject.transform.rotation = currentItem.rotation.normalized;

        return _currentGameObject;
    }

    private void RemoveOldItemFromHand(EquippableItem equippableItem)
    {
        if (_currentGameObject == null) return;
        //StopHoldAnimation();
        //var obje = GetComponent<GameObject>()
        Destroy(_currentGameObject);
        //DestroyImmediate(equippableItem.itemPrefab.gameObject,true);
        Debug.Log("Destroy(currentItem)");
    }
}