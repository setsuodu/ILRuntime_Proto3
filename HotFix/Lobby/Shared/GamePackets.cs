namespace HotFix
{
    public enum ErrorCode : byte
    {
        LobbyIsFull,    //��������
        RoomIsFull,     //���䱬��
        UserNameUsed,   //�˺��Ѿ�ע��
        Be_Kicked,      //�����ˣ�����/GM��
    }

    public enum PlayerStatus : byte
    {
        Offline     = 0,    //����
        AtLobby     = 1,    //�ڴ���
        Matching    = 2,    //ƥ����
        AtRoomWait  = 3,    //�ڷ���
        AtRoomReady = 4,    //�ڷ���
        AtBattle    = 5,    //��ս��
        Reconnect   = 6,    //�쳣���ߣ��ȴ�����
    }

    public enum SeatInfo : short
    {
        NONE        = -1,   //û�˻��ڷ���
        HOST        = 0,    //��λ
        GUEST       = 1,    //��λ
    }

    public enum PacketType : byte
    {
        Serialized          = 0,    //��¼
        ///////////////////////////////////////////////
        C2S_RegisterReq     ,   //ע������
        C2S_LoginReq        ,   //��¼����
        C2S_LogoutReq       ,   //�ǳ�����
        C2S_UserInfo        ,   //�����û���Ϣ
        C2S_Settings        ,   //����ѡ��
        C2S_MatchRequest    ,   //����ƥ��
        C2S_MatchCancel     ,   //����ƥ�����뿪
        C2S_MatchQuit       ,   //ƥ��ɹ����뿪
        C2S_RoleSelect      ,   //ƥ��ɹ���ѡ���ɫ
        C2S_GameReady       ,   //����׼��
        C2S_BattleStart     ,   //����ʼս��
        C2S_BattlePause     ,   //������ͣս��
        C2S_BattleQuit      ,   //�뿪���������䣩 =>���ش���
        C2S_BattleEnd       ,   //�ϱ����������˫����Ҫ������ս��ϵͳ�ж���
        ///////////////////////////////////////////////
        S2C_LoginResult     ,   //��¼���
        S2C_LogoutResult    ,   //�ǳ����
        S2C_UserInfo        ,   //�·��û���Ϣ
        S2C_Settings        ,   //����ѡ��
        S2C_MatchResult     ,   //ƥ����
        S2C_RoleSelect      ,   //ѡ���ɫ
        S2C_GameReady       ,   //׼�����
        S2C_LoadScene       ,   //��ת����
        S2C_BattleStart     ,   //��ʼս������һ֡ͬ����
        S2C_BattlePause     ,   //��ͣս������ͣ֡ͬ����
        S2C_BattleEnd       ,   //��������������
        S2C_ErrorOperate    ,   //�������
    }
}