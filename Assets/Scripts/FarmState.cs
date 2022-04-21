using UnityEngine;

public class FarmState : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material[] material;

    public FarmStatus farmStatus = FarmStatus.Empty;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public FarmStatus CheckFarmStatus()
    {
        return farmStatus;
    }

    public bool UpdateFarmStatus(string tool)
    {
        Debug.Log("Player holding a " + tool);
        switch (farmStatus)
        {
            case FarmStatus.Empty:
                Debug.Log("Farm is empty");
                if (tool.Equals("Kazma"))
                {
                    Debug.Log("Player used a " + tool);
                    farmStatus = FarmStatus.ToBePlanted;
                    meshRenderer.material = material[2];
                    return true;
                }
                break;

            case FarmStatus.ToBePlanted:
                Debug.Log("Farm is ToBePlanted");
                if (tool.Equals("Tohum"))
                {
                    Debug.Log("Player used a " + tool);
                    meshRenderer.material = material[1];
                    farmStatus = FarmStatus.Seeded;
                    return true;

                }
                else if (tool.Equals("Gübre"))
                {
                    //Product time total will get effected.
                    return true;

                }
                break;

            case FarmStatus.Seeded:
                if (tool.Equals("Su"))
                {
                    Debug.Log("Farm is Seeded");
                    Debug.Log("Player used a " + tool);
                    meshRenderer.material = material[2];
                    //Product total time will get effected.
                    //Reset time for plant rot

                    //This will be removed later
                    farmStatus = FarmStatus.ToBeCollected;
                    return true;
                }
                break;

            case FarmStatus.ToBeCollected:
                if (tool.Equals("Toplayýcý"))
                {
                    Debug.Log("Player used a " + tool + "Farm is Empty");
                    meshRenderer.material = material[0];
                    farmStatus = FarmStatus.Empty;
                    return true;
                }
                break;
        }
        return false;
    }

    public enum FarmStatus
    {
        Empty,
        ToBePlanted,
        Seeded,
        ToBeCollected,
        ToBeWatered,
    }
    public enum AnimationStatus
    {
        Empty,
        ToBePlanted,
        Seeded,
        ToBeCollected,
        ToBeWatered,
    }
}