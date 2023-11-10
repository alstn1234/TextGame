using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ConsoleTables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TextGame
{
    internal class Program
    {
        private static string path = Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName + @"\saves";
        private static string[] dungeonName = new[] { "쉬운 던전", "일반 던전", "어려운 던전", "극악 던전" };
        private static int[,] dungeonInfo = new[,]
            {
                // 0 : 권장 방어력, 2 : 획득 골드, 3 : 획득 경험치
                { 5, 1000, 108},
                {11, 1700, 20 },
                {17, 2500, 30 },
                {30, 4500, 40 }
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
        private const int dungeonExp = 2;
        private static Character player;
        private static List<Item> shopItemList;
        private static List<Item> myItemList;

        static void Main(string[] args)
        {
            string name = "";
            Console.Title = "Sparta Dungeon";
            if (!File.Exists(path + @"\player"))
            {
                Console.WriteLine("이름을 정해주세요.");
                Console.Write(">> ");
                name = Console.ReadLine();
                while (name == "")
                {
                    Console.WriteLine("닉네임을 다시 입력주세요.");
                    Console.Write(">> ");
                }
            }
            GameDataSetting(name);
            DisplayGameIntro(); /*SetData();*/
        }
        static void SetData()
        {
            string _myItemList = JsonConvert.SerializeObject(myItemList.ToArray(), Formatting.Indented);
            string _shopItemList = JsonConvert.SerializeObject(shopItemList.ToArray(), Formatting.Indented);
            string _player = JsonConvert.SerializeObject(player, Formatting.Indented);
            File.WriteAllText(path + @"\myItemList", _myItemList.ToString());
            File.WriteAllText(path + @"\shopItemList", _shopItemList.ToString());
            File.WriteAllText(path + @"\player", _player.ToString());
        }
        static void GameDataSetting(string name)
        {
            // 캐릭터 정보 세팅
            if (File.Exists(path + @"\player"))  // 저장된 파일이 있을 경우
            {
                string data = File.ReadAllText(path + @"\player");
                player = JsonConvert.DeserializeObject<Character>(data);
            }
            else
                player = new Character(name, "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            if (File.Exists(path + "@/myItemList"))
            {
                string data = File.ReadAllText(path + @"\myItemList");
                myItemList = JsonConvert.DeserializeObject<List<Item>>(data);
            }
            else
                myItemList = new List<Item>{
                ArmorCreate("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 5, 0),
                WeaponCreate("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0),
                WeaponCreate("파리채", "몬스터를 아무리 때려도 죽지 않습니다.", 1, 0),
                ArmorCreate("비닐갑옷", "비닐로 만들어져 아무 효과도 없습니다.", 1, 0)
            };
            if (File.Exists(path + "@/shopItemList"))
            {
                string data = File.ReadAllText(path + @"\shopItemList");
                shopItemList = JsonConvert.DeserializeObject<List<Item>>(data);

            }else
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
            string msg = "";
            while (true)
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
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("6. 데이터 저장");
                Console.WriteLine();
                Console.Write(msg);
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int input = CheckValidInput(1, 6);
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
                    case 5:
                        DisplayRest();
                        break;
                    case 6:
                        SetData();
                        msg = "저장이 완료되었습니다.\n\n";
                        break;
                }
            }
        }
        static void DisplayMyInfo()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
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
            Console.WriteLine("1. 닉네임 변경");
            Console.WriteLine("0. 나가기");
            Console.Write(">> ");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayChangeName();
                    break;
            }
        }
        static void DisplayChangeName()
        {
            string msg = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("닉네임 변경");
                Console.ResetColor();
                Console.WriteLine("캐릭터의 닉네임을 변경합니다.");
                Console.WriteLine("닉네임 변경 시 5000G가 차감됩니다.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"현재 닉네임 {player.Name}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write(msg);
                Console.WriteLine("닉네임을 입력해주세요.");
                Console.Write(">> ");
                string newName = Console.ReadLine();

                while (newName == "")
                {
                    Console.WriteLine("다시 입력해주세요.");
                    Console.Write(">> ");
                    player.ChangeName(newName);
                    player.ChangeGold(-5000);
                }

                if (newName.Length == 1 && char.IsDigit(newName[0]) && int.Parse(newName) == 0)
                    DisplayMyInfo();

                if (player.Gold < 5000)
                    msg = "골드가 부족합니다.\n\n";
                
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
            Console.Write(">> ");

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
            string msg = "";
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
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine(msg);
                Console.WriteLine("원하시는 행동을 입력해주세요.(번호 입력 : 구매)");
                Console.Write(">> ");

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
                            msg = $"{item.Name} 구매 완료\n";
                            myItemList.Add(item.DeepCopy());
                            player.ChangeGold(-item.Price);
                            item.Buy();
                        }
                        else if (item.Price == -1)
                        {
                            msg = $"{item.Name} 는 이미 구매한 아이템입니다.\n";
                        }
                        else
                        {
                            msg = "골드가 부족합니다.\n";
                        }
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
                Console.Write(">> ");

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
                        player.ChangeGold(item.Price);
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
            Console.Write(">> ");

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
                Console.Write(">> ");

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
                Console.Write(">> ");

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
            Console.WriteLine("4. 극악 던전      | 방어력 30 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(0, 4);
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
                (100 + random.Next(player.GetTotalAtk(), player.GetTotalAtk() * 2 + 1)) / 100;
            int hpLoss = random.Next(20, 35) - player.Def + recommendedDefense;

            DungeonExploration();

            if ((player.Def < recommendedDefense && random.Next(0, 10) < 4) || hpLoss > player.Hp)
            {
                DungeonFail();
            }
            // 던전 클리어
            else
            {
                var gainExp = dungeonInfo[dungeonNumber - 1, dungeonExp];

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("모든 몬스터를 퇴치하였습니다!");
                Console.WriteLine("던전 클리어");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{dungeonName[dungeonNumber - 1]}을 클리어 하였습니다."); Console.WriteLine();
                Console.WriteLine();
                player.ChangeExp(gainExp);
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"{player.Level} Level\nExp {player.Exp}");
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - hpLoss}");
                Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + goldReward} G");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                player.ChangeHp(-hpLoss);
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
            Console.WriteLine("몬스터에게 당했습니다!");
            Console.WriteLine("던전 공략 실패");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"체력 {player.Hp} -> {player.Hp / 2}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            player.Hp /= 2;
            int input = CheckValidInput(0, 0);
            DisplayDungeon();
        }
        static void DisplayRest()
        {
            string msg = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("휴식하기");
                Console.ResetColor();
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (5~20 랜덤 회복)");
                Console.WriteLine();
                Console.WriteLine($"현재 체력 : {player.Hp}\n보유 골드 : {player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write(msg);
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int input = CheckValidInput(0, 1);
                switch (input)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                    case 1:
                        int extraHp = new Random().Next(5, 20);
                        if (player.Gold >= 500 && player.Hp < 100)
                        {
                            msg = $"휴식을 완료했습니다. 체력 {player.Hp} -> {player.Hp + extraHp}\n\n";
                            player.ChangeHp(extraHp);
                            player.ChangeGold(-500);
                        }
                        else
                        {
                            msg = "골드가 부족하거나 HP가 최대입니다.\n\n";
                        }
                        break;
                }
            }
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
                Console.Write(">> ");
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
