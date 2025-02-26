using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
    public TextMeshProUGUI joinCodeText;
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void StartHost()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        joinCodeText.text = joinCode;

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
        NetworkManager.Singleton.StartHost();
    }

    public async void JoinGame(string joinCode)
    {
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
        NetworkManager.Singleton.StartClient();
    }
}

