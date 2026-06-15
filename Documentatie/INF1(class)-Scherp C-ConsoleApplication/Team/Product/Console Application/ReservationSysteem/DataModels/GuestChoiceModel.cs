public class GuestChoiceModel
{
    public Int64 Id { get; set; }
    public Int64 MenuItemId { get; set; }
    public Int64 GuestId { get; set; }
    public int Quantity { get; set; }

    public GuestChoiceModel()
    {
    }

    public GuestChoiceModel(long MenuItemId, long GuestId, int Quantity)
    {
        this.MenuItemId = MenuItemId;
        this.GuestId = GuestId; 
        this.Quantity = Quantity;
    }

}