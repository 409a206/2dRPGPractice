using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CommandBar : MonoBehaviour
{
    private CommandButton [] commandButtons;

    public float buttonSize = 1.28f;
    public float buttonRows = 1;
    public float buttonColumns = 6;
    public float buttonRowSpacing = 0;
    public float buttonColumnSpacing = 0;

    public Sprite DefaultButtonImage;
    public Sprite SelectedButtonImage;

    private float ScreenHeight;
    private float ScreenWidth;

    public int Layer {
        get{return gameObject.layer;}
    }

    float Width {
        get {
            return (buttonSize * buttonColumns) + Mathf.Clamp((buttonColumnSpacing * (buttonColumns - 1)), 0, int.MaxValue);
        }
    }

    float Height {
        get {
            return (buttonSize * buttonRows) + Mathf.Clamp((buttonColumnSpacing * (buttonRows - 1)), 0 , int.MaxValue);
        }
    }

    public bool anchor = true;
    public Vector2 anchorOffset = Vector2.zero;
    public ScreenPositionAnchorPoint anchorPoint = 
        ScreenPositionAnchorPoint.BottomCenter;

    public InventoryItem debugItem;

    private bool canSelectButton = true;
    private CommandButton selectedButton;

    public bool CanSelectButton {
        get {
            return canSelectButton;
        }
    }

    private void Awake() {
        ScreenHeight = Camera.main.orthographicSize * 2;
        ScreenWidth = ScreenHeight * Screen.width / Screen.height;

        #if UNITY_EDITOR
            GameState.CurrentPlayer.AddInventoryItem(debugItem);
        #endif
    }

    void Start() {
        InitCommandButtons();
        MessagingManager.Instance.SubscribeUIEvent(SetCanSelectButton);
    }

    private void Update() {
        Vector2 position = Vector2.zero;

        if(anchor) {
            position = CalculateAnchorScreenPosition();
        } else {
            position = transform.position;
        }
        SetPosition(position.x, position.y);
    }
    Vector2 CalculateAnchorScreenPosition() {
        Vector2 position = Vector2.zero;

        switch(anchorPoint) {
            case ScreenPositionAnchorPoint.BottomLeft : 
                position.y = -(ScreenHeight / 2) + Height;
                position.x = -(ScreenWidth / 2) + buttonSize;
                break;
            case ScreenPositionAnchorPoint.BottomCenter : 
                position.y = -(ScreenHeight / 2) + Height;
                position.x = -(Width / 2);
                break;
            case ScreenPositionAnchorPoint.BottomRight : 
                position.y = -(ScreenHeight / 2) + Height;
                position.x = (ScreenWidth / 2) -Width;
                break;
            case ScreenPositionAnchorPoint.MiddleLeft : 
                position.y = Height/2;
                position.x = -(ScreenWidth / 2) + buttonSize;
                break;
            
            case ScreenPositionAnchorPoint.MiddleCenter : 
                position.y = Height/2;
                position.x = -(Width / 2);
                break;
            case ScreenPositionAnchorPoint.MiddleRight : 
                position.y =  Height/2;
                position.x = (ScreenWidth / 2) -Width;
                break;
            case ScreenPositionAnchorPoint.TopLeft : 
                position.y = (ScreenHeight / 2) - Height;
                position.x = -(ScreenWidth / 2) + buttonSize;
                break;
            case ScreenPositionAnchorPoint.TopCenter : 
                position.y = -(ScreenHeight / 2) - Height;
                position.x = -(Width / 2);
                break;
            case ScreenPositionAnchorPoint.TopRight : 
                position.y = (ScreenHeight / 2) - Height;
                position.x = (ScreenWidth / 2) - Width;
                break;
        }
        return anchorOffset + position;
    }

    CommandButton CreateButton() {
        //새로운 게임 오브젝트를 생성한다
        GameObject go = new GameObject("CommandButton");

        //컴포넌트를 추가한다
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>();

        go.transform.parent = transform;

        //초기화
        CommandButton button = go.AddComponent<CommandButton>();
        button.Init(this);

        return button;
    }

    void InitButtonPositions() {
        int i = 0;
        float xPos = 0;
        float yPos = 0;

        for (int r = 0; r < buttonRows; r++)
        {
            xPos = 0;

            for (int c = 0; c < buttonColumns; c++)
            {
                commandButtons[i].transform.localScale = new
                    Vector3(buttonSize, buttonSize, 0);
                commandButtons[i].transform.localPosition = new
                    Vector3(xPos, yPos, 0);
                i++;
                xPos += buttonSize + buttonColumnSpacing;
            }
            yPos -= buttonSize + buttonRowSpacing;
        }
    }

    void InitCommandButtons() {
        commandButtons = new CommandButton[(int)buttonRows * (int)buttonColumns];

        for(int i = 0; i < commandButtons.Length; i++) {
            var newButton = CreateButton();
            if(i < GameState.CurrentPlayer.Inventory.Count) {
                newButton.AddInventoryItem(GameState.CurrentPlayer.Inventory[i]);
            }
            commandButtons[i] = newButton;
        }
        InitButtonPositions();
    }

    void SetPosition(float x, float y) {
        transform.position = new Vector3(x, y, 0);
    }

    public void ResetSelection(CommandButton button) {
        if(selectedButton != null) {
            selectedButton.ClearSelection();
        }
        selectedButton = button;
    }

    public void Selectbutton(CommandButton button) {

        if(selectedButton != null) {
            selectedButton.ClearSelection();
        }

        selectedButton = button;

        if(selectedButton != null) {
            MessagingManager.Instance.BroadcastInventoryEvent(selectedButton.Item);
        } else {
            MessagingManager.Instance.BroadcastInventoryEvent(null);
        }
    }

    void SetCanSelectButton(bool state) {
        canSelectButton = !state;
    }

    private void OnDestroy() {
        if(MessagingManager.Instance != null) {
            MessagingManager.Instance.UnSubscribeUIEvent(SetCanSelectButton);
        }
    }

}
