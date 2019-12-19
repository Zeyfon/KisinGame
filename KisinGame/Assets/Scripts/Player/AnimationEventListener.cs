using HutongGames.PlayMaker;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    PlayMakerFSM[] pFSMs;

    void Start()
    {
        pFSMs = GetComponents<PlayMakerFSM>();
    }
    //This scripts update the value of different global variables in PlayMaker.
    // In order to use the Animator Events from the Mecanim

    #region Actions

    void AddImpulse()
    {
        int a = 1;
        FsmVariables.GlobalVariables.GetFsmInt("action").Value = a;
    }
    void DashFareJump()
    {
        int a = 1;
        FsmVariables.GlobalVariables.GetFsmInt("action").Value = a;
    }
    void ComboWindow()
    {
        int a = 2;
        FsmVariables.GlobalVariables.GetFsmInt("action").Value = a;
    }
    void ComboWindowEnds()
    {
        int a = 4;
        FsmVariables.GlobalVariables.GetFsmInt("action").Value = a;
    }
    void Potion_Take()
    {
        pFSMs[0].Fsm.Event("Potion_Take");
    }

    #endregion

#region     DamageSenders

    void SwordLightAttackDamage()
    {
        int a = 1;
        FsmVariables.GlobalVariables.GetFsmInt("playerDamageType").Value = a;
    }

    void SwordHeavyAttackDamage()
    {
        int a = 2;
        FsmVariables.GlobalVariables.GetFsmInt("playerDamageType").Value = a;
    }

    void SwordLightAttackChargeDamage()
    {
        int a = 3;
        FsmVariables.GlobalVariables.GetFsmInt("playerDamageType").Value = a;
    }

    void SwordHeavyAttackChargeDamage()
    {
        int a = 4;
        FsmVariables.GlobalVariables.GetFsmInt("playerDamageType").Value = a;
    }
    #endregion

    #region Effects

    void SwordLightAttack1Sound()
    {
        int a = 1;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void SwordLightAttack2Sound()
    {
        int a = 2;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void SwordHeavyAttack1Sound()
    {
        int a = 3;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void SwordHeavyAttack2Sound()
    {
        int a = 4;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void ChargePhaseSound()
    {
        int a = 5;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void FullyChargedSound()
    {
        int a = 6;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void SwordLightAttackChargeSound()
    {
        int a = 7;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void SwordHeavyAttackChargeSound()
    {
        int a = 8;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void DamageSounds()
    {
        int a = 9;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
        Debug.Log("Si Dañó");
    }

    void DeadSound()
    {
        int a = 10;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void JumpSound()
    {
        int a = 11;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }

    void DashSound()
    {
        int a = 12;
        FsmVariables.GlobalVariables.GetFsmInt("effects").Value = a;
    }


    #endregion

    #region FootSteps
    void Footstep()
    {
        int a = 1;
        FsmVariables.GlobalVariables.GetFsmInt("footstep").Value = a;
    }

    #endregion

    #region Fair Play
    void FairPlayOverride()
    {
        int a = 1;
        FsmVariables.GlobalVariables.GetFsmInt("fairPlay").Value = a;
    }
    #endregion

    #region RestartGame

    void PlayerDead()
    {
        int a = 1;
        FsmVariables.GlobalVariables.GetFsmInt("sceneRestart").Value = a;
    }

    #endregion

}



