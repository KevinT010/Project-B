public class GuestChoiceModel
{
    public Int64 Id { get; set; }
    public Int64 MenuItemId { get; set; }
    public Int64 GuestId { get; set; }
    public int Quantity { get; set; }

    public GuestChoiceModel()
    {
    }

    public GuestChoiceModel(int MenuItemId, int GuestId, int Quantity)
    {
        this.MenuItemId = MenuItemId;
        this.GuestId = GuestId; 
        this.Quantity = Quantity;
    }

}