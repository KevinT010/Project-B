public class MenuModel
{
    public Int64 Id { get; set; }
    public string MenuName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string FoodCategory { get; set; }
    public Int64? AllergenId { get; set; }
    public string AllergenName { get; set; }
    public bool IsActive { get; set; }

    public MenuModel()
    {
    }

    public MenuModel(string menuName, string name, string description, double price, string foodCategory, Int64? allergenId)
    {
        MenuName = menuName;
        Name = name;
        Description = description;
        Price = price;
        FoodCategory = foodCategory;
        AllergenId = allergenId;
    }
}