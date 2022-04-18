namespace HotFix
{
    // ֻ�������ڵ�2�����
    public class ClientPlayerManager
    {
        public ClientPlayerManager() { }

        private ClientPlayer _localPlayer; //�Լ�
        public ClientPlayer LocalPlayer => _localPlayer;
        private ClientPlayer _rivalPlayer; //����
        public ClientPlayer RivalPlayer => _rivalPlayer;

        // ����
        public void Reset()
        {
            _localPlayer = null;
            _rivalPlayer = null;
        }
        public void ResetRival()
        {
            _rivalPlayer = null;
        }

        // ������¼�ɹ���ƥ��ɹ�������
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