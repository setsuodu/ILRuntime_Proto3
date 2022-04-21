namespace HotFix
{
    public enum ErrorCode : byte
    {
        LobbyIsFull,        //大厅爆满
        RoomIsFull,         //房间爆满
        UserNameUsed,       //账号已经注册
        Be_Kicked,          //被踢了（顶号/GM）
    }

    public enum PlayerStatus : byte
    {
        Offline     = 0,    //离线
        AtLobby     = 1,    //在大厅
        Matching    = 2,    //匹配中
        AtRoomWait  = 3,    //在房间
        AtRoomReady = 4,    //在房间
        AtBattle    = 5,    //在战场
        Reconnect   = 6,    //异常掉线，等待重连
    }

    public enum SeatInfo : short
    {
        NONE        = -1,   //没人或不在房间
        HOST        = 0,    //主位
        GUEST       = 1,    //客位
    }

    public enum PacketType : byte
    {
        Connected = 0   ,   //连接成功
        ///////////////////////////////////////////////
        C2S_RegisterReq ,   //注册请求
        C2S_LoginReq    ,   //登录请求
        C2S_LogoutReq   ,   //登出请求
        C2S_UserInfo    ,   //请求用户信息
        C2S_Chat        ,   //聊天消息
        C2S_Settings    ,   //设置选项
        
        C2S_RoomList    ,   //房间列表
        C2S_CreateRoom  ,   //创建房间
        C2S_JoinRoom    ,   //加入房间
        C2S_LeaveRoom   ,   //离开房间

        C2S_GameReady   ,   //请求准备
        C2S_BattleStart ,   //请求开始战斗
        C2S_BattleQuit  ,   //离开比赛（认输） =>返回大厅
        C2S_BattleEnd   ,   //上报比赛结果（双方都要发，由战斗系统判定）
        ///////////////////////////////////////////////
        S2C_LoginResult ,   //登录、注册结果
        S2C_LogoutResult,   //登出结果
        S2C_UserInfo    ,   //下发用户信息
        S2C_Chat        ,   //聊天消息广播
        S2C_Settings    ,   //设置选项
        
        S2C_RoomList    ,   //房间列表
        S2C_RoomInfo    ,   //创建、加入房间后，获取房间内信息
        S2C_LeaveRoom   ,   //离开房间

        S2C_GameReady   ,   //准备结果
        S2C_LoadScene   ,   //跳转场景
        S2C_BattleEnd   ,   //比赛结束，结算
        S2C_ErrorOperate,   //错误代码
    }
}