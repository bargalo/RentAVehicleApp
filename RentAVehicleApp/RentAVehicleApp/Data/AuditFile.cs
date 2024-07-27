using RentAVehicleApp.Entities;

namespace RentAVehicleApp.Data;

public class AuditFile
{
    public AuditFile(string auditFileName)
    {
        AuditFileName = auditFileName;
    }
    
    public string AuditFileName { get; private set; }

    public void AddInfo_HasBeenAdded<T>(T entity)
    {
        using (var writer = File.AppendText(AuditFileName))
        {
            writer.WriteLine($"[{DateTime.Now}] {entity} has been added");
        }
    }

    public void AddInfo_HasBeenRemoved<T>(T entity)
    {
        using (var writer = File.AppendText(AuditFileName))
        {
            writer.WriteLine($"[{DateTime.Now}] {entity} has been removed");
        }
    }
}
