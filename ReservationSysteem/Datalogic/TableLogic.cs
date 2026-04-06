public class TableLogic
{
    private TableAccess _access = new();

    public List<TableModel> GetAllTables()
    {
        var tables = _access.GetAllTables();
        return tables ?? new List<TableModel>();
    }
}