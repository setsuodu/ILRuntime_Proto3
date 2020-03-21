[System.Serializable]
public class ABInfo
{
    public string filePath;
    public string md5;
    public string[] depend;
    public ABInfo() { }
    public ABInfo(string _filePath, string _md5, string[] _depend)
    {
        this.filePath = _filePath;
        this.md5 = _md5;
        this.depend = _depend;
    }
}
