namespace KankuamoInventory.Core.Models;

public class ResultModel<T>
{
    public bool SuccessfulOperation { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
