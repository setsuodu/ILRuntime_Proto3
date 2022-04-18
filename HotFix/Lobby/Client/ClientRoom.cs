namespace HotFix
{
    /* 本地房间 */
    public class ClientRoom : BaseRoom
    {
        #region 房间数据
        public ClientRoom(int roomId, ClientPlayer host, ClientPlayer guest) : base(roomId, host, guest)
        {
            //Debug.LogError("测试先执行.ClientRoom"); //子类迟
            m_PlayerList = new ClientPlayer[] { host, guest };
        }

        public override BasePlayer[] m_PlayerList { get; protected set; }
        public override void Dispose()
        {
            // 清空帧同步，清空数据
        }
        public ClientPlayer GetOtherPlayer(short peerId)
        {
            if (m_PlayerList[0].PeerId == peerId && m_PlayerList[1].PeerId != peerId)
            {
                return m_PlayerList[1] as ClientPlayer;
            }
            else if (m_PlayerList[0].PeerId != peerId && m_PlayerList[1].PeerId == peerId)
            {
                return m_PlayerList[0] as ClientPlayer;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}