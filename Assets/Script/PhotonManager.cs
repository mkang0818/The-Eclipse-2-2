using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //���� �Է�
    private readonly string version = "1.0f";
    //����� ���̵� �Է�
    private string userld = "Mary";

    void Awake()
    {
        // ���� ���� �����鿡�� �ڵ����� ���� �ε�
        PhotonNetwork.AutomaticallySyncScene = true;
        // ���� ������ �������� ���� ���
        PhotonNetwork.GameVersion = version;
        // ���� ���̵� �Ҵ�
        PhotonNetwork.NickName = userld;
        // ���� ������ ��� Ƚ�� ����. �ʴ� 30ȸ
        Debug.Log(PhotonNetwork.SendRate);
        // ���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    //���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.inLoby ={PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby(); // �κ� ����
    }

    // �κ� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.inLoby ={PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom(); //���� ��Ī ����ŷ ��� ����
    }

    // ������ �� ������ �������� ��� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Filed {returnCode}:{message}");

        // ���� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;  // �ִ� ������ �� : 20��
        ro.IsOpen = true;   // ���� ���� ����
        ro.IsVisible = true;  // �κ񿡼� �� ��Ͽ� ���� ��ų�� ����  

        // �� ����
        PhotonNetwork.CreateRoom("My Room", ro);
    }

    // �� ������ �Ϸ�� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    // �뿡 ������ �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        // �뿡 ������ ����� ���� Ȯ��
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName}.{player.Value.ActorNumber}");
            // $ => string.format() ���ڿ��� ��ȯ
        }

        // ĳ���� ���� ������ �迭�� ����
        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, points.Length);
        // ĳ���͸� ����
        PhotonNetwork.Instantiate("Player1", points[idx].position, points[idx].rotation, 0);
    }
    void Start()
    {

    }

    void Update()
    {

    }
}