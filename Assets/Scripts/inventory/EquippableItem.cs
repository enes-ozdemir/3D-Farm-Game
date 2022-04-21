using UnityEngine;

namespace inventory
{
    public enum EquipmentType
    {
        Shovel,
        Fertilizer,
        WateringCan,
        Sickle,
    }

    [CreateAssetMenu]
    public class EquippableItem : Item, IEquippable

    {
        [SerializeField] public PlayerAnimationManager.AnimState animName;
        [SerializeField] public string holdingAnim;
        [SerializeField] public EquipmentType equipmentType;
        [SerializeField] private int level;
        [SerializeField] public GameObject itemPrefab;
        [SerializeField] private bool isSpendable;
        [SerializeField] public ParticleSystem particleSystem;

        [SerializeField] public Vector3 coordinates = new Vector3(0, 0, 0);
        [SerializeField] public Quaternion rotation = Quaternion.identity;
        
        public void Interact(Animator animator)
        {
            Debug.Log("Interact");
        }

        public void InstantiateEquippableItem(Transform transform, Animator animator)
        {
            Instantiate(itemPrefab.gameObject, transform);
            if (!holdingAnim.Equals(""))
            {
                animator.SetBool(holdingAnim, true);
            }

            itemPrefab.gameObject.transform.position = coordinates;
            itemPrefab.gameObject.transform.rotation = rotation.normalized;
        }
    }

    public interface IEquippable
    {
        void Interact(Animator animator);

        void InstantiateEquippableItem(Transform transform, Animator animator);
    }
}