using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictionary
{
    internal class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            Player player = new Player("Victor", 20);

            GameData.AddCard(new Card(1, "Меч новичка", Rarity.Usual));
            GameData.AddCard(new Card(2, "Палка", Rarity.Usual));
            GameData.AddCard(new Card(3, "Медное кольцо", Rarity.Usual));
            GameData.AddCard(new Card(4, "Огненный шар", Rarity.Rare));
            GameData.AddCard(new Card(5, "Морозная буря", Rarity.Rare));
            GameData.AddCard(new Card(6, "Пламенный меч", Rarity.Rare));
            GameData.AddCard(new Card(7, "Кинжал вора", Rarity.Epic));
            GameData.AddCard(new Card(8, "Шапка ушанка", Rarity.Epic));
            GameData.AddCard(new Card(9, "Лук власти", Rarity.Legendary));
            GameData.AddCard(new Card(10, "Корона правления", Rarity.Legendary));

            bool isPlaying = true;

            while (isPlaying)
            {
                Console.WriteLine("1 - Показать каталог карт");
                Console.WriteLine("2 - Открыть сундук");
                Console.WriteLine("3 - Продать карту");
                Console.WriteLine("4 - Показать инвентарь");
                Console.WriteLine("5 - Выйти");
                if(int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("===Каталог===");
                            GameData.ShowCatalog();
                            break;
                        case 2:
                            Console.WriteLine("===Сундук===");
                            OpenLootbox(player, GameData.CardsContainer);
                            break;
                        case 3:
                            Console.WriteLine("===Продажа===");
                            Console.Write("Введите id карты для продажи: ");
                            if (int.TryParse(Console.ReadLine(), out int cardId))
                                SellCard(player, cardId, GameData.CardsContainer);
                            break;
                        case 4:
                            Console.WriteLine("===Инвентарь===");
                            player.ShowInventory();
                            break;
                        case 5: isPlaying = false; break;
                        default:

                            break;
                    }
                }
            }
        }

        static void OpenLootbox(Player player, Dictionary<int, Card> allCards)
        {
            var keys = allCards.Keys.ToArray();
            
            int randomIndex = rand.Next(0, allCards.Count);
            int randomCardId = keys[randomIndex];
            Card card = allCards[randomCardId];

            Console.WriteLine($"Вы открыли сундук и нашли карту: {card.Name} ({card.CardRarity})!");
            player.AddToInventory(card);

            Console.WriteLine();
        }

        static void SellCard(Player player, int cardId, Dictionary<int, Card> allCards)
        {
            if (!allCards.ContainsKey(cardId))
            {
                Console.WriteLine("Такой карты не существует в игре!");
                return;
            }

            Card cardToSell = player.Inventory.FirstOrDefault(c => c.Id == cardId);
            if (cardToSell != null)
            {
                player.Inventory.Remove(cardToSell);
                int price = allCards[cardId].BasePrice;
                player.Gold += price;
                Console.WriteLine($"Вы продали {cardToSell.Name} за {price} монет. Теперь у вас {player.Gold} монет.");
            }
            else
                Console.WriteLine("Карта не найдена.");
            Console.WriteLine();
        }
    }
    
    public class Card
    {
        public int Id { get; }
        public string Name { get; }
        public Rarity CardRarity { get; }
        public int BasePrice { get; }

        public Card(int id, string name, Rarity cardRarity)
        {
            Id = id;
            Name = name;
            CardRarity = cardRarity;

            switch (cardRarity)
            {
                case Rarity.Usual: BasePrice = 10; break;
                case Rarity.Rare: BasePrice = 50; break;
                case Rarity.Epic: BasePrice = 200; break;
                case Rarity.Legendary: BasePrice = 1000; break;
            }
        }
    }

    public class Player
    {
        public string Name { get; }
        public int Gold { get; set; }
        public List<Card> Inventory { get; }

        public Player (string name, int gold)
        {
            Name = name;
            Gold = gold;
            Inventory = new List<Card>();
        }

        public void AddToInventory (Card card)
        {
            Inventory.Add(card);
        }

        public void ShowInventory()
        {
            foreach(Card card in Inventory)
            {
                Console.WriteLine($"Id: {card.Id} - {card.Name}, Цена - {card.BasePrice}");
            }
            Console.WriteLine();
        }
    }

    static class GameData
    {
        static public Dictionary<int, Card> CardsContainer = new Dictionary<int, Card>();

        public static void AddCard(Card card)
        {
            CardsContainer.Add(card.Id, card);
        }

        public static void ShowCatalog()
        {
            foreach (Card card in CardsContainer.Values)
            {
                Console.WriteLine($"Id: {card.Id} - {card.Name}, Цена - {card.BasePrice}");
            }
            Console.WriteLine();
        }
    }

    public enum Rarity
    {
        Usual,
        Rare,
        Epic,
        Legendary
    }
}
