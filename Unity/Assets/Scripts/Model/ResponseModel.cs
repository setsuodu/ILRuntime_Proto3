public class ResponseModel
{
    public int code;
    public string message;
    public string json;
    public ResponseModel() { }
    public ResponseModel(int _code, string _message, string _json)
    {
        this.code = _code;
        this.message = _message;
        this.json = _json;
    }
}
