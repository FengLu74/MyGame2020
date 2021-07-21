using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Logic
{
    public class AvatarComponentAnim: AvatarComponentBase
    {
        private Action<string> onAnimPlayCall;
        public override void Initialize(LogicAvatar pAvatar)
        {
            base.Initialize(pAvatar);
        }
        public void AddAnimPlayEventCall(Action<string> OnAnimPlayCall)
        {
            onAnimPlayCall += OnAnimPlayCall;
        }

        public void RemoveAnimPlayEventCall(Action<string> OnAnimPlayCall)
        {
            onAnimPlayCall -= OnAnimPlayCall;
        }
        public void PlayIdleAnim()
        {
            PlayAnim("wait");
        }
        public void PlayFinalAnim(bool win)
        {
            PlayAnim(win ?   "win" :   "defeat");
        }
        public override void Update()
        {
            base.Update();
        }
        public void PlayAnim(string animName)
        {
            if (onAnimPlayCall != null)
            {
                onAnimPlayCall(animName);
            }
        }

        public override void Clear()
        {
            onAnimPlayCall = null;
            base.Clear();
        }
        public override void OnDestroy()
        {
            onAnimPlayCall = null;
            base.OnDestroy();
        }
    }
}
