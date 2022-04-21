// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: TheMsg.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace HotFix {

  /// <summary>Holder for reflection information generated from TheMsg.proto</summary>
  public static partial class TheMsgReflection {

    #region Descriptor
    /// <summary>File descriptor for TheMsg.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TheMsgReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxUaGVNc2cucHJvdG8iJwoGVGhlTXNnEgwKBG5hbWUYASABKAkSDwoHY29u",
            "dGVudBgCIAEoCSIhCgpUaGVNc2dMaXN0EhMKC2NvbnRlbnRKc29uGAEgASgJ",
            "IlEKB01zZ0luZm8SDwoHcm9vbV9pZBgBIAEoBRIRCglyb29tX25hbWUYAiAB",
            "KAkSDwoHY3VyX251bRgDIAEoBRIRCglsaW1pdF9udW0YBCABKAVCCaoCBkhv",
            "dEZpeGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::HotFix.TheMsg), global::HotFix.TheMsg.Parser, new[]{ "Name", "Content" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::HotFix.TheMsgList), global::HotFix.TheMsgList.Parser, new[]{ "ContentJson" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::HotFix.MsgInfo), global::HotFix.MsgInfo.Parser, new[]{ "RoomId", "RoomName", "CurNum", "LimitNum" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class TheMsg : pb::IMessage<TheMsg> {
    private static readonly pb::MessageParser<TheMsg> _parser = new pb::MessageParser<TheMsg>(() => new TheMsg());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TheMsg> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::HotFix.TheMsgReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TheMsg() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TheMsg(TheMsg other) : this() {
      name_ = other.name_;
      content_ = other.content_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TheMsg Clone() {
      return new TheMsg(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "content" field.</summary>
    public const int ContentFieldNumber = 2;
    private string content_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Content {
      get { return content_; }
      set {
        content_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TheMsg);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TheMsg other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (Content != other.Content) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Content.Length != 0) hash ^= Content.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Content.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Content);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Content.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Content);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TheMsg other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Content.Length != 0) {
        Content = other.Content;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            Content = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class TheMsgList : pb::IMessage<TheMsgList> {
    private static readonly pb::MessageParser<TheMsgList> _parser = new pb::MessageParser<TheMsgList>(() => new TheMsgList());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TheMsgList> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::HotFix.TheMsgReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TheMsgList() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TheMsgList(TheMsgList other) : this() {
      contentJson_ = other.contentJson_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TheMsgList Clone() {
      return new TheMsgList(this);
    }

    /// <summary>Field number for the "contentJson" field.</summary>
    public const int ContentJsonFieldNumber = 1;
    private string contentJson_ = "";
    /// <summary>
    /// repeated string content = 1;
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ContentJson {
      get { return contentJson_; }
      set {
        contentJson_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TheMsgList);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TheMsgList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ContentJson != other.ContentJson) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ContentJson.Length != 0) hash ^= ContentJson.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ContentJson.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ContentJson);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ContentJson.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ContentJson);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TheMsgList other) {
      if (other == null) {
        return;
      }
      if (other.ContentJson.Length != 0) {
        ContentJson = other.ContentJson;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            ContentJson = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class MsgInfo : pb::IMessage<MsgInfo> {
    private static readonly pb::MessageParser<MsgInfo> _parser = new pb::MessageParser<MsgInfo>(() => new MsgInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MsgInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::HotFix.TheMsgReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MsgInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MsgInfo(MsgInfo other) : this() {
      roomId_ = other.roomId_;
      roomName_ = other.roomName_;
      curNum_ = other.curNum_;
      limitNum_ = other.limitNum_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MsgInfo Clone() {
      return new MsgInfo(this);
    }

    /// <summary>Field number for the "room_id" field.</summary>
    public const int RoomIdFieldNumber = 1;
    private int roomId_;
    /// <summary>
    /// 房间Id
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int RoomId {
      get { return roomId_; }
      set {
        roomId_ = value;
      }
    }

    /// <summary>Field number for the "room_name" field.</summary>
    public const int RoomNameFieldNumber = 2;
    private string roomName_ = "";
    /// <summary>
    /// 房间名
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string RoomName {
      get { return roomName_; }
      set {
        roomName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "cur_num" field.</summary>
    public const int CurNumFieldNumber = 3;
    private int curNum_;
    /// <summary>
    /// 当前玩家数
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CurNum {
      get { return curNum_; }
      set {
        curNum_ = value;
      }
    }

    /// <summary>Field number for the "limit_num" field.</summary>
    public const int LimitNumFieldNumber = 4;
    private int limitNum_;
    /// <summary>
    /// 玩家总数
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int LimitNum {
      get { return limitNum_; }
      set {
        limitNum_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MsgInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MsgInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (RoomId != other.RoomId) return false;
      if (RoomName != other.RoomName) return false;
      if (CurNum != other.CurNum) return false;
      if (LimitNum != other.LimitNum) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (RoomId != 0) hash ^= RoomId.GetHashCode();
      if (RoomName.Length != 0) hash ^= RoomName.GetHashCode();
      if (CurNum != 0) hash ^= CurNum.GetHashCode();
      if (LimitNum != 0) hash ^= LimitNum.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (RoomId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(RoomId);
      }
      if (RoomName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(RoomName);
      }
      if (CurNum != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(CurNum);
      }
      if (LimitNum != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(LimitNum);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (RoomId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RoomId);
      }
      if (RoomName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(RoomName);
      }
      if (CurNum != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(CurNum);
      }
      if (LimitNum != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(LimitNum);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MsgInfo other) {
      if (other == null) {
        return;
      }
      if (other.RoomId != 0) {
        RoomId = other.RoomId;
      }
      if (other.RoomName.Length != 0) {
        RoomName = other.RoomName;
      }
      if (other.CurNum != 0) {
        CurNum = other.CurNum;
      }
      if (other.LimitNum != 0) {
        LimitNum = other.LimitNum;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            RoomId = input.ReadInt32();
            break;
          }
          case 18: {
            RoomName = input.ReadString();
            break;
          }
          case 24: {
            CurNum = input.ReadInt32();
            break;
          }
          case 32: {
            LimitNum = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
