using Battle.Logic;
/// <summary>
/// 黑板数据类
/// 在一棵行为树内维护一个黑板数据，所有子节点皆可访问/修改
/// </summary>
namespace FrameWork.AI
{
    public class BlackBoard
    {
        public LogicAvatar attackDstAvatar;

        public LogicAvatar moveDstAvatar;

        public LogicAvatar passiveStateCaster;
    }
}
