using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {Idle, Battle, Dialog, Auction}
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState state;

    public void Start(){    
        DialogManager.Instance.OnShowDialog += () => {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnCloseDialog += () => {
            if (state == GameState.Dialog){
                state = GameState.Idle;    
            } 
        };
    }
    private void Update(){
        if (state == GameState.Idle)
        {
            playerController.HandleUpdate();
        } else if (state == GameState.Battle)
        {
            // Handle battle update
        } else if (state == GameState.Dialog)
        {
            // Handle dialog update
            DialogManager.Instance.HandleUpdate();
        } else if (state == GameState.Auction)
        {
            // Handle auction update
        }
    }

}

[CreateAssetMenu(fileName = "ethRealm", menuName = "ethRealm/create new ethRealm", order = 0)]
public class ethRealmBase : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] rarity raritytype;
    [SerializeField] string Name;
    [SerializeField] int MaxHP;
    [SerializeField] int Attack;
    [SerializeField] int Defense;
    
}
public enum rarity {
    None,
    Common,
    Uncommon,
    Rare,
    Epic
}