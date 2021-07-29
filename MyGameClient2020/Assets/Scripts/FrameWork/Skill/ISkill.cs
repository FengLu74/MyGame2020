using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Skill
{
    public interface ISkill
    {
        void Initialize();
        void Start();
        void Update();
        void End();
        void Destroy();

    }
}
