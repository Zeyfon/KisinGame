using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Spine.Unity;
using Spine;

public class WeaknessChange : MonoBehaviour
{
    [SerializeField] int weakness=0;
    [SerializeField] List<int> weaknesses = new List<int> { 1, 2, 3 };

    Skeleton skeleton;

    private void Start()
    {
        StartCoroutine(SetInitialWeakness());
        GetSpineInfo();
    }

    void GetSpineInfo()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        if (skeletonAnimation == null)
        {
            Debug.LogWarning("No Spine Assets are used");
            return;
        }
        Spine.AnimationState spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
    }


    IEnumerator SetInitialWeakness()
    {
        yield return new WaitForSeconds(2);
        SetNextWeakness();
    }

    #region WeaknessChange
    //Animation Event. Produce Sound
    void SetNextWeakness()
    {
        GetComponent<SoundsConnection>().SendSoundEventToFSM("WeaknessChange");
        weakness = GetNextWeakness();
        UpdateHealthFSM();
        UpdateAppearance();
    }

    int GetNextWeakness()
    {
        if (weaknesses.Contains(weakness))
        {
            weaknesses.Remove(weakness);
        }
        int nextWeakness = weaknesses[Random.Range(0, weaknesses.Count)];
        if(weakness !=0) weaknesses.Add(weakness);
        weakness = nextWeakness;
        return weakness;
    }
    void UpdateHealthFSM()
    {
        FsmEventData myfsmEventData = new FsmEventData();
        myfsmEventData.IntData = weakness;
        HutongGames.PlayMaker.Fsm.EventData = myfsmEventData;
        GetComponent<PlayMakerFSM>().Fsm.Event("ChangeWeakness");
    }
    void UpdateAppearance()
    {
        
        if (weakness == 1)
        {
            if(skeleton == null)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
                return;
            }
            skeleton.SetSkin("AMARILLA");

        }
        if (weakness == 2)
        {
            if(skeleton == null)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
                return;
            }
            skeleton.SetSkin("AZUL");

        }
        if (weakness == 3)
        {
            if(skeleton == null)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                return;
            }
            skeleton.SetSkin("ROJA");

        }

    }
    #endregion


}
