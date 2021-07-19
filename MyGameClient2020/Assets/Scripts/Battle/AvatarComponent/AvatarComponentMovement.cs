using Battle.Logic.MMath;
using MGame.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// avatar 移动组件
/// </summary>
namespace Battle.Logic
{
    public class AvatarComponentMovement : AvatarComponentBase
    {
        private bool m_moving;

        public override void Clear()
        {
            base.Clear();
            m_moving = false;
        }

        public void MoveToPos(FixVector3 pos)
        {
            if (!pAvatar.GetComponentMgr().State.IsCurrentPassiveState(AvatarPassiveState.noMoving) /*&& calculateErrataDistance(pAvatar.LocalPos, pos)*/)
            {
                pAvatar.LocalPos = pos;
                m_moving = true;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
                MessageEvent.Broadcast<int, UnityEngine.Vector3>(MessageEvent.EventType.Type_Avatar_PosAndCamera_Event_Change, pAvatar.InstanceId, pAvatar.LocalPos.ToVector3());
#endif
            }
            else
            {
                StopMove();
            }
        }

        public void StopMove()
        {
            m_moving = false;
        }
    }
}
