using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillBridge.Message;
using Entities;
using Services;
using UnityEngine.AI;

public class PlayerInputController : MonoBehaviour
{
    public Rigidbody rb;
    public float rotateSpeed = 2.0f;
    public float turnAngle = 10;
    public int speed;
    public EntityController entityController;
    public bool onAir = false;

    public Character character;
    CharacterState state;

    private NavMeshAgent agent;
    private bool autoNav = false;

    // Start is called before the first frame update
    void Start()
    {
        state = CharacterState.Idle;
        if(character == null)
        {
            DataManager.Instance.Load();
            NCharacterInfo cinfo = new NCharacterInfo();
            cinfo.Id = 1;
            cinfo.Name = "Test";
            cinfo.ConfigId = 1;
            cinfo.Entity = new NEntity();
            cinfo.Entity.Position = new NVector3();
            cinfo.Entity.Direction = new NVector3();
            cinfo.Entity.Direction.X = 0;
            cinfo.Entity.Direction.Y = 100;
            cinfo.Entity.Direction.Z = 0;
            character = new Character(cinfo);
            if (entityController != null)
                entityController.entity = character;
        }
        if(agent == null)
        {
            agent = this.gameObject.AddComponent<NavMeshAgent>();
            agent.stoppingDistance = 0.3f;
        }
    }

    public void StartNav(Vector3 target)
    {
        StartCoroutine(BeginNav(target));
    }

    IEnumerator BeginNav(Vector3 target)
    {
        agent.SetDestination(target);
        yield return null;
        autoNav = true;
        if(state != SkillBridge.Message.CharacterState.Move)
        {
            state = SkillBridge.Message.CharacterState.Move;
            this.character.MoveForward();
            this.SendEntityEvent(EntityEvent.MoveFwd);
            agent.speed = this.character.speed / 100f;
        }
    }

    public void StopNav()
    {
        autoNav = false;
        agent.ResetPath();
        if (state != SkillBridge.Message.CharacterState.Idle)
        {
            state = SkillBridge.Message.CharacterState.Idle;
            this.character.Stop();
            this.SendEntityEvent(EntityEvent.Idle);
        }
        NavPathRenderer.Instance.SetPath(null,Vector3.zero);
    }

    public void NavMove()
    {
        if (agent.pathPending) return;
        if(agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            StopNav();
            return;
        }
        if (agent.pathStatus != NavMeshPathStatus.PathComplete) return;

        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1)
        {
            StopNav();
            return;
        }
        NavPathRenderer.Instance.SetPath(agent.path, agent.destination);

        if(agent.isStopped || agent.remainingDistance < 2.0f)
        {
            StopNav();
            return;
        }
    }

    void FixedUpdate()
    {
        if (UserService.Instance.isGaming)
        {
            Move();
        }
    }

    void Move()
    {
        if (character == null) return;

        if (autoNav)
        {
            NavMove();
            return;
        }

        if (InputManager.Instance != null && InputManager.Instance.IsInputMode) return;

        float v = Input.GetAxis("Vertical");
        if (v > 0.01)
        {
            if (state != CharacterState.Move)
            {
                state = CharacterState.Move;
                character.MoveForward();
                SendEntityEvent(EntityEvent.MoveFwd);
            }
            rb.velocity = rb.velocity.y * Vector3.up + GameObjectTool.LogicToWorld(character.direction) * (character.speed + 9.8f) / 100f;
        }
        else if (v < -0.01)
        {
            if (state != CharacterState.Move)
            {
                state = CharacterState.Move;
                character.MoveBack();
                SendEntityEvent(EntityEvent.MoveBack);
            }
            rb.velocity = rb.velocity.y * Vector3.up + GameObjectTool.LogicToWorld(character.direction) * (character.speed + 9.8f) / 100f;
        }
        else
        {
            if (state != CharacterState.Idle)
            {
                state = CharacterState.Idle;
                rb.velocity = Vector3.zero;
                character.Stop();
                SendEntityEvent(EntityEvent.Idle);
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            SendEntityEvent(EntityEvent.Jump);
        }

        float h = Input.GetAxis("Horizontal");
        if (h < -0.1 || h > 0.1)
        {
            this.transform.Rotate(0, h * rotateSpeed, 0);
            Vector3 dir = GameObjectTool.LogicToWorld(character.direction);
            Quaternion rot = new Quaternion();
            rot.SetFromToRotation(dir, this.transform.forward);

            if (rot.eulerAngles.y > turnAngle && rot.eulerAngles.y < (360 - turnAngle))
            {
                character.SetDirection(GameObjectTool.WorldToLogic(transform.forward));
                rb.transform.forward = transform.forward;
                SendEntityEvent(EntityEvent.None);
            }
        }
    }
    Vector3 lastPos;
    float lastSync = 0;
    void LateUpdate()
    {
        Vector3 offset = rb.transform.position - lastPos;
        speed = (int)(offset.magnitude * 100f / Time.deltaTime);

        lastPos = this.rb.transform.position; 

        if((GameObjectTool.WorldToLogic(rb.transform.position)-character.position).magnitude > 50)
        {
            character.SetPosition(GameObjectTool.WorldToLogic(rb.transform.position));
            SendEntityEvent(EntityEvent.None);
        }
        transform.position = rb.transform.position;

        Vector3 dir = GameObjectTool.LogicToWorld(character.direction);
        Quaternion rot = new Quaternion();
        rot.SetFromToRotation(dir, this.transform.forward);

        if(rot.eulerAngles.y > this.turnAngle && rot.eulerAngles.y < (360 - this.turnAngle))
        {
            character.SetDirection(GameObjectTool.WorldToLogic(transform.forward));
            this.SendEntityEvent(EntityEvent.None);
        }
    }

    public void SendEntityEvent(EntityEvent entityEvent,int param = 0)
    {
        if (entityController != null)
            entityController.OnEntityEvent(entityEvent, param);
        MapService.Instance.SendMapEntitySync(entityEvent, this.character.EntityData, param);
    }
}
