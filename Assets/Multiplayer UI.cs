using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class NetworkUI : MonoBehaviour
{
    public RelayManager relayManager;
    public TMP_InputField joinCodeInput;
    public Button hostButton;
    public Button joinButton;

    private void Start()
    {
        hostButton.onClick.AddListener(() => relayManager.StartHost());
        joinButton.onClick.AddListener(() => relayManager.JoinGame(joinCodeInput.text));
    }
}

