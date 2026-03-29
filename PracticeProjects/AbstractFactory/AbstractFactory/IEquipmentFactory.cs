namespace AbstractFactory.AbstractFactory
{
    /// Абстрактная фабрика для создания экипировки героя.
    /// Позволяет создавать семейства связанных объектов (оружие, броня, расходуемые предметы)
    /// без привязки к конкретным классам.
    public interface IEquipmentFactory
    {
        IWeapon CreateWeapon();
        IArmor CreateArmor();
        IConsumable CreateConsumable();
    }
}
