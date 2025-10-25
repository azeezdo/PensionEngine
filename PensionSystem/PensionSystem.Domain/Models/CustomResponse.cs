namespace PensionSystem.Domain.Models;

public class CustomResponse
{
    public CustomResponse() { }
    public CustomResponse(int code,string response,object? data=null, object? pagedata=null)
    {
        ResponseMessage = response;
        ResponseCode= code;
        Data=data is not null? data : default;
        PaginationData=pagedata is not null? pagedata : default;
           

    }
    public int ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public object? Data { get; set; }
    public object? PaginationData { get; set; }

}