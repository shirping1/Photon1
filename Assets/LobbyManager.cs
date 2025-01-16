using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    private List<RoomInfo> roomList = new List<RoomInfo>();
    private List<Button> enterRoomButtonList = new List<Button>();

    private string gameVersion = "1";

    public Dropdown maxPlayerDropdown;
    public InputField roomNameInputField;
    public Button multiplayButton;
    public Button enterRoomButton;
    public RectTransform mainPanel;
    public RectTransform lobbyPanel;
    public RectTransform roomListPanelContent;
    public RectTransform createRoomPanel;

    private void Start()
    {
        mainPanel.gameObject.SetActive(true);
        lobbyPanel.gameObject.SetActive(false);
        createRoomPanel.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        mainPanel.gameObject.SetActive(false);
        lobbyPanel.gameObject.SetActive(true);

        Debug.Log("���漭�� �κ� ����");
    }

    public override void OnRoomListUpdate(List<RoomInfo> updateRoomList)
    {
        roomList.Clear();

        foreach (RoomInfo room in updateRoomList)
        {
            if (!room.RemovedFromList)
                roomList.Add(room);
        }

        Debug.Log("�� ����Ʈ �޾ƿ�");

        Button newEnterRoomButton;
        enterRoomButtonList.Clear();

        foreach (Button button in enterRoomButtonList)
        {
            Destroy(button);
        }

        foreach (RoomInfo room in roomList)
        {
            newEnterRoomButton = Instantiate(enterRoomButton, roomListPanelContent);
            newEnterRoomButton.onClick.AddListener(() => EnterRoom(room));
            newEnterRoomButton.GetComponentInChildren<Text>().text = $"�� �̸�: {room.Name}   {room.PlayerCount}/{room.MaxPlayers}";
            enterRoomButtonList.Add(newEnterRoomButton);
        }

    }

    public void EnterRoom(RoomInfo room)
    {
        PhotonNetwork.JoinRoom(room.Name);
        Debug.Log("����");
    }

    public void OnClickMultiplay()
    {
        if (PhotonNetwork.IsConnected)
            return;

        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        multiplayButton.interactable = false;

        Debug.Log("���漭�� ����");
    }

    public void OnClickOpenCreateRoomPanel()
    {
        createRoomPanel.gameObject.SetActive(true);
    }

    public void OnClickCreateRoom()
    {
        PhotonNetwork.CreateRoom(roomNameInputField.text, 
            new RoomOptions { 
                MaxPlayers = int.Parse(maxPlayerDropdown.options[maxPlayerDropdown.value].text)
            });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�濡 ������");

        PhotonNetwork.LoadLevel("InGame");
    }
}
