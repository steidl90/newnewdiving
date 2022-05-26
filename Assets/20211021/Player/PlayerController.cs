using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float power;
    public float y;
    private float timer = 0f;

    private Animator animator;
    private Rigidbody rigid;
    public GameObject model;
    public GameObject ragdoll;
    public GameObject[] effect;
    private Vector3 force;

    private bool isReplay;
    private bool isCollision = true;
    private bool endcheak = false;
    private bool initRag = true;
    private bool isDiving = false;
    [HideInInspector]
    public bool isSuccess = false;

    [HideInInspector]
    public int houseKey;
    [HideInInspector]
    public float beforeY;
    [HideInInspector]
    public float isStopTime;
    public float ragdollMagnitude;
    private PlayerReplay newReplay;
    public bool IsDiving { get => isDiving; }
    public bool IsReplay { get => isReplay; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        model = GameObject.FindGameObjectWithTag("PlayerModel");
        rigid = model.GetComponent<Rigidbody>();
        ragdoll = GetComponent<RagdollManager>().originalRagdoll.ragdoll.gameObject;
        newReplay = GetComponent<PlayerReplay>();
    }

    private void Update()
    {
        if (model.activeSelf && rigid != null)
        {
            ragdollMagnitude = rigid.velocity.magnitude;
            if (ragdollMagnitude > 25f)
                ragdollMagnitude = 25f;
        }
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
                effect[(int)StaticVariable.sector].GetComponent<EnterWaterEffect>().isSplash = true;
                GetComponent<PlayerReplay>().OnReplay();
            }
        }
    }

    public void Diving(Vector3 direction)
    {
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
            var rag = GetComponent<RagdollManager>();
            if (other.gameObject.layer == LayerMask.NameToLayer("Water") &&
                isCollision)
            {
                if (!isReplay)
                {
                    isSuccess = true;
                    model.SetActive(false);
                }
                ReplayActive();
                isCollision = false;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Floor") &&
                isCollision)
            {
                rag.ActiveRagdoll(force * ragdollMagnitude, pos);
                model.SetActive(false);
                ReplayActive();
                isCollision = false;
            }

            else if (other.gameObject.layer != LayerMask.NameToLayer("Water") &&
                     isCollision)
            {
                rag.ActiveRagdoll(force * ragdollMagnitude, pos);
                model.SetActive(false);
                ReplayActive();
                isCollision = false;
            }
        }
    }
    public void ReplaySetting()
    {
        var count = GetComponent<PlayerReplay>().data.Count;
        if (initRag)
        {
            initRag = false;
            Time.timeScale = 1.5f;
            ModelOnRagdollOff();
            animator.SetTrigger("IsReplay");
            isCollision = true;
            GameManager.gameManager.uiManager.GetComponent<UIManager>().OnReplayUI();
            GameManager.gameManager.cameraManager.GetComponent<CameraManager>().OnReplay();
            isDiving = false;
            InitHouse();

            model.GetComponent<Rigidbody>().useGravity = false;
            var rag = GetComponent<RagdollManager>().replayRagdoll;
            ragdoll = rag.ragdoll.gameObject;
            
            var joint = rag.GetComponentsInChildren<CharacterJoint>();
            foreach (var elem in joint)
            {
                elem.enableProjection = true;
            }
        }

        if (count == 0)
        {
            var ui = GameManager.gameManager.uiManager.GetComponent<UIManager>();
            if(isSuccess)
            {
                ui.OffReplayUI();
                //ui.OnEndingUI();
                effect[(int)StaticVariable.sector].GetComponent<EnterWaterEffect>().splashFlag = 0f;
                effect[(int)StaticVariable.sector].GetComponent<EnterWaterEffect>().BigSplash.SetActive(false);
                effect[(int)StaticVariable.sector].GetComponent<EnterWaterEffect>().StopAllCoroutines();

                var sub = GameManager.gameManager.cameraManager.GetComponent<CameraManager>().sub;
                sub.GetComponent<FollowerTarget>().isFinish = true;
            }
            else
            {
                ui.OnReStartUI();
            }

            endcheak = false;
            isReplay = false;
            isCollision = false;
            
            Time.timeScale = 1f;

            var rag = GetComponent<RagdollManager>().replayRagdoll;
            var joint = rag.GetComponentsInChildren<Rigidbody>();
            foreach (var elem in joint)
            {
                elem.isKinematic = true;
            }
        }
    }

    private void CheakReplay()
    {
        float currentY;
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
        GetComponent<RagdollManager>().OffRagdoll();
    }
    public void RagdollOnModelOff()
    {
        model.SetActive(false);
        GetComponent<RagdollManager>().OnRagdoll();
    }

    public void InitHouse()
    {
        var house = GameManager.gameManager.destoryHouse.GetComponent<DestroyHouse>();
        house.HouseDestroy(houseKey);
        house.HouseInit(houseKey);
    }

    public IEnumerator CoVictory()
    {
        var pos = new Vector3(model.transform.position.x, model.transform.position.y + 1.5f, model.transform.position.z);
        while (timer < 2f)
        {
            timer += Time.deltaTime * 5f;
            model.transform.position = Vector3.Lerp(model.transform.position, pos, timer);
            yield return null;
        }
        var ui = GameManager.gameManager.uiManager.GetComponent<UIManager>();
        ui.OnEndingUI();
    }
}
