using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleTables;

namespace TextGame
{
    internal class Program
    {
        private static string[] dungeonName = new []{ "쉬운 던전", "일반 던전", "어려운 던전" };
        private static int[,] dungeonInfo = new [,]
            {
                { 5, 1000},
                {11, 1700 },
                {17, 2500 }
            };
        private static string monsterArt = @"
                             __          __          __
                            /o \        /o \        /o \
                           |   |       |   |       |   |
                           |   |       |   |       |   |    
                            \_/         \_/         \_/
                        ";

        private const int dungeonDef = 0;
        private const int dungeonGold = 1;
        private static Character player;
        private static List<Item> shopItemList;
        private static List<Item> myItemList;

        static void Main(string[] args)
        {
            Console.WriteLine("이름을 정해주세요.");
            string name = Console.ReadLine();
            GameDataSetting("Ko");
            DisplayGameIntro();
        }

        static void GameDataSetting(string name)
        {
            // 캐릭터 정보 세팅
            player = new Character(name, "전사", 1, 10, 5, 100, 15000);

            // 아이템 정보 세팅
            myItemList = new List<Item>{
                ArmorCreate("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 5, 0),
                WeaponCreate("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0),
                WeaponCreate("파리채", "몬스터를 아무리 때려도 죽지 않습니다.", 1, 0),
                ArmorCreate("비닐갑옷", "비닐로 만들어져 아무 효과도 없습니다.", 1, 0)
            };

            shopItemList = new List<Item>
            {
                ArmorCreate("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 5, 1000),
                ArmorCreate("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 9, 2000),
                ArmorCreate("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 15, 3500),
                WeaponCreate("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 600),
                WeaponCreate("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 1500),
                WeaponCreate("스파르티의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 2500)
            };

        }

        static void DisplayGameIntro()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
                case 4:
                    DisplayDungeon();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({ player.Job })");
            Console.Write($"공격력 :{player.GetTotalAtk()}");
            if (player.extraAtk > 0)
                Console.Write($" (+{player.extraAtk})");
            Console.WriteLine();
            Console.Write($"방어력 : {player.GetTotalDef()}");
            if (player.extraDef > 0)
                Console.Write($" (+{player.extraDef})");
            Console.WriteLine();
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }
        static void DisplayShop()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("상점");
            Console.ResetColor();

            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명", "가격");
            for (int i = 0; i < shopItemList.Count; i++)
            {
                shopItemList[i].PrintItemInfo(ref table, 0, true);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayBuyItem();
                    break;
                case 2:
                    DisplaySellItem();
                    break;
            }
        }
        static void DisplayBuyItem()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("상점 - 아이템 구매");
                Console.ResetColor();

                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명", "가격");
                for (int i = 0; i < shopItemList.Count; i++)
                {
                    shopItemList[i].PrintItemInfo(ref table, i + 1, true);
                }
                table.Write();
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.(번호 입력 : 구매)");

                int input = CheckValidInput(0, shopItemList.Count);
                switch (input)
                {
                    case 0:
                        DisplayShop();
                        break;
                    default:
                        var item = shopItemList[input - 1];
                        if (item.Price >= 0 && item.Price < player.Gold)
                        {
                            myItemList.Add(item.DeepCopy());
                            player.BuyItem(item.Price);
                            item.Buy();
                        }
                        //else
                        // 구매 실패
                        break;
                }
            }
        }
        static void DisplaySellItem()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("상점 - 아이템 판매");
                Console.ResetColor();

                Console.WriteLine("보유 중인 아이템을 판매할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명", "가격");
                for (int i = 0; i < myItemList.Count; i++)
                {
                    myItemList[i].PrintItemInfo(ref table, i + 1, true);
                }
                table.Write();
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.(번호 입력 : 판매)");

                int input = CheckValidInput(0, myItemList.Count);
                switch (input)
                {
                    case 0:
                        DisplayShop();
                        break;
                    default:
                        var item = myItemList[input - 1];
                        if (item.IsEquip)
                        {
                            item.UnEquip();
                            player.UnequipItem(item);
                        }
                        player.SellItem(item.Price);
                        myItemList.Remove(item);
                        break;
                }
            }
        }
        static void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("인벤토리");
            Console.ResetColor();

            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명");
            for (int i = 0; i < myItemList.Count; i++)
            {
                myItemList[i].PrintItemInfo(ref table, 0, false);
            }
            table.Write();

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayEquipManage();
                    break;
                case 2:
                    DisplayInventorySort();
                    break;
            }

        }
        static void DisplayInventorySort()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("인벤토리 - 아이템 정렬");
                Console.ResetColor();

                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명");
                for (int i = 0; i < myItemList.Count; i++)
                {
                    myItemList[i].PrintItemInfo(ref table, 0, false);
                }
                table.Write();

                Console.WriteLine();
                Console.WriteLine("1. 이름");
                Console.WriteLine("2. 장착순");
                Console.WriteLine("3. 공격력");
                Console.WriteLine("4. 방어력");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = CheckValidInput(0, 4);
                switch (input)
                {
                    case 0:
                        DisplayInventory();
                        break;
                    case 1:   // 이름순
                        myItemList.Sort((ItemA, ItemB) => ItemA.Name.CompareTo(ItemB.Name));
                        break;
                    case 2:   // 장착순
                        myItemList.Sort((ItemA, ItemB) => ItemB.IsEquip.CompareTo(ItemA.IsEquip));
                        break;
                    case 3:   // 공격력
                        myItemList.Sort((ItemA, ItemB) => ItemB.Atk.CompareTo(ItemA.Atk));
                        break;
                    case 4:   // 방어력
                        myItemList.Sort((ItemA, ItemB) => ItemB.Def.CompareTo(ItemA.Def));
                        break;
                }
            }
        }
        static void DisplayEquipManage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("인벤토리");
                Console.ResetColor();

                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명");
                for (int i = 0; i < myItemList.Count; i++)
                {
                    myItemList[i].PrintItemInfo(ref table, i + 1, false);
                }
                table.Write();

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.(번호 입력 : 장착 또는 장착해제)");

                int input = CheckValidInput(0, myItemList.Count);
                switch (input)
                {
                    case 0:
                        DisplayInventory();
                        break;
                    default:
                        Item item = myItemList[input - 1];
                        var itemType = item.GetTypeEnum();
                        if (!item.IsEquip)
                        {
                            item.Equip();
                            player.EquipItem(item);
                        }
                        else
                        {
                            item.UnEquip();
                            player.UnequipItem(item);
                        }
                        break;
                }
            }
        }
        static void DisplayDungeon()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine($"Lv.{player.Level}");
            Console.Write($"공격력 :{player.GetTotalAtk()}");
            if (player.extraAtk > 0)
                Console.Write($" (+{player.extraAtk})");
            Console.WriteLine();
            Console.Write($"방어력 : {player.GetTotalDef()}");
            if (player.extraDef > 0)
                Console.Write($" (+{player.extraDef})");
            Console.WriteLine();
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");


            Console.WriteLine();
            Console.WriteLine("1. 쉬운 던전      | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전      | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                default:
                    EnterDungeon(input);
                    break;
            }
        }
        static void EnterDungeon(int dungeonNumber)
        {
            Random random = new Random();
            int recommendedDefense = dungeonInfo[dungeonNumber - 1, dungeonDef];
            int goldReward = dungeonInfo[dungeonNumber - 1, dungeonGold] *
                (100 + random.Next(player.GetTotalAtk(), player.GetTotalAtk()*2+1)) / 100;
            int hpLoss = random.Next(20, 35) - player.Def + recommendedDefense;

            DungeonExploration();

            if ((player.Def < recommendedDefense && random.Next(0, 10) < 4) || hpLoss > player.Hp)
            {
                    DungeonFail();
            }
            // 던전 클리어
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("던전 클리어");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{dungeonName[dungeonNumber - 1]}을 클리어 하였습니다."); Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - hpLoss}");
                Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + goldReward} G");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                player.Hp -= hpLoss;
                player.Gold += goldReward;
                int input = CheckValidInput(0, 0);
                DisplayDungeon();
            }
        }
        static void DungeonExploration()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("던전 탐험중");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(" .");
                Thread.Sleep(500);
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(monsterArt);
            Console.WriteLine();
            Console.WriteLine("던전에서 몬스터가 나타났습니다!");
            Console.WriteLine();
            Console.Write("몬스터와 전투중 ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(" .");
                Thread.Sleep(500);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        static void DungeonFail()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("던전 공략 실패");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"체력 {player.Hp} -> {player.Hp/2}");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            player.Hp /= 2;
            int input = CheckValidInput(0, 0);
            DisplayDungeon();
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static Item WeaponCreate(string name, string dscr, int atk, int price)
        {
            Weapon item = new Weapon(name, dscr, price, atk, 0);
            return item;
        }
        static Item ArmorCreate(string name, string dscr, int def, int price)
        {
            Armor item = new Armor(name, dscr, price, 0, def);
            return item;
        }
    }

    public enum Pack
    {
        Weapon,
        Armor,
        Unknown
    }

}
