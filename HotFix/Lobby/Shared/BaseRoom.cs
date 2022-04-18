using System;

namespace HotFix
{
    public class BaseRoom : IDisposable
    {
        public BaseRoom(int id, BasePlayer host, BasePlayer guest)
        {
            RoomID = id;
        }

        public readonly int RoomID; // 当前房间ID（1~65535）
        public string BattleID;     // 服务器战斗编号
        public int Seed;            // 随机种子
        public byte MapId = 0;      // 地图编号（暂时用不到）
        //public BattleMode BattleMode;

        // 一个房间必须满足有2个人(掉线?)
        public virtual BasePlayer[] m_PlayerList { get; protected set; }
        public virtual BasePlayer hostPlayer => m_PlayerList[0];
        public virtual BasePlayer guestPlayer => m_PlayerList[1];
        public virtual void Dispose() { }

        public static int CheckWinnerSeatId(int hostHP, int guestHP)
        {
            int winnerSeatId = 0;
            if (hostHP == guestHP)
                winnerSeatId = -1;
            else
                winnerSeatId = hostHP > guestHP ? 0 : 1;
            return winnerSeatId;
        }

        public override string ToString()
        {
            string str = $"房间#{RoomID}，";
            return str;
        }
    }
}