using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private CharacterSelector[] _characterSelectors;

    private List<PlayerInfo> _playersInfo;

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();

        _playersInfo = new List<PlayerInfo>();
    }

    #region Event Handler

    /*Quando o jogador entrar, atribuir a ele o proximo UI disponivel */
    public void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Joined player");
        //CharacterSelectorInputHandler input = player.GetComponent<CharacterSelectorInputHandler>();
        //input.Join(_characterSelectors[0]);
    }

    public void OnPlayerLeft(PlayerInput player)
    {
        Debug.Log("Left player");
    }

    #endregion

    // Depois do clique no start salvar as informações
    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("number_players", _playersInfo.Count);
        for (int i = 0; i < _playersInfo.Count; i++)
        {
            PlayerPrefs.SetString("player_" + i + "_device", _playersInfo[i].devicePath);
            PlayerPrefs.SetString("player_" + i + "_controlScheme", _playersInfo[i].controlScheme);
            PlayerPrefs.SetInt("player_" + i + "_character", _playersInfo[i].character);
        }
    }
}

public struct PlayerInfo
{
    public int character;
    public string devicePath;
    public string controlScheme;
};