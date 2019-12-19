﻿// (c) copyright Hutong Games, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Add an item to the end of a FSM Array.")]
    public class FsmArrayAdd : BaseFsmVariableAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        [Tooltip("The name of the FSM variable.")]
        public FsmString variableName;

        [RequiredField]
        public FsmVar value;

        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
            value = null;
        }

        public override void OnEnter()
        {
            ggop();
            Finish();
        }

        private void ggop()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go, fsmName.Value))
            {
                return;
            }

            var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);

            fsmArray.Resize(fsmArray.Length + 1);
            value.UpdateValue();
            fsmArray.Set(fsmArray.Length - 1, value.GetValue());
        }
    }
}