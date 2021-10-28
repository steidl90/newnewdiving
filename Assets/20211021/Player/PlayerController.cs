using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float power;
    public float y;

    private Animator animator;
    private Rigidbody rigid;
    public GameObject model;
    public GameObject ragdoll;
    public GameObject effect;
    private Vector3 force;

    private bool isReplay;
    private bool isCollision = true;
    private bool endcheak = false;
    private bool initRag = true;
    private bool isDiving = false;
    [HideInInspector]
    public int houseKey;
    [HideInInspector]
    public float beforeY;
    [HideInInspector]
    public float isStopTime;
    public float ragdollPower;
    private NewReplay newReplay;
    public bool IsReplay { get => isReplay; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        model = GameObject.FindGameObjectWithTag("PlayerModel");
        rigid = model.GetComponent<Rigidbody>();
        ragdoll = GetComponent<CreatRagdoll>().originalRagdoll.GetComponent<Ragdoll>().ragdoll.gameObject;
        newReplay = GetComponent<NewReplay>();
    }
    
    private void FixedUpdate()
    {
        if (isDiving && !isReplay)
        {
            var playerData = new ReplayData();
            if (model.activeSelf)
            {
                playerData.target = model;
            }
            else
            {
                playerData.target = ragdoll;
            }
            playerData.types = ReplayData.Types.posAndRot;
            playerData.position = playerData.target.transform.position;
            playerData.rotation = playerData.target.transform.rotation;
            newReplay.Record(playerData);
        }

        if (endcheak)
        {
            CheakReplay();

            if (isReplay)
            {
                ReplaySetting();
                effect.GetComponent<EnterWaterEffect>().isSplash = true;
                GetComponent<NewReplay>().OnReplay();
            }
        }
    }
    public void Fly(Vector3 direction)
    {
        // 로컬로케이션을 곱해줌으로 해당 오브젝트의 방향이 바뀌더라도 항상 앞으로 다이빙 하게 한다
        var dir = transform.localRotation * new Vector3(direction.x, y, direction.y);
        force = dir * power;
        animator.SetTrigger("Diving");
        rigid.AddForce(force, ForceMode.Impulse);
        rigid.useGravity = true;
        isDiving = true;
    }
    
    public void OnTrigger(Collider other, Vector3 pos)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player") &&
            other.gameObject.layer != LayerMask.NameToLayer("DivingBoard"))
        {
            endcheak = true;
            isStopTime = Time.time;
            var rag = GetComponent<CreatRagdoll>();
            if (other.gameObject.layer == LayerMask.NameToLayer("Floor") &&
                isCollision)
            {
                rag.CreateRagdoll(force * 0f, pos);
                model.SetActive(false);
                ReplayActive();

                isCollision = false;
            }
            else if (other.gameObject.layer != LayerMask.NameToLayer("Water") &&
                     isCollision)
            {
                rag.CreateRagdoll(force * ragdollPower, pos);
                model.SetActive(false);
                ReplayActive();

                isCollision = false;
            }
        }
    }
    public void ReplaySetting()
    {
        var count = GetComponent<NewReplay>().data.Count;
        if (initRag)
        {
            initRag = false;
            Time.timeScale = 1.5f;
            ModelOnRagdollOff();
            animator.SetTrigger("IsReplay");
            isCollision = true;
            GameManager.gameManager.UI.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.gameManager.cameraManager.GetComponent<CameraManager>().OnReplay();

            InitHouse();

            model.GetComponent<Rigidbody>().useGravity = false;
            ragdoll = GetComponent<CreatRagdoll>().replayRagdoll.GetComponent<Ragdoll>().ragdoll.gameObject;
            var rag = GetComponent<CreatRagdoll>().replayRagdoll;
            
            var joint = rag.GetComponentsInChildren<CharacterJoint>();
            foreach (var elem in joint)
            {
                elem.enableProjection = true;
            }
        }

        if (count == 0)
        {
            GameManager.gameManager.OnReStartUI();
            endcheak = false;
            isReplay = false;
            isCollision = false;
            
            Time.timeScale = 1f;

            var rag = GetComponent<CreatRagdoll>().replayRagdoll;
            var joint = rag.GetComponentsInChildren<Rigidbody>();
            foreach (var elem in joint)
            {
                elem.isKinematic = true;
            }
        }
    }

    private void CheakReplay()
    {
        var currentY = 0f;
        if(model.activeSelf)
        {
            currentY = model.transform.position.y;
        }
        else
        {
            currentY = ragdoll.transform.position.y;
        }

        if (beforeY + 0.1f < currentY ||
            beforeY - 0.1f > currentY)
        {
            isStopTime = Time.time;
        }
        else if(isStopTime + 1.5f < Time.time)
        {
            
            isReplay = true;
        }
        beforeY = currentY;
    }
    private void ReplayActive()
    {
        if (!isReplay)
        {
            ReplayData replay = new ReplayData();
            replay.types = ReplayData.Types.deActive;
            replay.target = model;
            newReplay.Record(replay);

            ReplayData replay1 = new ReplayData();
            replay1.types = ReplayData.Types.active;
            replay1.target = ragdoll;
            newReplay.Record(replay1); 
        }
    }

    public void ModelOnRagdollOff()
    {
        model.SetActive(true);
        GetComponent<CreatRagdoll>().OffRagdoll();
    }
    public void RagdollOnModelOff()
    {
        model.SetActive(false);
        GetComponent<CreatRagdoll>().OnRagdoll();
    }

    public void InitHouse()
    {
        var house = GameManager.gameManager.destoryHouse.GetComponent<DestroyHouse>();
        house.HouseDestroy(houseKey);
        house.HouseInit(houseKey);

    }
}
