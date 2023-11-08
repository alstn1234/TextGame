using System;
using System.Collections.Generic;
using System.Text;

namespace TextGame
{
    internal class Program
    {
        private static Character player;
        private static List<Item> shopItemList = new List<Item>();
        private static List<Item> myItemList = new List<Item>();
        private static int extraAtk = 0;
        private static int extraDef = 0;

        static void Main(string[] args)
        {
            // Console.WriteLine("이름을 정해주세요.");
            //string name = Console.ReadLine();
            GameDataSetting("Ko");
            DisplayGameIntro();
        }

        static void GameDataSetting(string name)
        {
            // 캐릭터 정보 세팅
            player = new Character(name, "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            myItemList.Add(ArmorCreate("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 5, 0));
            myItemList.Add(WeaponCreate("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0));
            myItemList.Add(WeaponCreate("파리채", "몬스터를 아무리 때려도 죽지 않습니다.", 1, 0));
            myItemList.Add(ArmorCreate("비닐갑옷", "비닐로 만들어져 아무 효과도 없습니다.", 1, 0));
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
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
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.Write($"공격력 :{player.Atk + extraAtk}");
            if (extraAtk > 0)
                Console.Write($" (+{extraAtk})");
            Console.WriteLine();
            Console.Write($"방어력 : {player.Def + extraDef}");
            if (extraDef > 0)
                Console.Write($" (+{extraDef})");
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

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("상점");
            Console.ResetColor();

            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i != 0;)
            {
            // todo : 상점 목록 배열 만들고 출력
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayBuyItem();
                    break;
            }
        }

        static void DisplayBuyItem()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();

            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            // todo : 상점 목록 배열 만들고 출력(앞에 번호)
            for (int i = 0; i != 0;)
            {

            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    // todo : 아이템 구매
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("인벤토리");
            Console.ResetColor();

            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < myItemList.Count; i++)
            {
                Console.Write("- ");
                myItemList[i].PrintItemInfo();
            }
            Console.WriteLine();
            Console.WriteLine("2. 아이템 정렬");
            Console.WriteLine("1. 장착 관리");
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

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("인벤토리 - 아이템 정렬");
                Console.ResetColor();

                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < myItemList.Count; i++)
                {
                    //todo:
                    var item = myItemList[i];
                    Console.Write("- ");
                    item.PrintItemInfo();

                }
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

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("인벤토리");
                Console.ResetColor();

                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < myItemList.Count; i++)
                {
                    var item = myItemList[i];
                    Console.Write($"- {i + 1} ");
                    item.PrintItemInfo();

                }
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
                        // true : Atk, false : Def
                        bool type; int status;
                        item.GetStatus(out status);
                        type = item.GetTypeBool();
                        if (!item.IsEquip)
                        {
                            item.Equip();
                            //Atk
                            if (type)
                                extraAtk += status;
                            //Def
                            else
                                extraDef += status;
                        }
                        else
                        {
                            item.UnEquip();
                            //Atk
                            if (type)
                                extraAtk -= status;
                            //Def
                            else
                                extraDef -= status;
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


    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public int Atk { get; }
        public int Def { get; }
        public string Name { get; }
        public string Dscr { get; }
        public int Price { get; }
        public bool IsEquip { get; set; }
        public Item(string name, string dscr, int price, int atk, int def)
        {
            Name = name;
            Dscr = dscr;
            Price = price;
            Atk = atk;
            Def = def;
            IsEquip = false;
        }
        public virtual void Equip() { }
        public virtual void UnEquip() { }
        public virtual void PrintItemInfo() { }
        public virtual void GetStatus(out int status)
        {
            status = 0;
        }
        public virtual bool GetTypeBool()
        {
            return false;
        }
    }

    public class Weapon : Item
    {
        public Weapon(string name, string dscr, int price, int atk, int def) : base(name, dscr, price, atk, def)
        {

        }
        public override void Equip()
        {
            IsEquip = true;
        }
        public override void UnEquip()
        {
            IsEquip = false;
        }
        public override void GetStatus(out int status)
        {
            status = Atk;
        }
        public override bool GetTypeBool()
        {
            // true : Atk, false : Def
            return true;
        }

        public override void PrintItemInfo()
        {
            if (IsEquip)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                
                Console.WriteLine(String.Format("{0, -9}| 공격력 +{1,-2} | {2, -30}", Name, Atk, Dscr));
            }
            else
                Console.WriteLine(String.Format("{0, -12}| 공격력 +{1,-2} | {2, -30}", Name, Atk, Dscr));
        }
    }

    public class Armor : Item
    {
        public Armor(string name, string dscr, int price, int atk, int def) : base(name, dscr, price, atk, def)
        {

        }
        public override void Equip()
        {
            IsEquip = true;
        }
        public override void UnEquip()
        {
            IsEquip = false;
        }
        public override void GetStatus( out int status)
        {
            status = Def;
        }
        public override bool GetTypeBool()
        {
            // true : Atk, false : Def
            return false;
        }
        public override void PrintItemInfo()
        {
            if (IsEquip)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.WriteLine(String.Format("{0, -9}| 방어력 +{1,-2} | {2, -30}", Name, Def, Dscr));
            }
            else
                Console.WriteLine(String.Format("{0, -12}| 방어력 +{1,-2} | {2, -30}", Name, Def, Dscr));
           
        }
    }
}
