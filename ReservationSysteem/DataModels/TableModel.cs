public class TableModel
{
    public Int64 Id { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }

    public TableModel()
    {
    }

    public TableModel(int tableNumber, int capacity)
    {
        TableNumber = tableNumber;
        Capacity = capacity;
    }
}