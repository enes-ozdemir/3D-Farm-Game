using UnityEngine;
using static PlayerAnimationManager;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationspeed = 5f;
    public Animator anim;
    private FarmState farmState;
    public string animName;
    [SerializeField] private PlayerManager PlayerManager;
    [SerializeField] public PlayerAnimationManager playerAnimationManager;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (PlayerManager.isPlayerNotBlocked)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        float yDirection = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(xDirection, 0, yDirection);
        moveDirection.Normalize();

        float cameraFacing = mainCamera.transform.eulerAngles.y;
        Vector3 inputVector = new Vector3(xDirection, 0, yDirection);
        Vector3 turnedInputVector = Quaternion.Euler(0, cameraFacing, 0) * inputVector;

        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        transform.position += turnedInputVector * moveSpeed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(turnedInputVector, Vector3.up);

            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);
        }
    }

    public bool PlayerRun()
    {
        if (!PlayerManager.isPlayerNotBlocked) return false;
        if ((Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10f;
            return true;
        }

        moveSpeed = 5f;
        return false;
    }

    public bool SetMovementAnimation()
    {
        if (!PlayerManager.isPlayerNotBlocked) return false;

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            return true;
        }

        return false;
    }

    private bool checkIfAnimFinished(string animName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            return true;
        }
        else return false;
    }


    /*  private void OnCollisionStay(Collision collision)
      {
          if (collision.gameObject.tag == "Farm")
          {
              if (Input.GetKey(KeyCode.E))
              {
                  farmState = collision.gameObject.GetComponent<FarmState>();
                  Debug.Log(farmState.farmStatus);
                  if (farmState.UpdateFarmStatus(playerTool.Name))
                  {
                      anim.SetBool(playerTool.AnimName, true);
                      Debug.Log(playerTool.AnimName);
                  }
              }
          }
  
          if (checkIfAnimFinished(playerTool.AnimName))
          {
              anim.SetBool(playerTool.AnimName, false);
          }
  
  
          /*
           switch (playerTool)
           {
  
               case "Kazma":
                   if(farmState.UpdateFarmStatus(playerTool))
                   {
                       anim.SetBool("isDigging", true);
                   }
                   break;
               case "Tohum":
                   if (farmState.UpdateFarmStatus(playerTool))
                   {
                       anim.SetBool("isPlanting", true);
                   }
                   break;
               case "Kazma":
                   if (farmState.UpdateFarmStatus("Kazma"))
                   {
                       anim.SetBool("isDigging", true);
                   }
                   break;
               case "Kazma":
                   if (farmState.UpdateFarmStatus("Kazma"))
                   {
                       anim.SetBool("isDigging", true);
                   }
                   break;
           } 
      }*/
}