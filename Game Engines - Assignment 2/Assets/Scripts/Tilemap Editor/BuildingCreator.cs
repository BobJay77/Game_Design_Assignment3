using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildingCreator : Singleton<BuildingCreator>
{
    [SerializeField] Tilemap previewMap, defaultMap;
    PlayerInput playerInput;

    TileBase tileBase;
    BuildingObjectBase selectedObj;

    public Camera _camera;

    Vector2 mousePos;
    Vector3Int currGridPos;
    Vector3Int prevGridPos;

    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        _camera = Camera.main;

    }
    
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Gameplay.MousePosition.performed += OnMouseMove;
        playerInput.Gameplay.MouseLeftClick.performed += OnLeftClick;
        playerInput.Gameplay.MouseRightClick.performed += OnRightClick;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Gameplay.MousePosition.performed -= OnMouseMove;
        playerInput.Gameplay.MouseLeftClick.performed -= OnLeftClick;
        playerInput.Gameplay.MouseRightClick.performed -= OnRightClick;
    }

    private BuildingObjectBase SelectedObj
    {
        set
        {
            selectedObj = value;

            tileBase = selectedObj != null ? selectedObj.TileBase : null;

            UpdatePreview();
        }
    }

    private void Update()
    {
        if (selectedObj != null)
        {
            Vector3 pos = _camera.ScreenToWorldPoint(mousePos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);

            if (gridPos != currGridPos)
            {
                prevGridPos = currGridPos;
                currGridPos = gridPos;

                UpdatePreview();
            }
        }
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    private void OnLeftClick(InputAction.CallbackContext context)
    {
        if (selectedObj != null && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleDrawing();
        }
    }

    private void OnRightClick(InputAction.CallbackContext context)
    {
        SelectedObj = null;
    }

    public void ObjectSelected(BuildingObjectBase objectBase)
    {
        SelectedObj = objectBase;
    }

    private void UpdatePreview()
    {
        previewMap.SetTile(prevGridPos, null);
        previewMap.SetTile(currGridPos, tileBase);
    }

    private void HandleDrawing()
    {
        DrawItem();
    }

    private void DrawItem()
    {
        defaultMap.SetTile(currGridPos, tileBase);
    }
}
