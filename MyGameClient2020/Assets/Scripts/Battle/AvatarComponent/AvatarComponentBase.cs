﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// avatar 组件基类
/// </summary>
namespace MGame.GameBattle.Logic
{
    public class AvatarComponentBase
    {
        protected LogicAvatar pAvatar;
        public bool updateWhileDead = false;
        public virtual void Initialize(LogicAvatar pAvatar)
        {
            this.pAvatar = pAvatar;
        }
        public virtual void Update()
        {

        }
   
        public virtual void OnDestroy()
        {

        }
        public static AvatarComponentBase ComponentFactory(AvatarComponentType eType, LogicAvatar pAvatar)
        {
            AvatarComponentBase pComponent = null;

            switch (eType)
            {
                case AvatarComponentType.ComponentAI:
                    pComponent = new AvatarComponentAI();
                    break;
                case AvatarComponentType.ComponentSkill:
                    pComponent = new AvatarComponentSkill();
                    break;
                case AvatarComponentType.ComponentSkillBuffEffectReceiver:
                    //pComponent = new AvatarComponentSkillBuffReceiver();
                    break;
                case AvatarComponentType.ComponentState:
                    pComponent = new AvatarComponentState();
                    break;
                case AvatarComponentType.ComponentAttribute:
                    pComponent = new AvatarComponentAttribute();
                    break;
                case AvatarComponentType.ComponentMovement:
                    pComponent = new AvatarComponentMovement();
                    break;
                case AvatarComponentType.ComponentAnim:
                    pComponent = new AvatarComponentAnim();
                    break;
                case AvatarComponentType.ComponentEvent:
                    //pComponent = new AvatarComponentEvent();
                    break;
                default:
                    throw new System.Exception(eType + " is not defined.");
            }

            return pComponent;
        }
    }
}
