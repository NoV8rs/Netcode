using Unity.Netcode;
using TMPro;
using UnityEngine;

public class PointSystem : NetworkBehaviour
{
    public TextMeshProUGUI homePointText;
    public TextMeshProUGUI awayPointText;

    private NetworkVariable<int> homePoint = new NetworkVariable<int>(0);
    private NetworkVariable<int> awayPoint = new NetworkVariable<int>(0);

    public override void OnNetworkSpawn()
    {
        homePoint.OnValueChanged += UpdateHomeScoreUI;
        awayPoint.OnValueChanged += UpdateAwayScoreUI;
        
        UpdateHomeScoreUI(0, homePoint.Value);
        UpdateAwayScoreUI(0, awayPoint.Value);
    }
    
    public override void OnDestroy()
    {
        homePoint.OnValueChanged -= UpdateHomeScoreUI;
        awayPoint.OnValueChanged -= UpdateAwayScoreUI;
    }

    [ServerRpc(RequireOwnership = false)]
    public void AddHomePointServerRpc()
    {
        homePoint.Value++;
    }

    [ServerRpc(RequireOwnership = false)]
    public void AddAwayPointServerRpc()
    {
        awayPoint.Value++;
    }

    private void UpdateHomeScoreUI(int oldValue, int newValue)
    {
        homePointText.text = "Points: " + newValue.ToString();
    }

    private void UpdateAwayScoreUI(int oldValue, int newValue)
    {
        awayPointText.text = "Points: " + newValue.ToString();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ResetPointsServerRpc()
    {
        homePoint.Value = 0;
        awayPoint.Value = 0;
    }
}

