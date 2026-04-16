public class MenuModel
{
    public Int64 Id { get; set; }
    public string MenuName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string FoodCategory { get; set; }
    public string Allergens { get; set; }
    public bool IsActive { get; set; }

    public MenuModel()
    {
    }

    public MenuModel(string menuName, string name, string description, decimal price, string foodCategory, string allergens)
    {
        MenuName = menuName;
        Name = name;
        Description = description;
        Price = price;
        FoodCategory = foodCategory;
        Allergens = allergens;
    }     
}