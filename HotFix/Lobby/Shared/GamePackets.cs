namespace HotFix
{
    public enum ErrorCode : byte
    {
        LobbyIsFull,        //��������
        RoomIsFull,         //���䱬��
        UserNameUsed,       //�˺��Ѿ�ע��
        Be_Kicked,          //�����ˣ�����/GM��
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
        Connected = 0   ,   //���ӳɹ�
        ///////////////////////////////////////////////
        C2S_RegisterReq ,   //ע������
        C2S_LoginReq    ,   //��¼����
        C2S_LogoutReq   ,   //�ǳ�����
        C2S_UserInfo    ,   //�����û���Ϣ
        C2S_Chat        ,   //������Ϣ
        C2S_Settings    ,   //����ѡ��
        
        C2S_RoomList    ,   //�����б�
        C2S_CreateRoom  ,   //��������
        C2S_JoinRoom    ,   //���뷿��
        C2S_LeaveRoom   ,   //�뿪����

        C2S_GameReady   ,   //����׼��
        C2S_BattleStart ,   //����ʼս��
        C2S_BattleQuit  ,   //�뿪���������䣩 =>���ش���
        C2S_BattleEnd   ,   //�ϱ����������˫����Ҫ������ս��ϵͳ�ж���
        ///////////////////////////////////////////////
        S2C_LoginResult ,   //��¼��ע����
        S2C_LogoutResult,   //�ǳ����
        S2C_UserInfo    ,   //�·��û���Ϣ
        S2C_Chat        ,   //������Ϣ�㲥
        S2C_Settings    ,   //����ѡ��
        
        S2C_RoomList    ,   //�����б�
        S2C_RoomInfo    ,   //���������뷿��󣬻�ȡ��������Ϣ
        S2C_LeaveRoom   ,   //�뿪����

        S2C_GameReady   ,   //׼�����
        S2C_LoadScene   ,   //��ת����
        S2C_BattleEnd   ,   //��������������
        S2C_ErrorOperate,   //�������
    }
}