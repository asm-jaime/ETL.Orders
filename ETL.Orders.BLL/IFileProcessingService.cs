namespace ETL.Orders.BLL;

public interface IFileProcessingService
{
    Task ProcessFile(string filePath);
}
