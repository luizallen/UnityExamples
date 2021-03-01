using UnityEngine;

public delegate void DelegateModel(object sender, object args);
public class InputController : MonoBehaviour
{

    float _hCooldown = 0;
    float _vCooldown = 0;
    float _cooldownTimer = 0.5f;
    public static InputController Instance;
    public DelegateModel OnMove;
    public DelegateModel OnFire;
    public DelegateModel OnMoveMouse;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        int h = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        int v = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

        Vector3Int moved = new Vector3Int(0, 0, 0);
        if (h != 0)
        {
            moved.x = GetMoved(ref _hCooldown, h);
        }
        else
            _hCooldown = 0;
        if (v != 0)
        {
            moved.y = GetMoved(ref _vCooldown, v);
        }
        else
            _vCooldown = 0;

        if (moved != Vector3Int.zero && OnMove != null)
        {
            OnMove(null, moved);
        }

        if (OnMoveMouse != null)
            OnMoveMouse(null, new Mouse { Button = 0 });

        if (Input.GetButtonDown("Fire1") && OnFire != null)
        {
            OnFire(null, new Mouse { Button = 1 });
        }
        if (Input.GetButtonDown("Fire2") && OnFire != null)
        {
            OnFire(null, new Mouse { Button = 2 });
        }
    }

    int GetMoved(ref float cooldownSum, int value)
    {
        if (Time.time > cooldownSum)
        {
            cooldownSum += Time.time + _cooldownTimer;
            return value;
        }
        return 0;
    }
}

public class Mouse
{
    public int Button;
    public Vector3Int Position
    {
        get
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var position = Floor.Instance.GetTileMapPosition(pos);

            Debug.Log(position);

            return position;
        }
    }
}