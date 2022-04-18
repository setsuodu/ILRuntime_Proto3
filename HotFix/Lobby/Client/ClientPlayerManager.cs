namespace HotFix
{
    // 只管理房间内的2个玩家
    public class ClientPlayerManager
    {
        public ClientPlayerManager() { }

        private ClientPlayer _localPlayer; //自己
        public ClientPlayer LocalPlayer => _localPlayer;
        private ClientPlayer _rivalPlayer; //对手
        public ClientPlayer RivalPlayer => _rivalPlayer;

        // 重置
        public void Reset()
        {
            _localPlayer = null;
            _rivalPlayer = null;
        }
        public void ResetRival()
        {
            _rivalPlayer = null;
        }

        // 增，登录成功，匹配成功，调用
        public void AddClientPlayer(ClientPlayer player, bool isSelf)
        {
            if (isSelf)
                _localPlayer = player;
            else
                _rivalPlayer = player;

            player.ResetToLobby();
        }
    }
}